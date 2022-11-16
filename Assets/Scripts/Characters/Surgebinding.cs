using UnityEngine;
using UnityEngine.UI;

abstract public class Surgebinding : MonoBehaviour
{
    public float stamina = MAX_STAMINA;

    protected bool canSurge;
    protected bool surgeUsed;
    protected bool surgeActive;

    protected const float MAX_STAMINA = 50;
    protected const float STAMINA_REGENERATION_RATE = 2;
    protected const float DEFAULT_LINEAR_DRAG = 10;

    protected CharacterController2D m_characterController;
    private GameObject m_staminaBar;

    protected Rigidbody2D m_rigidbody;
    protected Animator m_animator;

    // Start is called before the first frame update
    protected void Start()
    {
        canSurge = true;
        surgeUsed = false;
        surgeActive = false;

        m_characterController = transform.GetComponent<CharacterController2D>();
        m_staminaBar = m_characterController.transform.Find("Canvas").Find("StaminaBar").gameObject;

        m_rigidbody = transform.GetComponent<Rigidbody2D>();
        m_animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        CheckStamina();

        if (canSurge)
        {
            CheckSurge();
        }
    }

    private void CheckStamina()
    {
        m_staminaBar.SetActive(stamina < MAX_STAMINA);
        m_staminaBar.transform.GetComponent<Image>().fillAmount = stamina / MAX_STAMINA;

        if (stamina < MAX_STAMINA && m_characterController.getGrounded() && !surgeActive)
        {
            stamina += STAMINA_REGENERATION_RATE;
        }
    }

    abstract protected void CheckSurge();

    public bool getSurgeUsed()
    {
        return surgeUsed;
    }

    public void SetCanSurge(bool enable)
    {
        canSurge = enable;
    }
}
