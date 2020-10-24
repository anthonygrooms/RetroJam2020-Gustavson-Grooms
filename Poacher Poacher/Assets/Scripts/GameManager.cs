using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject poacher;
    private int wavesLeft;
    private int level;
    public static readonly Vector3[] spawnPositions = new Vector3[]
        {new Vector3(-3,-3,0), new Vector3(-1,-3,0),
         new Vector3(1,-3,0), new Vector3(3,-3,0)};
    private int poachersEscaped;

    // Start is called before the first frame update
    void Start()
    {
        poachersEscaped = 0;
        level = 0;
        StartCoroutine(NextLevel());
    }

    public IEnumerator NextLevel()
    {
        print("loading...");
        yield return new WaitForSeconds(3);
        level++;

        if (level % 5 == 0)
        {
            poachersEscaped = 0;
        }

        wavesLeft = (int)Mathf.Ceil(level/2f);

        while (wavesLeft > 0)
        {
            int iMaximum = (level % 2 == 0 && wavesLeft == 1 ? 3 : 2);
            List<int> randomNumbers = new List<int>();
            for (int i = 1; i <= iMaximum; i++)
            {
                int randomNumber = Random.Range(0, 4);
                while (randomNumbers.Contains(randomNumber))
                    randomNumber = Random.Range(0, 4);
                randomNumbers.Add(randomNumber);
                Instantiate(poacher, spawnPositions[randomNumber], Quaternion.identity);
            }
            wavesLeft--;
            yield return new WaitForSeconds(3);
        }
    }

    public void LevelOver()
    {
        print(wavesLeft);
        print(!GameObject.Find("Poacher(Clone)"));
        if (wavesLeft == 0 && !GameObject.Find("Poacher(Clone)"))
            StartCoroutine(NextLevel());
    }

    public void PoacherEscaped()
    {
        poachersEscaped++;
        if (poachersEscaped >= 3)
        {
            GameOver();
        }
    }

    private void GameOver()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
