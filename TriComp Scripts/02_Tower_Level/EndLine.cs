using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    public GameObject YouFailPanel;
    public AudioSource YouFailAudio;
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collider)
    {
        GameObject hitobject = collider.gameObject;

        if (hitobject.tag == "Enemy")
        {
            PlayerPrefs.SetInt("currentEnergy", PlayerPrefs.GetInt("currentEnergy") - 1);
            Time.timeScale = 0f;
            Debug.Log("Mission Failed");
            YouFailPanel.SetActive(true);

            BGM.Stop();
            YouFailAudio.Play();
        }
    }
}
