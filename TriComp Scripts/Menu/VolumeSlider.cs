using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] public VideoPlayer video;
    public Toggle mute;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("mute", (mute ? 0 : 1));

        if(PlayerPrefs.GetInt("mute")== 1)
        {
            mute.isOn = true;

        }
        else if(PlayerPrefs.GetInt("mute") == 0)
        {
            mute.isOn = false;
        }

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        video.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
        //Debug.Log(PlayerPrefs.GetFloat("MusicVolume"));

        Save();
    }

    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
        //Debug.Log(PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("mute", 1);
        }
        else 
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("mute", 0);

        }
    }
}