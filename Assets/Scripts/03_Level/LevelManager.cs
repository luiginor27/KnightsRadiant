using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    private GameObject character;

    //private GameObject surgeHint;
    //private float hintCounter;
    //private const float HINT_TIME = 10f;

    //private GameObject tutorialUI;

    private GameObject endingUI;
    //private bool endingReached;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        //character.GetComponent<CharacterController2D>().enableMovement(false);

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        //tutorialUI = canvas.transform.Find("TutorialUI").gameObject;

        endingUI = canvas.transform.Find("EndingUI").gameObject;
        //endingReached = false;
    }

    public void EndingReached()
    {
        endingUI.SetActive(true);
        endingUI.GetComponent<Animator>().Play("Base Layer.FadeIn", 0, 0);
        //endingReached = true;
        character.GetComponent<CharacterController2D>().ReachEnding();
    }

    //void OnSkipTutorial(InputValue value)
    //{
    //    Debug.Log("hey");
    //    if(tutorialUI.activeSelf)
    //    {
    //        tutorialUI.SetActive(false);
    //        character.GetComponent<CharacterController2D>().enableMovement(true);
    //    }
    //}

    void OnReturnToSelection(InputValue value)
    {
        //if (endingReached)
        //{
            SceneManager.LoadScene(1);
        //}
    }
}
