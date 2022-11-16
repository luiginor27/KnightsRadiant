using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour
{
    [Header("Movement")]
    protected bool canMove;
    private float moveValue;
    public float maxSpeed = 5f;
    private bool _facingRight = true;

    [Header("Jump")]
    private bool canJump;
    private float _jumpVal;
    private bool _jumpKeyPressed;
    public float jumpForce = 1700f;
    private bool m_grounded;
    private Transform m_groundCheck;
    private float groundRadious = 0.2f;
    public LayerMask whatIsGround;

    private bool forceIdle;

    private Image m_staminaBar;

    private Animator m_animator;
    private Rigidbody2D m_rigidbody;

    protected const float DEFAULT_LINEAR_DRAG = 10;

    // Use this for initialization
    void Start()
    {
        canMove = true;
        canJump = true;
        forceIdle = false;

        m_groundCheck = transform.Find("GroundCheck");
        m_staminaBar = transform.Find("Canvas").Find("StaminaBar").GetComponent<Image>();
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckJump();
        CheckGrounded();
        CheckMovement();
    }

    private void CheckGrounded()
    {
        m_grounded = Physics2D.OverlapCircle(m_groundCheck.position, groundRadious, whatIsGround);
        //if (m_grounded) m_rigidbody.drag = DEFAULT_LINEAR_DRAG;
        m_animator.SetBool("Grounded", m_grounded);
    }

    private void CheckMovement()
    {
        if (forceIdle)
        {
            m_animator.SetInteger("AnimState", 0);
            forceIdle = false;
        }
        else
        {
            if (canMove)
            {
                if (moveValue != 0)
                {
                    m_rigidbody.velocity = new Vector2(moveValue * maxSpeed, m_rigidbody.velocity.y);
                    m_animator.SetInteger("AnimState", 1);

                    if ((moveValue > 0 && !_facingRight) || (moveValue < 0 && _facingRight))
                    {
                        Flip();
                    }
                }
                else
                {
                    m_animator.SetInteger("AnimState", 0);
                }
            }

            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeedY", m_rigidbody.velocity.y);
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(new Vector3(0, 180, 0), Space.World);
        m_staminaBar.transform.Rotate(new Vector3(0, 180, 0), Space.World);
    }

    private void CheckJump()
    {
        if (canJump)
        {
            if (m_grounded && _jumpVal > 0)
            {
                if (_jumpKeyPressed == false)
                {
                    _jumpKeyPressed = true;
                    _jumpVal = 0;
                    m_grounded = false;
                    m_rigidbody.AddForce(new Vector2(0, jumpForce));
                    m_animator.SetTrigger("Jump");
                    m_animator.SetBool("Grounded", m_grounded);
                }
            }

            if (_jumpVal == 0 && _jumpKeyPressed)
            {
                _jumpKeyPressed = false;
            }
        }
    }

    public void enableMovement(bool enable)
    {
        canJump = enable;
        canMove = enable;

        forceIdle = true;
        moveValue = 0;
        
        if(!enable) m_rigidbody.velocity = new Vector2(0, 0);
    }

    public void enableSurge(bool enable)
    {
        if (gameObject.GetComponent<Surgebinding>() != null)
        {
            gameObject.GetComponent<Surgebinding>().SetCanSurge(enable);
        }
    }

    public bool getGrounded()
    {
        return m_grounded;
    }

    public float getMoveValue()
    {
        return moveValue;
    }

    public void setSpeed(float newSpeed)
    {
        maxSpeed = newSpeed;
    }

    void OnJump(InputValue value)
    {
        _jumpVal = value.Get<float>();
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<float>();
    }

    public void ReachEnding()
    {
        enableMovement(false);
        enableSurge(false);
    }
}
