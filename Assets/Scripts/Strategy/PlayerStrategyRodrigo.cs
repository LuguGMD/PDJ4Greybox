using Unity.Cinemachine;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


namespace Player.Strategy
{
    //[CreateAssetMenu(fileName = "PlayerStrategyRodrigo", menuName = "Scriptable Objects/PlayerStrategyRodrigo")]
    public class PlayerStrategyRodrigo : PlayerStrategyScriptable
    {
        private PlayerStrategyHandler.Strategy m_strategy = PlayerStrategyHandler.Strategy.Rodrigo;
        //private float graplingCooldownTimer;
        //private float maxGrapplingDistance
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
        public override void Skill(PlayerMovement player)
        {
            RaycastHit hit;

            if(Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
                Camera.main.GetComponent<CinemachineBrain>().enabled = false;
                player.gameObject.GetComponent<CharacterController>().enabled = false;
                player.gameObject.GetComponent<Animator>().enabled = false;
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit)) 
                {
                    player.transform.position = hit.point;
                    player.gameObject.GetComponent<CharacterController>().enabled = true;
                    player.gameObject.GetComponent<Animator>().enabled = true;
                    Camera.main.GetComponent<CinemachineBrain>().enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
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