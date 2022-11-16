using UnityEngine;
using UnityEngine.InputSystem;

public class Abrasion : Surgebinding
{
    public float abrasionForce;

    private bool resetLinearDrag;

    private const float DEFAULT_SPEED = 5;
    private const float ABRASION_SPEED = 15;
    private const float ABRASION_COST = 1f;
    private const float ABRASION_LINEAR_DRAG = 1f;

    new void Start()
    {
        base.Start();
        resetLinearDrag = false;
    }

    protected override void CheckSurge()
    {
        //Debug.Log("stamina " + stamina);

        if (surgeActive && stamina > 0)
        {
            surgeUsed = true;

            stamina -= ABRASION_COST;

            if (stamina <= 0)
            {
                surgeActive = false;
                setAbrasionActive();
            }

            m_rigidbody.drag = ABRASION_LINEAR_DRAG;
            m_characterController.setSpeed(ABRASION_SPEED);
        }
        else
        {
            m_characterController.setSpeed(DEFAULT_SPEED);
            resetLinearDrag = true;
        }

        if (m_characterController.getGrounded() && resetLinearDrag)
        {
            m_rigidbody.drag = DEFAULT_LINEAR_DRAG;
            resetLinearDrag = false;
        }
    }

    private void setAbrasionActive()
    {
        m_animator.SetBool("Abrasion", surgeActive);
    }

    void OnAbrasion(InputValue value)
    {
        //Debug.Log("abrasion");
        surgeActive = value.Get<float>() >= 0.5;
        setAbrasionActive();
    }

}
