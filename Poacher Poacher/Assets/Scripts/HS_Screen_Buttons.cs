using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HS_Screen_Buttons : MonoBehaviour
{
    public Text buttonText;
    // Start is called before the first frame update
    void Start()
    {
        string scn = PlayerPrefs.GetString("PreviousScene", "");
        if(scn == "Title")
        {
            buttonText.text = "PLAY";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toTitleScreen()
    {
        SceneManager.LoadScene("Title");
    }
    public void toGame()
    {
        SceneManager.LoadScene("Game");
    }
}
