using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Poacher : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 destinationPos;
    private Vector3 velocity;
    private Rigidbody2D rB;
    public SpriteRenderer[] frames = new SpriteRenderer[2];
    public SpriteRenderer sR;
    public SpriteRenderer deathSpriteRenderer;
    private GameManager gM;
    public bool dead;
    
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        gM = FindObjectOfType<GameManager>();
        dead = false;

        startPos = transform.position;
        switch (startPos.x){
            case -3:
                destinationPos = new Vector3(-4, 1.5f, -1);
                break;
            case -1:
                destinationPos = new Vector3(-1, 1.5f, -1);
                break;
            case 1:
                destinationPos = new Vector3(1, 1.5f, -1);
                break;
            default:
                destinationPos = new Vector3(4, 1.5f, -1);
                break;
        }
        velocity = destinationPos - transform.position;
        rB.velocity = velocity.normalized * UnityEngine.Random.Range(1,gM.level/2f);
        StartCoroutine(PlayAnimation());
    }

    public IEnumerator PlayAnimation()
    {
        int frame = -1;
        while (!dead)
        {
            frame++;
            if (frame > frames.Length-1)
                frame = 0;
            sR.sprite = frames[frame].sprite;
            yield return new WaitForSeconds(.2f);
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !dead)
        {
            
            sR.sprite = deathSpriteRenderer.sprite;
            name = "DeadPoacher";
            gM.LevelOver();
            StartCoroutine(DestroySelf(3));
        }
    }

    void Update()
    {
        if (transform.position.y >= destinationPos.y && name.Equals("Poacher(Clone)"))
        {
            gM.PoacherEscaped();
            name = "EscapedPoacher";
            gM.LevelOver();
            StartCoroutine(DestroySelf(0));
        }
    }

    public IEnumerator DestroySelf(int t)
    {
        dead = true;
        rB.velocity = Vector2.zero;
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
