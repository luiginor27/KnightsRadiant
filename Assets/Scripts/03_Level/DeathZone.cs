using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    private Vector2 checkPoint;

    private void Start()
    {
        checkPoint = new Vector2(-6, -1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject player = collision.gameObject;
            player.transform.position = checkPoint;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
