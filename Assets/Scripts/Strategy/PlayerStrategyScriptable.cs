using UnityEngine;

namespace Player.Strategy
{
    [System.Serializable]
    public abstract class PlayerStrategyScriptable : ScriptableObject
    {
        [SerializeField] protected float _speed = 8;

        [SerializeField] protected float _jumpForce = 10;
        [SerializeField] protected float _jumpCancelFactor = 0;
        [SerializeField] protected float _gravity = -20;
        [SerializeField] protected float _fallGravityFactor = 3;



        #region Properties

        public abstract PlayerStrategyHandler.Strategy strategy { get; protected set; }

        public float speed { get { return _speed; } private set { _speed = value; } }
        public float jumpForce { get { return _jumpForce; } private set { _jumpForce = value; } }
        public float jumpCancelFactor { get { return _jumpCancelFactor; } }
        public float gravity { get { return _gravity; } }
        public float fallGravityFactor { get { return _fallGravityFactor; } }

        #endregion

        public virtual void Jump(PlayerMovement player)
        {
            player.force = new Vector3(player.force.x, _jumpForce, player.force.z);
        }
        public virtual void Move(PlayerMovement player)
        {
            Vector3 movement = player.direction * _speed;
            movement.y = _gravity;

            player.HandleGravity();

            player.force = new Vector3(movement.x, player.force.y, movement.z);
        }
        public virtual void GetDirection(PlayerMovement player)
        {
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = Camera.main.transform.right;
            right.y = 0;
            right.Normalize();
            player.direction = forward * player.input.z + right * player.input.x;
        }
        public virtual void Rotate(PlayerMovement player)
        {
            if (player.direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(player.force, Vector3.up);
                toRotation = Quaternion.Euler(0, toRotation.eulerAngles.y, 0);

                player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation, 720 * Time.deltaTime);
            }
        }
        public virtual void Skill(PlayerMovement player) 
        {
            
        }

        public virtual void EnterStrategy(PlayerMovement player)
        {

        }

        public virtual void UpdateStrategy(PlayerMovement player)
        {

        }

        public virtual void ExitStrategy(PlayerMovement player)
        {

        }

    }
}
