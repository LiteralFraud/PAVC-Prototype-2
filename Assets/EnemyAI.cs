using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
    //Following
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;
    public float followingspeed;
    bool facingRight = false;

    //For pathfinding script
    public float nextWayPointDistance = 3f;
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;
    

    //For seeker script
    Seeker seeker;
    Rigidbody2D rb;

    //Animation
    public Animator anim;
    Vector2 movement;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("UpdatePath", 0f, .5f);

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            //Starting Path
            seeker.StartPath(rb.position, player.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
            return;
        }
    }

    private void FixedUpdate()
    {
       
    }
    void Update()
    {
      

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            anim.SetBool("isrunning", true);
            FollowingAI();
        }

        else if (Vector2.Distance(transform.position, player.position) < retreatDistance  )
        {
            anim.SetBool("isrunning", true);
            followingspeed = 2 * followingspeed;
            followingspeed = -followingspeed;
            FollowingAI();
            followingspeed = followingspeed / 2;
            followingspeed = -followingspeed;
            
        }
        else if(Vector2.Distance(transform.position, player.position) <= stoppingDistance)
        {

            anim.SetBool("isrunning", false);
        }

    }

    void FollowingAI()
    {

        if (path == null)
        {
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * followingspeed * Time.deltaTime;

        rb.AddForce(force);
       

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
       
        if (facingRight == false && (player.position.x-rb.position.x)>0)
        {
                Flip();
        }
        else if (facingRight == true && (player.position.x - rb.position.x) < 0)
        {
                Flip();
        }
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    void Attacking ()
    {
        float dash = 600;
        Vector2 direction = (Vector2)player.position - rb.position;
        Vector2 force = direction * dash * Time.deltaTime;
        rb.AddForce(force);
    }

}
