using UnityEngine;

namespace Player.Strategy
{
    [CreateAssetMenu(fileName = "PlayerStrategyLuke", menuName = "Scriptable Objects/PlayerStrategyLuke")]
    public class PlayerStrategyLuke : PlayerStrategyScriptable
    {
        // NOVO: Campos para controle de salto duplo
        private const int MAX_JUMPS = 2; 
        private int m_jumpsRemaining;    

        private PlayerStrategyHandler.Strategy m_strategy = PlayerStrategyHandler.Strategy.Luke;
        public override PlayerStrategyHandler.Strategy strategy { get => m_strategy; protected set { m_strategy = value; } }

        public override void Jump(PlayerMovement player)
        {
            // Lógica do Salto Duplo: Só aplica a força se houver pulos restantes.
            if (m_jumpsRemaining > 0)
            {
                m_jumpsRemaining--;
                
                // Aplica a força de pulo no eixo Y, mantendo o momento horizontal.
                player.force = new Vector3(player.force.x, _jumpForce, player.force.z);
            }
        }

        public override void Move(PlayerMovement player)
        {
            base.Move(player);
        }

        public override void GetDirection(PlayerMovement player)
            // ... (Métodos GetDirection, Rotate, Skill, etc. permanecem inalterados)
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
            // Resetar pulos ao entrar na estratégia.
            m_jumpsRemaining = MAX_JUMPS;
            base.EnterStrategy(player);
        }

        public override void UpdateStrategy(PlayerMovement player)
        {
            // Resetar pulos se o jogador estiver no chão.
            if (player.isGrounded)
            {
                m_jumpsRemaining = MAX_JUMPS;
            }
            base.UpdateStrategy(player);
        }

        public override void ExitStrategy(PlayerMovement player)
        {
            base.ExitStrategy(player);
        }
    }
}