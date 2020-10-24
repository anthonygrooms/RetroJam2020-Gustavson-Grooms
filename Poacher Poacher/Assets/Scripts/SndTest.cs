using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SndTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<Audio_Manager>().Play("BeginLevel");
        }
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<Audio_Manager>().Play("Hit");
        }
        if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<Audio_Manager>().Play("Miss");
        }
    }
}
