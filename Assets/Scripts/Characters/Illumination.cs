using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Illumination : Surgebinding
{
    public RuntimeAnimatorController defaultController;
    public RuntimeAnimatorController alternativeController;

    private int illuminationState;

    private const float ILLUMINATION_COST = 0.2f;

    new private void Start()
    {
        base.Start();
        illuminationState = 0;
    }

    protected override void CheckSurge()
    {
        if (surgeActive)
        {
            stamina -= ILLUMINATION_COST;
            if (stamina > 0)
            {
                if (illuminationState == 0)
                {
                    surgeUsed = true;
                    illuminationState = 1;

                    m_animator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    m_animator.runtimeAnimatorController = alternativeController;
                }
            }
            else
            {
                surgeActive = false;
            }
        }
        else
        {
            if (illuminationState == 1)
            {
                illuminationState = 0;
                m_animator.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                m_animator.runtimeAnimatorController = defaultController;
            }
        }
    }

    void OnIllumination(InputValue value)
    {
        //Debug.Log("illumination");
        if (!canSurge) return;
        surgeActive = !surgeActive;
    }

    public bool GetDisguised()
    {
        return illuminationState == 1;
    }
}
