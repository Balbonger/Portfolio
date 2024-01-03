using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Net.Sockets;

public class UnlockButtons : MonoBehaviour
{
    public Button Motherboardbutton;
    public Button Processorbutton;
    public Button Rambutton;
    public Button Graphicsbutton;
    public Button Inputbutton;
    public Button Outputbutton;
    public Button Storagebutton;
    public Button Powerbutton;

    public GameObject MotherboardLock;
    public GameObject ProcessorLock;
    public GameObject RamLock;
    public GameObject GraphicsLock;
    public GameObject InputLock;
    public GameObject OutputLock;
    public GameObject StorageLock;
    public GameObject PowerLock;

    public GameObject MoboCheck;
    public GameObject CpuCheck;
    public GameObject RamCheck;
    public GameObject GpuCheck;
    public GameObject InputCheck;
    public GameObject OutputCheck;
    public GameObject StorageCheck;
    public GameObject PowerCheck;

    public GameObject MainPanel;
    public GameObject AlmanacPanel;

    public AudioSource BGM;

    public GameObject Cutscene;
    [SerializeField] private VideoPlayer videoPlayer;
    public GameObject CongratsPanel;

    public GameObject PCButton;
    public GameObject TutorialPanel;
    public GameObject Alock;

    // Start is called before the first frame update
    void Start()
    {
        Motherboardbutton.interactable = false;
        Processorbutton.interactable = false;
        Rambutton.interactable = false;
        Graphicsbutton.interactable=false;
        Inputbutton.interactable=false;
        Outputbutton.interactable=false;
        Storagebutton.interactable=false;
        Powerbutton.interactable=false;

        MotherboardLock.SetActive(true);
        ProcessorLock.SetActive(true);
        RamLock.SetActive(true);
        InputLock.SetActive(true);
        OutputLock.SetActive(true);
        StorageLock.SetActive(true);
        PowerLock.SetActive(true);

        MoboCheck.SetActive(false);
        CpuCheck.SetActive(false);
        RamCheck.SetActive(false);
        GpuCheck.SetActive(false);
        InputCheck.SetActive(false);
        OutputCheck.SetActive(false);
        StorageCheck.SetActive(false);
        PowerCheck.SetActive(false);
        PCButton.SetActive(false);

        videoPlayer.Stop();
        Cutscene.SetActive(false);

        if (PlayerPrefs.GetInt("Intro") == 0)
        {
            TutorialPanel.SetActive(true);
            videoPlayer.Play();
            Cutscene.SetActive(true);
            PlayerPrefs.SetInt("Intro", 1);
        }else if (PlayerPrefs.GetInt("Intro") == 1)
        {
            BGM.Play();
        }
        if (BuildPcCutsceneScript.congratspanel)
        {
            CongratsPanel.SetActive(true);
            BuildPcCutsceneScript.congratspanel = false;
        }

        if((PlayerPrefs.GetInt("TotalFin")>= 34))
        {
            PCButton.SetActive(true);
        }

        Buttonchecker();
    }

    private void Update()
    {
        

        if (MainMenu.isDirectAlmanac)
        {
            directAlmanac();
            MainMenu.isDirectAlmanac = false;
        }
    }

    public void directAlmanac()
    {
        MainPanel.SetActive(false);
        AlmanacPanel.SetActive(true);
        BGM.Play();
    }

    public void Buttonchecker()
    {
    
        if (PlayerPrefs.GetInt("MobolevelsUnlocked") >= 5)
        {
            Motherboardbutton.interactable = true;
            MotherboardLock.SetActive(false);
            MoboCheck.SetActive(true);
        }

        if(PlayerPrefs.GetInt("ProcessorlevelsUnlocked") >= 5)
        {
            Processorbutton.interactable = true;
            ProcessorLock.SetActive(false);
            CpuCheck.SetActive(true);
        }

        if (PlayerPrefs.GetInt("RamlevelsUnlocked") >= 5)
        {
            Rambutton.interactable = true;
            RamLock.SetActive(false);
            RamCheck.SetActive(true);
        }

        if (PlayerPrefs.GetInt("GpulevelsUnlocked") >= 5)
        {
            Graphicsbutton.interactable = true;
            GraphicsLock.SetActive(false);
            GpuCheck.SetActive(true);
        }

        if (PlayerPrefs.GetInt("InputlevelsUnlocked") >= 5)
        {
            Inputbutton.interactable = true;
            InputLock.SetActive(false);
            InputCheck.SetActive(true);
        }

        if (PlayerPrefs.GetInt("OutputlevelsUnlocked") >= 5)
        {
            Outputbutton.interactable = true;
            OutputLock.SetActive(false);
            OutputCheck.SetActive(true);
        }

        if (PlayerPrefs.GetInt("StoragelevelsUnlocked") >= 5)
        {
            Storagebutton.interactable = true;
            StorageLock.SetActive(false);
            StorageCheck.SetActive(true);
        }

        if (PlayerPrefs.GetInt("PsulevelsUnlocked") >= 5)
        {
            Powerbutton.interactable = true;
            PowerLock.SetActive(false);
            PowerCheck.SetActive(true);
        }
    }

    public void Anotiflock()
    {
        Alock.SetActive(true);
        Invoke("RAnotiflock", 3f);
    }

    public void RAnotiflock()
    {
        Alock.SetActive(false);
    }
}
