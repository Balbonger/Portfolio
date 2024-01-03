using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragPuzzle : MonoBehaviour
{
    public GameObject SelectedPiece;

    [SerializeField] private TextMesh TimerLabel;
    public float timeleft;
    public bool TimerOn = false;
    public GameObject YouFailPanel;

    public AudioSource YouFailAudio;
    public AudioSource BGM;

    int SceneLoad;

    // Start is called before the first frame update
    void Start()
    {
        unpause();
        SceneLoad = SceneManager.GetActiveScene().buildIndex;
        YouFailPanel.SetActive(false);
        TimerLabel.text = "Time Left: " + Mathf.Floor(timeleft / 60) + ":" + Mathf.Floor(timeleft % 60) + " min";

    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            try
            {
                if (hit.transform.CompareTag("Puzzle"))
                {
                    if (!hit.transform.GetComponent<pieceScript>().InRightPosition)
                    {
                        SelectedPiece = hit.transform.gameObject;
                        SelectedPiece.GetComponent<pieceScript>().Selected = true;
                    }
                }
            }

            catch (NullReferenceException)
            {
                //its fine.....
            }
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            try
            {
                SelectedPiece.GetComponent<pieceScript>().Selected = false;
            }

            catch (UnassignedReferenceException)
            {
                //its fine....
            }
            catch (NullReferenceException)
            {
                //its fine....
            }
            SelectedPiece = null;
        }

        if (SelectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
        }

        if (TimerOn)
        {
            if (timeleft > 0)
            {
                timeleft -= Time.deltaTime;

                updateTimer(timeleft);
                TimerLabel.text = "Time Left: " + Mathf.Floor(timeleft/60) + ":" + Mathf.Floor(timeleft%60) + " min";

                if(timeleft <= 31)
                {
                    TimerLabel.color = Color.red;
                    
                }
            }
            else
            {
                //Debug.Log("time is up");

                PlayerPrefs.SetInt("currentEnergy", PlayerPrefs.GetInt("currentEnergy") - 1);
                timeleft = 0;
                TimerOn = false;
                YouFailPanel.SetActive(true);

                BGM.Stop();
                YouFailAudio.Play();
                TimerLabel.text = "Time's up";

                Time.timeScale = 0f;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float seconds = Mathf.FloorToInt(currentTime % 60);
    }

    public void start()
    {
        TimerOn = true;
    }

    public void Restart()
    {
       
        if (PlayerPrefs.GetInt("currentEnergy") != 0)
        {
            SceneManager.LoadScene(SceneLoad);
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MapSelector");
        }
    }

    public void unpause()
    {
        Time.timeScale = 1f;
    }

    public void nextlevel()
    {
        SceneManager.LoadScene(SceneLoad+1);
        Time.timeScale = 1f;
    }
}
