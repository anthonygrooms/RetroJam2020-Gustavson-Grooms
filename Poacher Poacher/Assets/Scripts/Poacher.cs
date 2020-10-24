using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poacher : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 destinationPos;
    private Vector3 velocity;
    private Rigidbody2D rB;
    public SpriteRenderer[] frames = new SpriteRenderer[2];
    private SpriteRenderer sP;
    
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        sP = GetComponent<SpriteRenderer>();

        startPos = transform.position;
        switch (startPos.x){
            case -3:
                velocity = new Vector3(-4, 2, 0) - transform.position;
                break;
            case -1:
                velocity = new Vector3(-1, 2, 0) - transform.position;
                break;
            case 1:
                velocity = new Vector3(1, 2, 0) - transform.position;
                break;
            default:
                velocity = new Vector3(4, 2, 0) - transform.position;
                break;
        }
        rB.velocity = velocity.normalized;
        StartCoroutine(PlayAnimation());
    }

    public IEnumerator PlayAnimation()
    {
        int frame = -1;
        while (true)
        {
            frame++;
            if (frame > frames.Length-1)
                frame = 0;
            sP.sprite = frames[frame].sprite;
            yield return new WaitForSeconds(.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
