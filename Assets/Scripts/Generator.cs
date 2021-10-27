using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public float progress = 0;
    public float maxProgress;
    public bool done = false;
    public int playersNear = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (progress >= maxProgress)
        {
            done = true;
        }
        else if (playersNear > 0)
        {
            progress += 1 * Time.deltaTime;
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playersNear += 1;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playersNear -= 1;
        }
    }
    public bool isComplete()
    {
        return done;
    }
}
