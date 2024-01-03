using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject finish;
    public Transform spawnPoint;
    public Transform FinishSpawnPoint;

    public GameObject finishPanel;

    public int duration;
    int score = 0;
    public bool moving;

    IEnumerator ObstacleCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        ObstacleCoroutine = SpawnObstacles();  
        finishPanel.SetActive(false);
        Physics.gravity = new Vector3(0, -70f, 0);
        //Debug.Log(Physics.gravity);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator SpawnObstacles()
    {
        while (duration >= score)
        {
            float waitTime = Random.Range(0.8f, 2f);
            yield return new WaitForSeconds(waitTime);

            if (moving)
            {
                Instantiate(obstacle, spawnPoint.position, Quaternion.identity);
            }
        }

        yield return new WaitForSeconds(0.5f);
        finish.SetActive(true);
    }
    void Score()
    {
        score++;
        //Debug.Log("Score " + score);
    }

    public void GameStart()
    {
        moving = true;
        Time.timeScale = 1f;
        InvokeRepeating("Score", 2f, 1f);
        StartCoroutine(ObstacleCoroutine);
        
      
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;

    }

}
