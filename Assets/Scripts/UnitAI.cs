using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class UnitAI : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldOfView;
    //FieldOfView fieldOfView;

    public GameObject target;
    public GameObject prevTarget;
    public Animator animator;

    public float speed;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        prevTarget = target;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine(WalkCoroutine());

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {

        if (seeker.IsDone() || prevTarget != target)
        {
            seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
            Destroy(prevTarget.gameObject);
            prevTarget = target;
        }
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

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            rb.velocity = Vector2.zero;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        transform.position = new Vector3(transform.position.x + dir.x * speed, transform.position.y + dir.y * speed, 0);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360; //maybe needed? added because tutorial had lol

        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);

        fieldOfView.SetAimDirection(angle - 0); //dir.normalized);

        fieldOfView.SetOrigin(rb.position);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }

    IEnumerator WalkCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("walkingA");
    }
}
