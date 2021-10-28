using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public Transform prevTarget;
    public List<GameObject> players;
    public GameObject randPos;

    public float speed;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool playerClose = false;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        prevTarget = target;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();


        randPos = new GameObject();
        randPos.transform.position = new Vector3(Random.Range(0, 20), Random.Range(0, 20), 0);
        target = randPos.transform;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        
        foreach(GameObject plr in players)
        {
            if(other.gameObject.tag == "Player")
            {
                Debug.Log("Enemy: Player Detected");
                target = plr.transform;
            }
        }
    }

    void UpdatePath()
    {

        if (seeker.IsDone() || prevTarget != target)
            seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            rb.velocity = Vector2.zero;
            randPos.transform.position = new Vector3(Random.Range(-49, 49), Random.Range(-34, 38), 0);
            target = randPos.transform;

            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        transform.position = new Vector3(transform.position.x + dir.x * speed, transform.position.y+ dir.y * speed, 0);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360; //maybe needed? added because tutorial had lol

        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }
}

