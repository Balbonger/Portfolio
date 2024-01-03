using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaintainAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource BGM;
    void Start()
    {
        if (PlayerPrefs.GetInt("TotalReplays") == 0 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
