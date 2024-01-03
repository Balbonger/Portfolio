using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Waypoint Wpoints;
    private int waypointIndex;

    public int health;
    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
        Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, Wpoints.waypoints[waypointIndex].position, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, Wpoints.waypoints[waypointIndex].position) < 0.1f)
        {

            if (waypointIndex < Wpoints.waypoints.Length - 1)
            {
                waypointIndex++;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        if (health <= 0)
        {
            try
            {
                this.gameObject.SetActive(false);

            }
            catch (MissingReferenceException){
                //
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health = health - 10;
            //Debug.Log("hit: " + health);
        }
    }

    public void move()
    {
        moving = true;
    }
    public void stopmove()
    {
        moving = false;
    }
}
