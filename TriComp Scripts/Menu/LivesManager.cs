using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LivesManager : MonoBehaviour
{
    [SerializeField] Text energyText;
    [SerializeField] Text timerText;
    [SerializeField] Slider energyBar;

    private int maxEnergy = 5;
    private int currentEnergy;
    private int restoreDuration = 5;

    private DateTime nextEnergyTime;
    private DateTime lastEnergyTime;
    private bool isRestoring = false;

    public GameObject NoEnergyInfo;


    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("currentEnergy"))
        {
            PlayerPrefs.SetInt("currentEnergy", 5);
            Load();
            StartCoroutine(RestoreEnergy());
        }

        else
        {
            Load();
            StartCoroutine(RestoreEnergy());
        }
    }

    public void UseEnergy()
    {
        if(currentEnergy >= 1)
        {
            currentEnergy--;
            UpdateEnergy();

            if(isRestoring == false)
            {
                if(currentEnergy + 1 == maxEnergy)
                {
                    nextEnergyTime = AddDuration(DateTime.Now, restoreDuration);
                }

                StartCoroutine(RestoreEnergy());
            }
        }

        else
        {
            Debug.Log("Insufficient Energy!!");
            show();
        }
    }

    public void show()
    {
        NoEnergyInfo.SetActive(true);
        Invoke("hide", 3f);
    }

    public void hide()
    {
        NoEnergyInfo.SetActive(false);
    }

    private IEnumerator RestoreEnergy()
    {
        UpdateEnergyTimer();
        isRestoring = true;

        while(currentEnergy < maxEnergy)
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime nextDateTime = nextEnergyTime;
            bool isEnergyAdding = false;

            while(currentDateTime > nextDateTime)
            {
                if(currentEnergy < maxEnergy)
                {
                    isEnergyAdding = true;
                    currentEnergy++;
                    UpdateEnergy();
                    DateTime timeToAdd = lastEnergyTime > nextDateTime ? lastEnergyTime : nextDateTime;
                    nextDateTime = AddDuration(timeToAdd, restoreDuration);
                }
                else
                {
                    break;
                }
            }

            if(isEnergyAdding == true)
            {
                lastEnergyTime = DateTime.Now;
                nextEnergyTime = nextDateTime;
            }

            UpdateEnergyTimer();
            UpdateEnergy();
            Save();

            yield return null;
        }

        isRestoring = false;
    }
    private DateTime AddDuration(DateTime dateTime, int duration)
    {
        return dateTime.AddMinutes(duration);
    }

    private void UpdateEnergyTimer()
    {
        if (currentEnergy >= maxEnergy)
        {
            timerText.text = "Full";
            return;
        }

        TimeSpan time = nextEnergyTime - DateTime.Now;
        string timeValue = String.Format("{0:D2}:{1:D1}", time.Minutes, time.Seconds);
        timerText.text = timeValue;
    }

    private void UpdateEnergy()
    {
        energyText.text = currentEnergy.ToString() + " / " + maxEnergy.ToString();

        energyBar.maxValue = maxEnergy;
        energyBar.value = currentEnergy;
    }

    private DateTime StringToDate(string datetime)
    {
        if (String.IsNullOrEmpty(datetime))
        {
            return DateTime.Now;
        }

        else
        {
            return DateTime.Parse(datetime);
        }
    }

    public void Load()
    {
        currentEnergy = PlayerPrefs.GetInt("currentEnergy");
        nextEnergyTime = StringToDate(PlayerPrefs.GetString("nextEnergyTime"));
        lastEnergyTime = StringToDate(PlayerPrefs.GetString("lastEnergyTime"));
    }

    public void Save()
    {
        PlayerPrefs.SetInt("currentEnergy", currentEnergy);
        PlayerPrefs.SetString("nextEnergyTime", nextEnergyTime.ToString());
        PlayerPrefs.SetString("lastEnergyTime", lastEnergyTime.ToString());

    }
}
