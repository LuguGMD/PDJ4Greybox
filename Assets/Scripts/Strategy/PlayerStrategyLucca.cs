using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Player.Strategy
{
    //[CreateAssetMenu(fileName = "PlayerStrategyLucca", menuName = "Scriptable Objects/PlayerStrategyLucca")]
    public class PlayerStrategyLucca : PlayerStrategyScriptable
    {
        [SerializeField] float _dashDistance = 3;

        [SerializeField] float _dashDuration = 0.15f;
        [SerializeField] float _dashCooldown = 0.5f;

        float dashTimer;
        Vector3 dashDirection;
        float dashSpeed;
        float dashRotationSpeed = 3000;

        #region Properties
        public float dashDistance { get { return _dashDistance; } private set { _dashDistance = value; } }
        public float dashDuration { get { return _dashDuration; } private set { _dashDuration = value; } }
        public float dashCooldown { get { return _dashCooldown; } private set { _dashCooldown = value; } }

        #endregion

        private PlayerStrategyHandler.Strategy m_strategy = PlayerStrategyHandler.Strategy.Lucca;
        public override PlayerStrategyHandler.Strategy strategy { get => m_strategy; protected set { m_strategy = value; } }

        public override void Jump(PlayerMovement player)
        {
            base.Jump(player);
        }
        public override void Move(PlayerMovement player)
        {
            if (dashTimer < dashDuration)
            {
                player.force = dashSpeed * dashDirection;
            }
            else
            {
                Vector3 movement = player.direction * _speed;
                movement.y = _gravity;

                player.force = new Vector3(movement.x, player.force.y, movement.z);
            }
            dashTimer += Time.deltaTime;
        }
        public override void GetDirection(PlayerMovement player)
        {
            base.GetDirection(player);
        }
        public override void Rotate(PlayerMovement player)
        {
            base.Rotate(player);

            if (dashTimer < dashDuration)
            {
                Quaternion toRotation = Quaternion.LookRotation(dashDirection, Vector3.up);
                toRotation = Quaternion.Euler(0, toRotation.eulerAngles.y, 0);

                player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation,  dashRotationSpeed * Time.deltaTime);
            }
        }
        public override void Skill(PlayerMovement player)
        {
            if (dashTimer >= dashDuration + dashCooldown)
            {
                dashTimer = 0;
                dashDirection = player.direction.magnitude == 0 ? Camera.main.transform.forward : player.direction;
                dashDirection.y = 0;
                dashDirection.Normalize();
            }
        }

        public override void EnterStrategy(PlayerMovement player)
        {
            dashTimer = dashDuration + dashCooldown;
            dashSpeed = dashDistance / dashDuration;
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
