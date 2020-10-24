using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Poacher : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 destinationPos;
    private Vector3 velocity;
    public Rigidbody2D rB;
    public SpriteRenderer[] frames = new SpriteRenderer[2];
    public SpriteRenderer sR;
    public SpriteRenderer deathSpriteRenderer;
    private GameManager gM;
    public bool dead;
    private int scaleStep;
    public SpriteRenderer[] escapedFrames = new SpriteRenderer[4];
    
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        gM = FindObjectOfType<GameManager>();
        dead = false;
        scaleStep = 0;

        startPos = transform.position;
        switch (startPos.x){
            case -3:
                destinationPos = new Vector3(-4, 1.39f, -1);
                break;
            case -1:
                destinationPos = new Vector3(-1, 1.39f, -1);
                break;
            case 1:
                destinationPos = new Vector3(1, 1.39f, -1);
                break;
            default:
                destinationPos = new Vector3(4, 1.39f, -1);
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

    public IEnumerator EscapedAnimation()
    {
        int frame = 0;
        while (frame < escapedFrames.Length)
        {
            sR.sprite = escapedFrames[frame].sprite;
            frame++;
            yield return new WaitForSeconds(.1f);
        }
        StartCoroutine(DestroySelf(0));
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !dead)
        {
            dead = true;
            rB.velocity = Vector2.zero;
            gM.scoreText.text =  (Convert.ToInt32(gM.scoreText.text) + 1).ToString();
            sR.sprite = deathSpriteRenderer.sprite;
            name = "DeadPoacher";
            gM.LevelOver();
            StartCoroutine(DestroySelf(3));
        }
    }

    void Update()
    {
        if (transform.position.y >= -1f && scaleStep==0)
        {
            scaleStep++;
            transform.localScale = new Vector3(transform.localScale.x * .75f, transform.localScale.y * .75f, transform.localScale.z);
        }
        if (transform.position.y >= 0f && scaleStep == 1)
        {
            scaleStep++;
            transform.localScale = new Vector3(transform.localScale.x * .75f, transform.localScale.y * .75f, transform.localScale.z);
        }
        if (transform.position.y >= 1f && scaleStep == 2)
        {
            scaleStep++;
            transform.localScale = new Vector3(transform.localScale.x * .75f, transform.localScale.y * .75f, transform.localScale.z);
        }
        if (transform.position.y >= 1.25f && scaleStep == 3)
        {
            scaleStep++;
            transform.localScale = new Vector3(transform.localScale.x * .75f, transform.localScale.y * .75f, transform.localScale.z);
        }

        if (transform.position.y >= destinationPos.y && name.Equals("Poacher(Clone)"))
        {
            dead = true;
            rB.velocity = Vector2.zero;
            gM.PoacherEscaped();
            name = "EscapedPoacher";
            gM.LevelOver();
            StartCoroutine(EscapedAnimation());
        }
    }

    public IEnumerator DestroySelf(int t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
