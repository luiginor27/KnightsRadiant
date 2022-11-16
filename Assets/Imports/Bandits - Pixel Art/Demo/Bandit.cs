using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{

    /*[SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;*/

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private bool m_combatIdle = false;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Combat Idle
        if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Run
        else if (m_body2d.velocity.x != Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    public void PlayerDetected()
    {
        m_body2d.velocity.Set(0,0);
        m_combatIdle = true;
    }

    public void ResetState()
    {
        m_combatIdle = false;
    }
}
