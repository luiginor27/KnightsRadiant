using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SelectionMenuManager : MonoBehaviour
{
    private const int ORDERS_NUMBER = 10;

    private Text selectionText;
    private string[] orders = { "Edgedancers", "Dustbringers", "Skybreakers", "Windrunners", "Stonewards", 
        "Willshapers", "Elsecallers", "Lightweavers", "Bondsmiths", "Truthwatchers" };

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        selectionText = canvas.transform.Find("SelectionText").GetComponent<Text>();

        var buttonsObject = canvas.transform.Find("Buttons");

        for (int i = 0; i < ORDERS_NUMBER; i++)
        {
            SelectionButton button = buttonsObject.GetChild(i).GetComponent<SelectionButton>();
            button.Initialize(this, orders[i]);
        }
    }


    public void changeText(string character)
    {
        if (character != "")
        {
            selectionText.text = character;
        } else
        {
            selectionText.text = "Select your character";
        }
    }

    public void loadScene(string orderName)
    {
        SceneManager.LoadScene(orderName + "Level");
    }

    public void OnReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
