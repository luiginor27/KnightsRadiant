using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour
{
    void OnStart()
    {
        SceneManager.LoadScene(1);
    }

     void OnQuit()
    {
        Application.Quit();
    }
}
