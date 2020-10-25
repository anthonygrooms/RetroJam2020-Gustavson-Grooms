using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject poacher;
    private int wavesLeft;
    public int level;
    public static readonly Vector3[] spawnPositions = new Vector3[]
        {new Vector3(-3,-3,-1), new Vector3(-1,-3,-1),
         new Vector3(1,-3,-1), new Vector3(3,-3,-1)};
    private int poachersEscaped;
    public bool gameOver;
    public Text scoreText, levelText, escapedText, gameOverText;
    public SpriteRenderer lion;
    public Sprite[] lionSprites;
    public Text enterName;
    public Text enterNameDirection;
    public int enterNameTimer;
    private int enterNameStep;
    private string before, after;
    private char middle;
    private char[] characters = new char[]
        {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
    private int charIndex;
    private int newScoreIndex;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Audio_Manager>().Play("Begin Level");
        scoreText.text = "0";
        levelText.text = "LEVEL 1";
        escapedText.text = "ESCAPED 3";
        gameOverText.enabled = true;
        gameOverText.text = "READY...";
        poachersEscaped = 0;
        level = 0;
        gameOver = false;
        enterNameStep = 0;
        before = "";
        middle = 'A';
        after = "";
        enterName.enabled = false;
        enterNameDirection.enabled = false;
        charIndex = 0;
    StartCoroutine(IntroAnimation());
    }

    public IEnumerator IntroAnimation()
    {
        lion.GetComponent<Rigidbody2D>().velocity = new Vector2(-3.2f, 0);
        int frame = 0;
        while (frame < 10)
        {
            lion.sprite = lionSprites[frame % 2];
            frame++;
            yield return new WaitForSeconds(.2f);
        }
        lion.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        lion.sprite = lionSprites[2];
        lion.transform.position = new Vector3(lion.transform.position.x, lion.transform.position.y + .402f, -3f);
        yield return new WaitForSeconds(1);
        FindObjectOfType<Audio_Manager>().Play("Roar");
        lion.sprite = lionSprites[3];
        lion.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
        yield return new WaitForSeconds(1);
        lion.transform.position = new Vector3(lion.transform.position.x, lion.transform.position.y, 2f);
        lion.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3);
        yield return new WaitForSeconds(1);
        lion.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameOverText.text = "GAME OVER";
        gameOverText.enabled = false;
        StartCoroutine(NextLevel(0));
        Destroy(lion.gameObject);
    }

    public IEnumerator NextLevel(int t)
    {
        yield return new WaitForSeconds(.4f);
        FindObjectOfType<Audio_Manager>().Play("Begin Level");
        yield return new WaitForSeconds(t);
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
            StartCoroutine(NextLevel(3));
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
        StartCoroutine(Return());
        gameOverText.enabled = true;
        Poacher[] poachers = FindObjectsOfType<Poacher>();
        for (int i = 0; i < poachers.Length; i++)
        {
            poachers[i].rB.velocity = Vector2.zero;
            poachers[i].dead = true;
            poachers[i].sR.sprite = null;
        }
    }

    private IEnumerator Return()
    {
        FindObjectOfType<Audio_Manager>().Play("Game Over");
        yield return new WaitForSeconds(5);

        bool newHigh = NewHighScore();

        if (newHigh)
        {
            enterNameStep = 1;
            enterName.enabled = true;
            enterNameDirection.enabled = true;
            before = "";
            after = "AAAAAAA";
            StartCoroutine(Timer());
            gameOverText.enabled = false;
        }
        else
        {
            StartCoroutine(GoToHighScoreScreen(0));
        }
    }

    private IEnumerator GoToHighScoreScreen(int t)
    {
        for (int i = 1; i <= 10; i++)
        {
            print(i+" "+PlayerPrefs.GetInt("hs" + (i), 0));
            print(i+" "+PlayerPrefs.GetString("hsp" + (i), ""));
        }
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene("HighScores");
    }

    private bool NewHighScore()
    {
        int k = 1, s;
        for (int i = 10; i >= 1; i--)
        {
            s = PlayerPrefs.GetInt("hs" + i, 0);
            if (int.Parse(scoreText.text) <= s)
            {
                k = i+1;
                break;
            }
        }
        bool newHigh = k >= 1 && k <= 10;
        if (newHigh)
        {
            for (int i = 10; i > k; i--)
            {
                PlayerPrefs.SetInt("hs" + i, PlayerPrefs.GetInt("hs" + (i - 1), 0));
                PlayerPrefs.SetString("hsp" + i, PlayerPrefs.GetString("hsp" + (i - 1), ""));
            }
            PlayerPrefs.SetInt("hs" + k, int.Parse(scoreText.text));
            newScoreIndex = k;
        }
        return newHigh;
    }

    private IEnumerator Timer()
    {
        while (enterNameTimer > 0 && enterNameStep < 9)
        {
            enterNameDirection.text = "ENTER NAME: " + enterNameTimer + "\nUSE LEFT, RIGHT, ENTER";
            yield return new WaitForSeconds(1);
            enterNameTimer--;
        }
        if (enterNameTimer == 0)
        {
            enterNameDirection.text = "ENTER NAME: " + enterNameTimer + "\nUSE LEFT, RIGHT, ENTER";
            enterNameStep = 9;
            PlayerPrefs.SetString("hsp" + newScoreIndex, before + middle + after);
            StartCoroutine(GoToHighScoreScreen(3));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enterNameStep > 0 && enterNameStep < 9)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                enterNameStep++;
                if (enterNameStep < 9)
                    before += middle;
                else
                {
                    PlayerPrefs.SetString("hsp" + newScoreIndex, before + middle);
                    StartCoroutine(GoToHighScoreScreen(3));
                }
                if (enterNameStep < 8)
                    after = after.Substring(1);
                else
                    after = "";
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                charIndex--;
                if (charIndex < 0)
                    charIndex = characters.Length - 1;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                charIndex++;
                if (charIndex > characters.Length - 1)
                    charIndex = 0;
            }
            middle = characters[charIndex];
            enterName.text = before + middle + after;
        }
    }
}
