using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighScores : MonoBehaviour
{
    public Text display;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("hsp1"))
        {
            for (int i = 1; i <= 10; i++)
            {
                PlayerPrefs.SetString("hsp" + i, "");
                PlayerPrefs.SetInt("hs" + i, 0);
                PlayerPrefs.Save();
            }
        }
        string[] names = new string[10];
        int[] scores = new int[10];
        for (int i = 1; i <= 10; i++)
        {
            names[i - 1] = PlayerPrefs.GetString("hsp" + i, "");
            scores[i - 1] = PlayerPrefs.GetInt("hs" + i, 0);
        }
        string scoreDisplay = "";
        for (int i = 1; i <= 10; i++)
        {
            string addition = "";
            if (i < 10)
            {
                addition = " " + i + ": ";
            }
            else
            {
                addition = i + ": ";
            }
            if(names[i - 1].Length > 0)
            {

                addition += names[i - 1];
                string score = scores[i - 1] + "";
                while (addition.Length + score.Length < 19)
                {
                    addition += ".";
                }
                addition += score;
            }
            else
            {
                addition += "???????????????";
            }
            addition += " \n\n";
            scoreDisplay += addition;
        }
        display.text = scoreDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
