using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HS_Screen_Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
