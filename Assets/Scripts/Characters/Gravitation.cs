using UnityEngine;
using UnityEngine.InputSystem;

public class Gravitation : Surgebinding
{
    public float lashForce;

    private Vector2 lashValue;
    private bool cancelLash;
    private bool resetLinearDrag;

    private float groundCheckCounter;
    private const float GROUND_CHECK_DELAY = 2;
    protected const float DEFAULT_GRAVITY_SCALE = 1;
    private const float GRAVITATION_LINEAR_DRAG = 1;

    private const float LASH_COST = 1;

    new void Start()
    {
        base.Start();
        cancelLash = false;
        resetLinearDrag = false;
        groundCheckCounter = 0;
    }

    override protected void CheckSurge()
    {
        if (!surgeActive)
        {
            if ((lashValue.x != 0 || lashValue.y != 0) && (stamina > 0))
            {
                //Debug.Log("start lash");

                surgeActive = true;
                surgeUsed = true;
                groundCheckCounter = 0;

                m_rigidbody.gravityScale = 0f;
                m_rigidbody.drag = GRAVITATION_LINEAR_DRAG;
                m_animator.SetBool("Lash", true);

                m_characterController.enableMovement(false);
            }
        }
        else
        {
            if (((groundCheckCounter >= GROUND_CHECK_DELAY) && m_characterController.getGrounded())
                || cancelLash
                || stamina <= 0)
            {
                //Debug.Log("stop lash");

                lashValue = new Vector2(0, 0);
                surgeActive = false;
                cancelLash = false;

                m_rigidbody.gravityScale = DEFAULT_GRAVITY_SCALE;
                m_animator.SetBool("Lash", false);

                m_characterController.enableMovement(true);

                resetLinearDrag = true;
            }
            else
            {
                //Debug.Log("add force");
                stamina -= LASH_COST;
                groundCheckCounter += Time.fixedDeltaTime;
                m_rigidbody.AddForce(new Vector2(lashValue.x, lashValue.y) * lashForce);
            }
        }

        if (m_characterController.getGrounded() && resetLinearDrag)
        {
            m_rigidbody.drag = DEFAULT_LINEAR_DRAG;
            resetLinearDrag = false;
        }
    }

    void OnActivateLash(InputValue value)
    {
        //Debug.Log("right stick move");
        Vector2 temp = value.Get<Vector2>();
        lashValue = (temp != lashValue) ? temp : new Vector2(0, 0);
    }

    void OnCancelLash(InputValue value)
    {
        //Debug.Log("right stick press");
        cancelLash = true;
    }
}
