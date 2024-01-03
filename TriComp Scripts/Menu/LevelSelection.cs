using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    int levelsUnlocked;
    int MobolevelsUnlocked;
    int ProcessorlevelsUnlocked;
    int RamlevelsUnlocked;
    int GpulevelsUnlocked;
    int InputlevelsUnlocked;
    int OutputlevelsUnlocked;
    int StoragelevelsUnlocked;
    int PsulevelsUnlocked;

    public Button Buildbutton;
    public GameObject llock;
    public GameObject BuildCheck;

    public Button CongratsButton;

    public Button[] MoboButton;
    public GameObject[] Mobolock;
    public GameObject[] MoboCheck;

    public Button[] CpuButton;
    public GameObject[] Cpulock;
    public GameObject[] CpuCheck;


    public Button[] RamButton;
    public GameObject[] Ramlock;
    public GameObject[] RamCheck;


    public Button[] GpuButton;
    public GameObject[] Gpulock;
    public GameObject[] Gpucheck;

    public Button[] InputButton;
    public GameObject[] Inputlock;
    public GameObject[] Inputcheck;

    public Button[] OutputButton;
    public GameObject[] Outputlock;
    public GameObject[] Outputcheck;

    public Button[] StorageButton;
    public GameObject[] Storagelock;
    public GameObject[] Storagecheck;

    public Button[] PsuButton;
    public GameObject[] Psulock;
    public GameObject[] Psucheck;

    public GameObject NoEnergyInfo;

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        Buildbutton.interactable = false;
        llock.SetActive(true);
        BuildCheck.SetActive(false);
        Check();
        MotherboardButtonsManager();
        CpuButtonsManager();
        RamButtonsManager();
        GpuButtonsManager();
        InputButtonsManager();
        OutputButtonsManager();
        StorageButtonsManager();
        PsuButtonsManager();

    }

    public void Check()
    {
        if (PlayerPrefs.GetInt("TotalFin") >= 33)
        {
            CongratsButton.gameObject.SetActive(true);
            BuildCheck.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt("TotalFin") >= 32)
        {
            Buildbutton.interactable = true;
            llock.SetActive(false);
        }
    }

    public void MotherboardButtonsManager()
    {
        MobolevelsUnlocked = PlayerPrefs.GetInt("MobolevelsUnlocked", 1);

        for (int i = 0; i < MoboButton.Length; i++)
        {
            MoboButton[i].interactable = false;
            Mobolock[i].SetActive(true);
            MoboCheck[i].SetActive(false);
        }

        for (int i = 0; i < MobolevelsUnlocked; i++)
        {
            MoboButton[i].interactable = true;
            Mobolock[i].SetActive(false);
        }
        
        for (int i = 0; i < MobolevelsUnlocked; i++)
        {
            MoboCheck[i].SetActive(true);
        }
        
    }

    public void CpuButtonsManager()
    {
        ProcessorlevelsUnlocked = PlayerPrefs.GetInt("ProcessorlevelsUnlocked", 1);

        for (int i = 0; i < CpuButton.Length; i++)
        {
            CpuButton[i].interactable = false;
            Cpulock[i].SetActive(true);
            CpuCheck[i].SetActive(false);

        }

        for (int i = 0; i < ProcessorlevelsUnlocked; i++)
        {
            CpuButton[i].interactable = true;
            Cpulock[i].SetActive(false);
        }

        for (int i = 0; i < ProcessorlevelsUnlocked; i++)
        {
            CpuCheck[i].SetActive(true);
        }
    }

    public void RamButtonsManager()
    {
        RamlevelsUnlocked = PlayerPrefs.GetInt("RamlevelsUnlocked", 1);

        for (int i = 0; i < RamButton.Length; i++)
        {
            RamButton[i].interactable = false;
            Ramlock[i].SetActive(true);
            RamCheck[i].SetActive(false);
        }

        for (int i = 0; i < RamlevelsUnlocked; i++)
        {
            RamButton[i].interactable = true;
            Ramlock[i].SetActive(false);
        }

        for (int i = 0; i < RamlevelsUnlocked; i++)
        {
            RamCheck[i].SetActive(true);
        }
    }

    public void GpuButtonsManager()
    {
        GpulevelsUnlocked = PlayerPrefs.GetInt("GpulevelsUnlocked", 1);

        for (int i = 0; i < GpuButton.Length; i++)
        {
            GpuButton[i].interactable = false;
            Gpulock[i].SetActive(true);
            Gpucheck[i].SetActive(false);
        }

        for (int i = 0; i < GpulevelsUnlocked; i++)
        {
            GpuButton[i].interactable = true;
            Gpulock[i].SetActive(false);
        }

        for (int i = 0; i < GpulevelsUnlocked; i++)
        {
            Gpucheck[i].SetActive(true);
        }
    }

    public void InputButtonsManager()
    {
        InputlevelsUnlocked = PlayerPrefs.GetInt("InputlevelsUnlocked", 1);

        for (int i = 0; i < InputButton.Length; i++)
        {
            InputButton[i].interactable = false;
            Inputlock[i].SetActive(true);
            Inputcheck[i].SetActive(false);
        }

        for (int i = 0; i < InputlevelsUnlocked; i++)
        {
            InputButton[i].interactable = true;
            Inputlock[i].SetActive(false);
        }

        for (int i = 0; i < InputlevelsUnlocked; i++)
        {
            Inputcheck[i].SetActive(true);
        }
    }

    public void OutputButtonsManager()
    {
        OutputlevelsUnlocked = PlayerPrefs.GetInt("OutputlevelsUnlocked", 1);

        for (int i = 0; i < OutputButton.Length; i++)
        {
            OutputButton[i].interactable = false;
            Outputlock[i].SetActive(true);
            Outputcheck[i].SetActive(false);
        }

        for (int i = 0; i < OutputlevelsUnlocked; i++)
        {
            OutputButton[i].interactable = true;
            Outputlock[i].SetActive(false);
        }

        for (int i = 0; i < OutputlevelsUnlocked; i++)
        {
            Outputcheck[i].SetActive(true);
        }
    }

    public void StorageButtonsManager()
    {
        StoragelevelsUnlocked = PlayerPrefs.GetInt("StoragelevelsUnlocked", 1);

        for (int i = 0; i < StorageButton.Length; i++)
        {
            StorageButton[i].interactable = false;
            Storagelock[i].SetActive(true);
            Storagecheck[i].SetActive(false);
        }

        for (int i = 0; i < StoragelevelsUnlocked; i++)
        {
            StorageButton[i].interactable = true;
            Storagelock[i].SetActive(false);
        }

        for (int i = 0; i < StoragelevelsUnlocked; i++)
        {
            Storagecheck[i].SetActive(true);
        }
    }

    public void PsuButtonsManager()
    {
        PsulevelsUnlocked = PlayerPrefs.GetInt("PsulevelsUnlocked", 1);

        for (int i = 0; i < PsuButton.Length; i++)
        {
            PsuButton[i].interactable = false;
            Psulock[i].SetActive(true);
            Psucheck[i].SetActive(false);
        }

        for (int i = 0; i < PsulevelsUnlocked; i++)
        {
            PsuButton[i].interactable = true;
            Psulock[i].SetActive(false);
        }

        for (int i = 0; i < PsulevelsUnlocked; i++)
        {
            Psucheck[i].SetActive(true);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if(PlayerPrefs.GetInt("currentEnergy") != 0)
        {
            SceneManager.LoadScene(levelIndex);
        }

        else
        {
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

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs Deleted");

    }


    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }

    [HideInInspector] public static bool StopIntro;
    public void menuSkip()
    {
        SceneManager.LoadScene("Menu");
        StopIntro = true;
    }

    public GameObject locked;
    public GameObject PcLocked;

    public void notiflock()
    {
        locked.SetActive(true);
        Invoke("Rnotiflock", 3f);
    }

    public void Rnotiflock()
    {
        locked.SetActive(false);
    }

    public void PCnotiflock()
    {
        PcLocked.SetActive(true);
        Invoke("PCRnotiflock", 3f);
    }

    public void PCRnotiflock()
    {
        PcLocked.SetActive(false);
    }

}
