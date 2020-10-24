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

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        StartCoroutine(NextLevel());
    }

    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3);
        level++;
        wavesLeft = (int)Mathf.Ceil(level/2f);

        while (wavesLeft > 0)
        {
            int iMaximum = (level % 2 == 0 && wavesLeft == 1 ? 3 : 2);
            for (int i = 1; i <= iMaximum; i++)
                Instantiate(poacher, spawnPositions[Random.Range(0,4)],Quaternion.identity);
            wavesLeft--;
            yield return new WaitForSeconds(3);
        }
    }

    public void LevelOver()
    {
        if (wavesLeft == 0 && !GameObject.Find("Poacher"))
            NextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
