using UnityEngine;
using UnityEngine.AI;


public class Patrol : MonoBehaviour
{
    
    public Transform[] points;
    private int destPoint = 0;
    public float moveSpeed = 2f;
    private bool patrol;

    public float detectionRange = 3f;
    private bool facingRight;
    public LayerMask rayCastLayer;
    private Transform rayCastOrigin;

    private DetectionManager detectionManager;

    private void Start()
    {
        destPoint = 0;
        patrol = true; 

        facingRight = true;
        rayCastOrigin = transform.Find("RayCastOrigin").transform;

        detectionManager = GameObject.Find("DetectionManager").GetComponent<DetectionManager>();
    }

    void Update()
    {
        if (patrol)
        {
            CheckMovement();
            CheckDetection();
        }
    }

    private void CheckMovement()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (Mathf.Abs(transform.position.x - points[destPoint].position.x) < 0.2)
        {
            destPoint = (destPoint + 1) % points.Length;
            transform.Rotate(new Vector3(0, 180, 0));
            facingRight = !facingRight;
        }

        float step = moveSpeed * Time.deltaTime;
        Vector2 target = new Vector2(points[destPoint].position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    private void CheckDetection()
    {
        Vector2 direction = -transform.right;


        RaycastHit2D hit = Physics2D.Raycast(rayCastOrigin.position, direction, detectionRange, rayCastLayer);
        Debug.DrawRay(rayCastOrigin.position, direction * detectionRange, Color.green);

        //if (hit.collider != null)
        //    Debug.Log(name + " --- " + hit.collider.name);


        if ((hit.collider != null) && hit.collider.CompareTag("Player"))
        {
            if(detectionManager.PlayerDetected()) patrol = false;
        }
    }

    public void StartPatrol()
    {
        patrol = true;
    }
}