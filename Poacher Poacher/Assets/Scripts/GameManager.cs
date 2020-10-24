using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject poacher;
    private int wavesLeft;
    public int level;
    public static readonly Vector3[] spawnPositions = new Vector3[]
        {new Vector3(-3,-3,-1), new Vector3(-1,-3,-1),
         new Vector3(1,-3,-1), new Vector3(3,-3,-1)};
    private int poachersEscaped;
    private bool gameOver;
    public Text scoreText, levelText, escapedText, gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
        levelText.text = "LEVEL 1";
        escapedText.text = "ESCAPED 3";
        gameOverText.enabled = false;
        poachersEscaped = 0;
        level = 0;
        gameOver = false;
        StartCoroutine(NextLevel());
    }

    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3);
        level++;
        levelText.text = "LEVEL " + level;

        if (level % 5 == 0)
        {
            poachersEscaped = 0;
            escapedText.text = "ESCAPED 3";
        }

        wavesLeft = (int)Mathf.Ceil(level/2f);

        while (wavesLeft > 0)
        {
            int iMaximum = (level % 2 == 0 && wavesLeft == 1 ? 3 : 2);
            List<int> randomNumbers = new List<int>();
            for (int i = 1; i <= iMaximum && !gameOver; i++)
            {
                int randomNumber = Random.Range(0, 4);
                while (randomNumbers.Contains(randomNumber))
                    randomNumber = Random.Range(0, 4);
                randomNumbers.Add(randomNumber);
                Instantiate(poacher, spawnPositions[randomNumber], Quaternion.identity);
            }
            wavesLeft--;
            yield return new WaitForSeconds(2);
        }
    }

    public void LevelOver()
    {
        if (wavesLeft == 0 && !GameObject.Find("Poacher(Clone)") && !gameOver)
            StartCoroutine(NextLevel());
    }

    public void PoacherEscaped()
    {
        poachersEscaped++;
        if (poachersEscaped > 3)
            poachersEscaped = 3;
        escapedText.text = "ESCAPED "+(3-poachersEscaped);
        if (poachersEscaped >= 3 && !gameOver)
        {
            gameOver = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        Poacher[] poachers = FindObjectsOfType<Poacher>();
        for (int i = 0; i < poachers.Length; i++)
        {
            poachers[i].rB.velocity = Vector2.zero;
            poachers[i].dead = true;
            poachers[i].sR.sprite = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
