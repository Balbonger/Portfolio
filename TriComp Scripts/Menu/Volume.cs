using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM.volume = PlayerPrefs.GetFloat("MusicVolume");
        AudioListener.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
