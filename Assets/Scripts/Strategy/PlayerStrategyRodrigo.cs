using UnityEngine;


namespace Player.Strategy
{
    //[CreateAssetMenu(fileName = "PlayerStrategyRodrigo", menuName = "Scriptable Objects/PlayerStrategyRodrigo")]
    public class PlayerStrategyRodrigo : PlayerStrategyScriptable
    {
        private PlayerStrategyHandler.Strategy m_strategy = PlayerStrategyHandler.Strategy.Rodrigo;
        private bool isGrabing = false;
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

            Vector3 RayInitPos = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y + 1.5f, player.gameObject.transform.position.z);
            
            if (Physics.Raycast(RayInitPos, player.gameObject.transform.forward, out hit, 2f))
            {
                if (hit.transform.CompareTag("Grabbable") && !isGrabing)
                {
                    isGrabing = true;
                    hit.transform.SetParent(player.gameObject.transform);
                    hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
                else
                {
                    hit.transform.SetParent(null);
                    hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    isGrabing = false;
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