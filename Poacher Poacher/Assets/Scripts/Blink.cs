using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    private Text text;
    private float time; // Determines when the object should blink next

    public bool restartOnEnable; // Determines if the counter is reset when the script is enabled
    public float speed;

    void OnEnable()
    {
        if (restartOnEnable)
        {
            time = 0;
            if (text != null)
                text.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>(); // Get a reference to this object's text component
    }

    // Update is called once per frame
    void Update()
    {
        // Blink the screen every speed seconds if the speed is not 0
        if (speed > 0)
        {
            time += Time.deltaTime;
            if (time >= speed)
            {
                time = 0;
                text.enabled = !text.enabled;
            }
        }
        else
        {
            time = 0;
        }
    }
}
