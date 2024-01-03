using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;

    private Transform enemies;



    // Start is called before the first frame update
    void Start()
    {
            enemies = GameObject.FindGameObjectWithTag("Enemy").transform;
        



        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
            float distance = Vector3.Distance(enemies.transform.position, gameObject.transform.position);

            if (timeBtwShots <= 0)
            {
                if(distance <= 6)
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                    //Debug.Log("distance = " + distance);
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }


    }
}
