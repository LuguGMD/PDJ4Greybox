using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private enum State
    {
        Idle,
        Walking,
        Jumping,
        Falling,
        Climbing
    }

    private enum Form
    {
        Solid,
        Mud,
        Transforming
    }

    private State m_currentState = State.Idle;
    private Form m_currentForm = Form.Solid;

    private CharacterController m_characterController;

    [SerializeField] private float m_speed = 5f;
    [SerializeField] private float m_jumpForce = 10f;
    [SerializeField] private float m_jumpCancelFactor = 0;
    private Vector3 m_input;
    private bool m_canCancelJump = false;
    private bool m_isGrounded = false;
    private bool m_canJump = false;

    private Vector3 m_force;
    private Vector3 m_direction;

    [SerializeField] private float m_gravity = -9f;
    [SerializeField] private float m_fallGravityFactor = 1.5f;

    [SerializeField] private InputInfo m_jumpInput;
    [SerializeField] private InputInfo m_transformInput;

    [Header("Solid")]
    [SerializeField] private float m_solidHeight;

    [Header("Mud")]
    [SerializeField] private float m_mudHeight;

    private float m_lastTimeOnGround;

    #region Properties

    public Vector3 force
    {
        get { return m_force; }
    }

    public Vector3 direction
    {
        get { return m_direction; }
    }

    #endregion

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
        Jump();
        Rotate();

        switch (m_currentForm)
        {
            case Form.Solid:
                // Solid form logic
                break;
            case Form.Mud:
                // Mud form logic
                break;
            case Form.Transforming:
                // Transforming logic
                break;
        }
    }

    private void LateUpdate()
    {
        CheckGround();
    }

    private void Move()
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();

        m_direction = forward * m_input.z + right * m_input.x;
        Vector3 movement = m_direction * m_speed;
        movement.y = m_gravity;

        HandleGravity();

        m_force.x = movement.x;
        m_force.z = movement.z;

        m_characterController.Move(m_force * Time.deltaTime);
    }

    private void Rotate()
    {
        if (m_direction != Vector3.zero)
        {
            

            Quaternion toRotation = Quaternion.LookRotation(m_force, Vector3.up);
            toRotation = Quaternion.Euler(0, toRotation.eulerAngles.y, 0);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }
    }

    private void HandleGravity()
    {
        if (!m_isGrounded)
        {
            float gravity = m_gravity;

            if(m_force.y < 0)
            {
                gravity *= m_fallGravityFactor;
            }

            if(!m_jumpInput.isPressed && m_force.y > 0 && m_canCancelJump)
            {
                m_force.y *= m_jumpCancelFactor;
                m_canCancelJump = false;
            }
            else
            {
                m_force.y += gravity * Time.deltaTime;
            }
        }
        else if(m_isGrounded)
        {
            m_lastTimeOnGround = Time.time;
        }
    }

    private void Jump()
    {
        bool coyoteTimeEnabled = m_jumpInput.GetDelayInput(m_lastTimeOnGround);
        if ((m_isGrounded || coyoteTimeEnabled) && m_canJump)
        {
            if (m_jumpInput.isEnabled || coyoteTimeEnabled)
            {
                m_canCancelJump = true;
                m_force.y = m_jumpForce;
                m_canJump = false;
            }
            else if(!m_jumpInput.isPressed)
            {
                m_canCancelJump = false;
            }
        }
    }

    private void CheckGround()
    {
        m_isGrounded = m_characterController.isGrounded;
        m_canJump = m_characterController.isGrounded;

        if (m_isGrounded) m_force.y = 0;
    }

    private void GetInputs()
    {
        m_jumpInput.GetInput();
        m_transformInput.GetInput();

        if (m_transformInput.isDown)
        {
            if (m_currentForm == Form.Solid)
            {
                ChangeForm(Form.Mud);
            }
            else if (m_currentForm == Form.Mud)
            {
                ChangeForm(Form.Solid);
            }
        }

            m_input.x = Input.GetAxis("Horizontal");
        m_input.z = Input.GetAxis("Vertical");
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private bool TransformValidation(Form nextForm)
    {
        switch(nextForm)
        {
            case Form.Solid:

                Physics.Raycast(transform.position, Vector3.up, out RaycastHit hitInfo, m_solidHeight);
                if (hitInfo.collider != null)
                    return false;

                break;
            case Form.Mud:

                

                break;
        }
        return true;
    }

    private void ChangeForm(Form nextForm)
    {
        if (TransformValidation(nextForm))
        {
            m_currentForm = nextForm;

            switch (m_currentForm)
            {
                case Form.Solid:
                    m_characterController.height = m_solidHeight;
                    m_speed = 10;
                    break;
                case Form.Mud:
                    m_characterController.height = m_mudHeight;
                    m_speed = 2;
                    break;
            }
        }
    }

    private void ChangeState(State nextState)
    {
        m_currentState = nextState;
    }
}
