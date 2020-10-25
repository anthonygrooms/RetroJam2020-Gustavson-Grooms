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
    public bool gameOver;
    public Text scoreText, levelText, escapedText, gameOverText;
    public SpriteRenderer lion;
    public Sprite[] lionSprites;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Audio_Manager>().Play("Roar");
        FindObjectOfType<Audio_Manager>().Play("Begin Level");
        scoreText.text = "0";
        levelText.text = "LEVEL 1";
        escapedText.text = "ESCAPED 3";
        gameOverText.enabled = true;
        gameOverText.text = "READY...";
        poachersEscaped = 0;
        level = 0;
        gameOver = false;
        StartCoroutine(IntroAnimation());
    }

    public IEnumerator IntroAnimation()
    {
        lion.GetComponent<Rigidbody2D>().velocity = new Vector2(-3.4285f, 0);
        int frame = 0;
        while (frame < 10)
        {
            lion.sprite = lionSprites[frame % 2];
            frame++;
            yield return new WaitForSeconds(.2f);
        }
        FindObjectOfType<Audio_Manager>().Play("Roar");
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
        yield return new WaitForSeconds(.5f);
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
