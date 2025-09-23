using UnityEngine;

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
        public override void Skill(PlayerMovement player)
        {
            base.Skill(player);
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