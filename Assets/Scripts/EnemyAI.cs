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

    public float speed = 200f;
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

    private void OnTriggerEnter(Collider other)
    {
        foreach(GameObject plr in players)
        {
            if(other == plr)
            {
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
            randPos.transform.position = new Vector3(Random.Range(0, 20), Random.Range(0, 20), 0);
            target = randPos.transform;

            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        transform.position = new Vector3(transform.position.x + dir.x, transform.position.y+ dir.y, 0);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }
}

