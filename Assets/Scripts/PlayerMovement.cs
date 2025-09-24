using Player;
using Player.Strategy;
using System;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController), typeof(PlayerStrategyHandler))]
public class PlayerMovement : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walking,
        Jumping,
        Falling,
        Climbing
    }
    private State m_currentState = State.Idle;

    [SerializeField] private PlayerStrategyHandler.Strategy m_startStrategy;
    private PlayerStrategyScriptable m_currentStrategy;

    private CharacterController m_characterController;

    private Vector3 m_input;
    private bool m_canCancelJump = false;
    private bool m_isGrounded = false;
    private bool m_canJump = false;

    private Vector3 m_force;
    private Vector3 m_direction;

    //Strategy
    private PlayerStrategyHandler m_strategyHandler;

    private Action<PlayerMovement> JumpStrategy;
    private Action<PlayerMovement> MoveStrategy;
    private Action<PlayerMovement> DirectionStrategy;
    private Action<PlayerMovement> RotateStrategy;
    private Action<PlayerMovement> SkillStrategy;



    private float m_lastTimeOnGround;

    #region Properties

    public Vector3 force
    {
        get { return m_force; }
        internal set { m_force = value; }
    }

    public Vector3 direction
    {
        get { return m_direction; }
        internal set { m_direction = value; }
    }

    public Vector3 input
    {
        get { return m_input; }
        private set { m_input = value; }
    }

    public CharacterController characterController { get  { return m_characterController; } internal set { m_characterController = value; } }
    public State currentState { get { return m_currentState; } internal set { m_currentState = value; } }

    public bool isGrounded { get { return m_isGrounded; } }

    #endregion

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
        m_strategyHandler = GetComponent<PlayerStrategyHandler>();

        StartStrategy();
        LockMouse();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateStrategy?.Invoke(this);

        m_currentStrategy.UpdateStrategy(this);
    }

    private void LateUpdate()
    {
        CheckGround();
    }

    private void Move()
    {
        HandleGravity();

        DirectionStrategy?.Invoke(this);
        MoveStrategy?.Invoke(this);

        characterController.Move(m_force * Time.deltaTime);
    }

    internal void HandleGravity()
    {
        float gravity = m_currentStrategy.gravity;

        if (m_force.y < 0)
        {
            gravity *= m_currentStrategy.fallGravityFactor;
        }

        m_force.y += gravity * Time.deltaTime;

        if (m_isGrounded)
        {
            m_lastTimeOnGround = Time.time;
        }
    }

    private void CheckGround()
    {
        m_isGrounded = m_characterController.isGrounded;
        m_canJump = m_characterController.isGrounded;

        if (m_isGrounded)
        {
            ChangeState(State.Idle);
            m_force.y = 0;
        }
        else
        {
            ChangeState(State.Jumping);
        }
    }

    public void GetDirectionInput(Vector3 direction)
    {
        m_input.x = direction.x;
        m_input.z = direction.z;
    }

    public void GetJumpInput(InputInfo input)
    {
        if (!input.IsPressed && m_force.y > 0 && m_canCancelJump)
        {
            m_force.y *= m_currentStrategy.jumpCancelFactor;
            m_canCancelJump = false;
        }

        bool coyoteTimeEnabled = input.GetDelayInput(m_lastTimeOnGround);
        if ((m_isGrounded || coyoteTimeEnabled) && m_canJump)
        {
            if (input.IsEnabled || coyoteTimeEnabled)
            {
                m_canCancelJump = true;
                JumpStrategy?.Invoke(this);
                m_canJump = false;
            }
            else if (!input.IsPressed)
            {
                m_canCancelJump = false;
            }
        }
    }

    public void GetSkillInput(InputInfo input)
    {
        if (input.IsDown)
        {
            SkillStrategy?.Invoke(this);
        }
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    internal void ChangeStrategy(PlayerStrategyHandler.Strategy nextStrategyEnum)
    {
        JumpStrategy -= m_currentStrategy.Jump;
        MoveStrategy -= m_currentStrategy.Move;
        DirectionStrategy -= m_currentStrategy.GetDirection;
        SkillStrategy -= m_currentStrategy.Skill;
        RotateStrategy -= m_currentStrategy.Rotate;

        m_currentStrategy.ExitStrategy(this);


        m_currentStrategy = m_strategyHandler.ChangeStrategy(nextStrategyEnum);

        m_currentStrategy.EnterStrategy(this);

        JumpStrategy += m_currentStrategy.Jump;
        MoveStrategy += m_currentStrategy.Move;
        DirectionStrategy += m_currentStrategy.GetDirection;
        SkillStrategy += m_currentStrategy.Skill;
        RotateStrategy += m_currentStrategy.Rotate;
    }
    private void StartStrategy()
    {
        m_currentStrategy = m_strategyHandler.ChangeStrategy(m_startStrategy);

        m_currentStrategy.EnterStrategy(this);

        JumpStrategy += m_currentStrategy.Jump;
        MoveStrategy += m_currentStrategy.Move;
        DirectionStrategy += m_currentStrategy.GetDirection;
        SkillStrategy += m_currentStrategy.Skill;
        RotateStrategy += m_currentStrategy.Rotate;
    }
    public PlayerStrategyScriptable GetStrategy(PlayerStrategyHandler.Strategy strategy)
    {
        return m_strategyHandler.ChangeStrategy(strategy);
    }
    private void ChangeState(State state)
    {
        m_currentState = state;
    }

}
