using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform enemy;
    private GameObject Enemy;
    private Vector2 target;




    // Start is called before the first frame update
    void Start()
    {
        try
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
            Enemy = GameObject.FindGameObjectWithTag("Enemy").gameObject;
            target = new Vector2(enemy.position.x, enemy.position.y);
        }
        catch(NullReferenceException)
        {
            //Debug.Log("no enemies present");
        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.position, speed * Time.deltaTime);

        }
        catch (NullReferenceException)
        {
            this.gameObject.SetActive(false);
        }

        try
        {
            if (transform.position == enemy.position)
            {
                Destroy(this.gameObject);
            }

            else
            {
                //nothing
            }
        }
        catch (NullReferenceException)
        {
            //Debug.Log("no enemies on sight");
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
