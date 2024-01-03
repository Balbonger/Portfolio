using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBarrier : MonoBehaviour
{
    public GameObject YoufailPanel;
    public AudioSource YouFailAudio;
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        YoufailPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("currentEnergy", PlayerPrefs.GetInt("currentEnergy") - 1);
            YoufailPanel.SetActive(true);
            //Debug.Log("LEVEL NOT CLEARED, FINISH LEVEL " + PlayerPrefs.GetInt("levelsUnlocked") + (" FIRST!"));

            BGM.Stop();
            YouFailAudio.Play();
            other.gameObject.SetActive(false);

        }
    }
}
