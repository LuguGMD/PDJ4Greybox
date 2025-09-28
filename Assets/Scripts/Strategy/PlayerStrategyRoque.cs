using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using Unity.VisualScripting;

namespace Player.Strategy
{
    //[CreateAssetMenu(fileName = "PlayerStrategyRoque", menuName = "Scriptable Objects/PlayerStrategyRoque")]
    public class PlayerStrategyRoque : PlayerStrategyScriptable
    {
        private PlayerStrategyHandler.Strategy m_strategy = PlayerStrategyHandler.Strategy.Roque;
        public override PlayerStrategyHandler.Strategy strategy { get => m_strategy; protected set { m_strategy = value; } }

        public override void Jump(PlayerMovement player)
        {
            base.Jump(player);
        }
        public override void Move(PlayerMovement player)
        {
            base.Move(player);
        }
        public override void GetDirection(PlayerMovement player)
        {
            base.GetDirection(player);
        }
        public override void Rotate(PlayerMovement player)
        {
            base.Rotate(player);
        }

        [Header("Configurações de Pegar Objeto")]
        public float maxPickupDistance = 20f;
        public LayerMask pickupMask = ~0;
        public float moveSpeed = 10f; // Velocidade da interpolação
        public Vector3 holdOffset = new Vector3(0, 1f, 2f); // posição em relação ao player
 
        private GameObject carriedObject;
        private Coroutine moveCoroutine;

        /// <summary>
        /// Método chamado quando o input correto da Skill é ativado.
        /// </summary>
        public override void Skill(PlayerMovement player)
        {
            TryPickupOrDrop(player);
        }

        void TryPickupOrDrop(PlayerMovement player)
        {
            Collider[] colliders = Physics.OverlapSphere(player.transform.position, maxPickupDistance, pickupMask);
            Debug.Log($"[Pickup] {colliders.Length} objeto(s) detectado(s) no raio.");

            if (carriedObject == null)
            {
                var grabbables = colliders
                    .Where(c => c.CompareTag("Grabbable"))
                    .OrderBy(c => Vector3.Distance(player.transform.position, c.transform.position))
                    .ToArray();

                if (grabbables.Length > 0)
                {
                    Collider nearest = grabbables.First(); // agora só pega de objetos válidos
                    Debug.Log($"[Pickup] Objeto mais próximo (Grabbable): {nearest.name}");
                    Debug.Log($"[Pickup] Pegando objeto: {nearest.name}");
                    PickupObject(nearest.gameObject, player);
                }
            }
            else
            {
                    DropObject(player);          
            }
        }


        void PickupObject(GameObject obj, PlayerMovement player)
        {
            carriedObject = obj;
            carriedObject.transform.SetParent(null);
            obj.GetComponent<Rigidbody>().useGravity = false;
            if (moveCoroutine != null) player.StopCoroutine(moveCoroutine);
            moveCoroutine = player.StartCoroutine(MoveObjectToHoldPoint(player));
            Debug.Log($"Pegou: {carriedObject.name}");
        }

        IEnumerator MoveObjectToHoldPoint(PlayerMovement player)
        {
            Vector3 targetPos;
            do
            {
                targetPos = player.transform.position + player.transform.forward * holdOffset.z + Vector3.up * 3 * holdOffset.y;

                carriedObject.transform.position = Vector3.Lerp(
                    carriedObject.transform.position,
                    targetPos,
                    Time.deltaTime * moveSpeed
                );

                yield return null;
            }
            while (Vector3.Distance(carriedObject.transform.position, targetPos) > 0.05f);
            carriedObject.transform.position = targetPos;
            carriedObject.transform.rotation = player.transform.rotation;
            carriedObject.transform.SetParent(player.transform);

            moveCoroutine = null;
        }

        void DropObject(PlayerMovement player)
        {
            if (carriedObject == null) return;

            if (moveCoroutine != null)
            {
                player.StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
            carriedObject.transform.SetParent(null);
            carriedObject.GetComponent<Rigidbody>().useGravity = true;
            carriedObject = null;
            Debug.Log("Objeto solto");
        }



        public override void EnterStrategy(PlayerMovement player)
        {
            base.EnterStrategy(player);
        }

        public override void UpdateStrategy(PlayerMovement player)
        {
            base.UpdateStrategy(player);
        }

        public override void ExitStrategy(PlayerMovement player)
        {
            base.ExitStrategy(player);
        }
    }
}