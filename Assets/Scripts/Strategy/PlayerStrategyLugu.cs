using UnityEngine;

namespace Player.Strategy
{
    //[CreateAssetMenu(fileName = "PlayerStrategyLugu", menuName = "Scriptable Objects/PlayerStrategyLugu")]
    public class PlayerStrategyLugu : PlayerStrategyScriptable
    {
        private PlayerStrategyHandler.Strategy m_strategy = PlayerStrategyHandler.Strategy.Lugu;
        public override PlayerStrategyHandler.Strategy strategy { get => m_strategy; protected set { m_strategy = value; } }
    }
}
