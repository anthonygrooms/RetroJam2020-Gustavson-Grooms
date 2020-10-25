using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Text resetScoresDirection;
    public Text resetScores;

    void Start()
    {
        resetScores.enabled = false;
        resetScoresDirection.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
        if (resetScoresDirection.enabled)
        {
            if (resetScores.text.Equals("ERASE"))
            {
                ResetScores();
            }
            if (resetScores.text.Equals("ERAS") && Input.GetKeyDown(KeyCode.E))
            {
                resetScores.text = "ERASE";
            }
            if (resetScores.text.Equals("ERA") && Input.GetKeyDown(KeyCode.S))
            {
                resetScores.text = "ERAS";
            }
            if (resetScores.text.Equals("ER") && Input.GetKeyDown(KeyCode.A))
            {
                resetScores.text = "ERA";
            }
            if (resetScores.text.Equals("E") && Input.GetKeyDown(KeyCode.R))
            {
                resetScores.text = "ER";
            }
            if (resetScores.text.Equals("") && Input.GetKeyDown(KeyCode.E))
            {
                resetScores.text = "E";
            }
        }
    }

    public void ResetScoresButton()
    {
        if (!resetScoresDirection.enabled)
        {
            resetScores.enabled = true;
            resetScoresDirection.enabled = true;
        }
    }

    void ResetScores()
    {
        resetScoresDirection.text = "SCORES ERASED";
        resetScores.text = "";
        resetScores.enabled = false;

        for (int i = 1; i <= 10; i++)
        {
            PlayerPrefs.SetInt("hs" + i, 0);
            PlayerPrefs.SetString("hsp" + i, "");
        }
    }

    public void GoToHighScoreScreen()
    {
        PlayerPrefs.SetString("PreviousScene", "Title");
        SceneManager.LoadScene("HighScores");
    }
}
