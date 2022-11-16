using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionManager : MonoBehaviour
{
    private GameObject[] bandits;
    private CharacterController2D player;

    private bool playerDetected;
    private float detectionCounter;
    private GameObject blackScreen;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        bandits = GameObject.FindGameObjectsWithTag("Bandit");

        playerDetected = false;
        detectionCounter = 0;
        blackScreen = GameObject.FindGameObjectWithTag("Canvas").transform.Find("BlackScreen").gameObject;
    }

    private void Update()
    {
        if (playerDetected)
        {
            detectionCounter += Time.deltaTime;

            if (detectionCounter > 2)
            {
                resetPlayerPosition();

                player.enableMovement(true);
                player.enableSurge(true);

                for (int i = 0; i < bandits.Length; i++)
                {
                    bandits[i].GetComponent<Bandit>().ResetState();
                    bandits[i].GetComponent<Patrol>().StartPatrol();
                }

                detectionCounter = 0;
                playerDetected = false;
            }
        }
    }

    public bool PlayerDetected()
    {
        Illumination surge = player.GetComponent<Illumination>();
        if ((surge == null) || (!surge.GetDisguised())) {
            for (int i = 0; i < bandits.Length; i++)
            {
                bandits[i].GetComponent<Bandit>().PlayerDetected();
            }

            player.enableMovement(false);
            player.enableSurge(false);
            playerDetected = true;
            blackScreen.GetComponent<Animator>().Play("FadeInOutCaught", 0, 0);

            return true;
        }
        return false;
    }

    private void resetPlayerPosition()
    {
        player.transform.position = new Vector2(-6, -1);
    }
}
