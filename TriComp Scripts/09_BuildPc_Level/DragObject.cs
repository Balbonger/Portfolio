using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class DragObject : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

    }

    private Vector2 mousePosition;

    private float offsetX, offsetY;

    public static bool mouseButtonReleased;

    public bool notmovable;
    public Camera Part1Cam;
    public GameObject FinalCam;
    public GameObject p1c;


    private void OnMouseDown()
    {
            mouseButtonReleased = false;
            offsetX = Part1Cam.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            offsetY = Part1Cam.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;  
    }

    private void OnMouseDrag()
    {
        if (!notmovable)
        {
            mousePosition = Part1Cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - offsetX, mousePosition.y - offsetY);
        }
            
       
    }

    private void OnMouseUp()
    {
        mouseButtonReleased = true;
    }
    DisplayManager displayManager;
    int Pro;
    int Gra;
    int Pow;
    public GameObject Pt1CPanel;
    public GameObject Pt2CPanel;
    public GameObject YouWinPanel;
    public GameObject pausebutton;

    public AudioSource YouWinAudio;
    public AudioSource BGM;
    public AudioSource PlusAudio;
    public GameObject Incorrect;

    public GameObject Square;
    static int counter2 = 0;
    static int counter = 0;
    static int counter3 = 0;

    private void Start()
    {
        displayManager = GameObject.FindGameObjectWithTag("Display").GetComponent<DisplayManager>();
        Pro = displayManager.pp;
        Gra = displayManager.gg;
        Pow = displayManager.power;
        Pow = displayManager.power;

    }

    public void Part1Complete()
    {
        Pt1CPanel.SetActive(true);
    }

    public void SpawnCase()
    {
        GameObject Mcase = Instantiate(Case, new Vector3(-4.06f, -71.9f, -60.5f), transform.rotation);
        Mcase.transform.Rotate(0f, 90f, 0f);
    }

    public void CamresizeOriginal()
    {
        Part1Cam.orthographicSize = 33.85655f;
        Part1Cam.transform.position = new Vector3(-7f, 0f, 8.3f);
    }
    public void Camresize()
    {
        Part1Cam.orthographicSize = 63.84966f;
        Part1Cam.transform.position = new Vector3(-45.6f, -17.6f, 8.3f);
    }

    public int Increment()
    {
        PlusAudio.Play();
        counter = counter + 1;
        return counter;
    }

    public int Increment2()
    {
        PlusAudio.Play();
        counter2 = counter2 + 1;
        return counter2;
    }

    public int Increment3()
    {
        PlusAudio.Play();
        counter3 = counter3 + 1;
        return counter3;
    }

    public void Win()
    {
        pausebutton.SetActive(false);
        FinalCam.SetActive(true);
        p1c.SetActive(false);
        GameObject WMonitor = Instantiate(monitor, new Vector3(22.4f, -85f, 69.9f), transform.rotation);
        WMonitor.transform.Rotate(0f, -90f, 0f);
        WMonitor.gameObject.transform.localScale += new Vector3(3.208041f, 3.208041f, 3.208041f);

        GameObject Wmouse = Instantiate(mouse, new Vector3(-37.7f, -80.2f, -7.8827f), transform.rotation);
        Wmouse.transform.Rotate(0f, 180f, 0f);
        Wmouse.gameObject.transform.localScale += new Vector3(2.280062f, 2.280062f, 2.280062f);

        GameObject Wkeyboard = Instantiate(keyboard, new Vector3(-28.6f, -81.3f, 88.3f), transform.rotation);
        Wkeyboard.transform.Rotate(0f, -90f, 0f);
        Wkeyboard.gameObject.transform.localScale += new Vector3(17.49768f, 17.49768f, 17.49768f);

        GameObject Wspeaker = Instantiate(speaker, new Vector3(34.2f, -53.4f, 191.6f), transform.rotation);
        Wspeaker.transform.Rotate(18.4f, 90f, 0f);
        Wspeaker.gameObject.transform.localScale += new Vector3(-7.260424f, -7.260424f, -7.260424f);

        counter = 0;
        counter2 = 0;
        counter3 = 0;

        YouWinPanel.SetActive(true);

        BGM.Stop();
        YouWinAudio.Play();
        Square.SetActive(false);

        PlayerPrefs.SetInt("TotalFin", 34);
        //Debug.Log("Finished LEVEL " + PlayerPrefs.GetInt("TotalFin"));
    }

    public GameObject i13900k;
    public GameObject i12900;
    public GameObject r7900x;
    public GameObject r5900x;

    public GameObject ThermalPaste;
    public GameObject Therm;

    public GameObject Fan;
    public GameObject Fan1;

    public GameObject Screw;
    public GameObject Screw1;

    public GameObject m2;
    public GameObject m2screw;
    public GameObject RAM4;
    public GameObject RAM5;

    public GameObject Case;

    public GameObject rtx4090;
    public GameObject rtx3050;
    public GameObject rx7900;
    public GameObject rx6500;

    public GameObject Ssd;
    public GameObject Hdd;

    public GameObject P500w;
    public GameObject P1000w;

    public GameObject PsuPowerCord;
    public GameObject Pin24;
    public GameObject Cpupower;
    public GameObject Usbcon;
    public GameObject Audiocon;
    public GameObject Ssdcon;
    public GameObject Hddcon;
    public GameObject FrontP;

    public GameObject monitor;
    public GameObject mouse;
    public GameObject keyboard;
    public GameObject speaker;

    private void OnTriggerStay2D(Collider2D collision)
    {
        string thisGameObjectName;
        string collisionGameObjectName;

        try
        {
            thisGameObjectName = gameObject.name.Substring(0, name.IndexOf("_"));
            collisionGameObjectName = collision.gameObject.name.Substring(0, name.IndexOf("_"));



            if (mouseButtonReleased && thisGameObjectName == "Cpu" && thisGameObjectName == collisionGameObjectName)
            {
                Increment();
                if (Pro == 1)
                {
                    GameObject Part1Cpu = Instantiate(i13900k, new Vector3(25.48f, 11.65f, -76.04f), transform.rotation);
                    Part1Cpu.transform.Rotate(90f, -0f, 0f);
                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);

                }
                else if (Pro == 2)
                {
                    GameObject Part1Cpu = Instantiate(i12900, new Vector3(25.48f, 11.65f, -76.04f), transform.rotation);
                    Part1Cpu.transform.Rotate(90f, -0f, 0f);
                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);

                }
                else if (Pro == 3)
                {
                    GameObject Part1Cpu = Instantiate(r7900x, new Vector3(25.48f, 11.65f, -76.04f), transform.rotation);
                    Part1Cpu.transform.Rotate(90f, -0f, 0f);
                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);

                }
                else if (Pro == 4)
                {
                    GameObject Part1Cpu = Instantiate(r5900x, new Vector3(25.48f, 11.65f, -76.04f), transform.rotation);
                    Part1Cpu.transform.Rotate(90f, -0f, 0f);
                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);

                }
                Therm.SetActive(true);

            }
            else if (mouseButtonReleased && thisGameObjectName == "ThermalPaste" && thisGameObjectName == collisionGameObjectName)
            {
                Increment();

                GameObject Part1TP = Instantiate(ThermalPaste, new Vector3(25.64f, 12.05f, -75.89f), transform.rotation);
                Part1TP.transform.Rotate(90f, -0f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Fan1.SetActive(true);

            }
            else if (mouseButtonReleased && thisGameObjectName == "CCpufan" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part1CF = Instantiate(Fan, new Vector3(21.32f, 0.78f, -78.6f), transform.rotation);
                Part1CF.transform.Rotate(90f, -0f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Screw1.SetActive(true);

                Increment();


            }
            else if (mouseButtonReleased && thisGameObjectName == "Screw" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part1Screw = Instantiate(Screw, new Vector3(21.32f, 0.78f, -78.6f), transform.rotation);
                Part1Screw.transform.Rotate(90f, -0f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment();

                if (counter == 6)
                {
                    Part1Complete();
                }

            }
            else if (mouseButtonReleased && thisGameObjectName == "Ram" && thisGameObjectName == collisionGameObjectName)
            {
                Increment();
                if (Pro == 4)
                {
                    GameObject Part1RAM = Instantiate(RAM4, new Vector3(13.6f, 12.46f, -76.49f), transform.rotation);
                    Part1RAM.transform.Rotate(90f, -0f, 0f);

                    GameObject Part1RAM1 = Instantiate(RAM4, new Vector3(9.42f, 12.46f, -76.49f), transform.rotation);
                    Part1RAM1.transform.Rotate(90f, -0f, 0f);

                    GameObject Part1RAM2 = Instantiate(RAM4, new Vector3(5.39f, 12.46f, -76.49f), transform.rotation);
                    Part1RAM2.transform.Rotate(90f, -0f, 0f);

                    GameObject Part1RAM3 = Instantiate(RAM4, new Vector3(1.54f, 12.46f, -76.49f), transform.rotation);
                    Part1RAM3.transform.Rotate(90f, -0f, 0f);

                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }
                else
                {

                    GameObject Part1RAM = Instantiate(RAM5, new Vector3(13.6f, 12.46f, -76.49f), transform.rotation);
                    Part1RAM.transform.Rotate(90f, -0f, 0f);

                    GameObject Part1RAM1 = Instantiate(RAM5, new Vector3(9.42f, 12.46f, -76.49f), transform.rotation);
                    Part1RAM1.transform.Rotate(90f, -0f, 0f);

                    GameObject Part1RAM2 = Instantiate(RAM5, new Vector3(5.39f, 12.46f, -76.49f), transform.rotation);
                    Part1RAM2.transform.Rotate(90f, -0f, 0f);

                    GameObject Part1RAM3 = Instantiate(RAM5, new Vector3(1.54f, 12.46f, -76.49f), transform.rotation);
                    Part1RAM3.transform.Rotate(90f, -0f, 0f);

                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);

                }
                if (counter == 6)
                {
                    Part1Complete();
                }

            }
            else if (mouseButtonReleased && thisGameObjectName == "M2" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part1m2 = Instantiate(m2, new Vector3(25.71f, -3.98f, -76.99f), transform.rotation);
                Part1m2.transform.Rotate(90f, -0f, 0f);

                GameObject Part1m2screw = Instantiate(m2screw, new Vector3(20.23f, 1.08f, -78.82f), transform.rotation);
                Part1m2screw.transform.Rotate(90f, -0f, 0f);

                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment();

                if (counter == 6)
                {
                    Part1Complete();
                }
            }

            //Part2 Start
            else if (mouseButtonReleased && thisGameObjectName == "Gpu" && thisGameObjectName == collisionGameObjectName)
            {
                if (Gra == 1)
                {
                    GameObject Part2Gpu = Instantiate(rtx4090, new Vector3(14.8f, -22.3f, -66.2f), transform.rotation);
                    Part2Gpu.transform.Rotate(0f, 0f, 0f);
                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);

                }
                else if (Gra == 2)
                {
                    GameObject Part2Gpu = Instantiate(rtx3050, new Vector3(14.98f, -22.94f, -64.6f), transform.rotation);
                    Part2Gpu.transform.Rotate(0f, 0f, 0f);
                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);

                }
                else if (Gra == 3)
                {
                    GameObject Part2Gpu = Instantiate(rx7900, new Vector3(15.97f, -14.2f, -63.74f), transform.rotation);
                    Part2Gpu.transform.Rotate(0f, 90f, 180f);
                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);

                }
                else if (Gra == 4)
                {
                    GameObject Part2Gpu = Instantiate(rx6500, new Vector3(14.59f, -14.27f, -63.74f), transform.rotation);
                    Part2Gpu.transform.Rotate(0f, 90f, 180f);
                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);

                }
                Increment2();
                if (counter2 == 4)
                {
                    Pt2CPanel.SetActive(true);
                }

            }
            else if (mouseButtonReleased && thisGameObjectName == "Ssd" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part2Ssd = Instantiate(Ssd, new Vector3(-28.3f, 8.8f, -82.49f), transform.rotation);
                Part2Ssd.transform.Rotate(-90f, -90f, -270f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment2();
                if (counter2 == 4)
                {
                    Pt2CPanel.SetActive(true);
                }
            }
            else if (mouseButtonReleased && thisGameObjectName == "Hdd" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part2Hdd = Instantiate(Hdd, new Vector3(-0.1f, -53.1f, -56.1f), transform.rotation);
                Part2Hdd.transform.Rotate(0, 180f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment2();
                if (counter2 == 4)
                {
                    Pt2CPanel.SetActive(true);
                }
            }
            else if (mouseButtonReleased && thisGameObjectName == "Psu" && thisGameObjectName == collisionGameObjectName)
            {
                if (Pow == 1)
                {
                    GameObject Part2Psu = Instantiate(P500w, new Vector3(30f, -69.33f, -65.5f), transform.rotation);
                    Part2Psu.transform.Rotate(0, 90f, 0f);

                    GameObject Part2PsuCord = Instantiate(PsuPowerCord, new Vector3(-5.64f, -71.33f, -59.75f), transform.rotation);
                    Part2PsuCord.transform.Rotate(0, 90f, 0f);
                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }
                else if (Pow == 2)
                {
                    GameObject Part2Psu = Instantiate(P1000w, new Vector3(30f, -69.33f, -65.5f), transform.rotation);
                    Part2Psu.transform.Rotate(0, 90f, 0f);

                    GameObject Part2PsuCord = Instantiate(PsuPowerCord, new Vector3(-5.64f, -71.33f, -59.75f), transform.rotation);
                    Part2PsuCord.transform.Rotate(0, 90f, 0f);
                    mouseButtonReleased = false;
                    collision.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }

                Increment2();
                if (counter2 == 4)
                {
                    Pt2CPanel.SetActive(true);
                }
            }

            //Part3 Start
            else if (mouseButtonReleased && thisGameObjectName == "24pin" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part324pin = Instantiate(Pin24, new Vector3(-4.2f, -72.08f, -61.24f), transform.rotation);
                Part324pin.transform.Rotate(0, 90f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment3();
                if (counter3 == 8)
                {
                    Win();
                }
            }
            else if (mouseButtonReleased && thisGameObjectName == "Cpupower" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part3cpupower = Instantiate(Cpupower, new Vector3(-4.17f, -72.02f, -60.23f), transform.rotation);
                Part3cpupower.transform.Rotate(0, 90f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment3();
                if (counter3 == 8)
                {
                    Win();
                }
            }
            else if (mouseButtonReleased && thisGameObjectName == "Usbconnector" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part3usb = Instantiate(Usbcon, new Vector3(-4.23f, -72.1f, -60.27f), transform.rotation);
                Part3usb.transform.Rotate(0, 90f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment3();
                if (counter3 == 8)
                {
                    Win();
                }
            }
            else if (mouseButtonReleased && thisGameObjectName == "Audioconnector" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part3aud = Instantiate(Audiocon, new Vector3(-4.27f, -72.02f, -60.49f), transform.rotation);
                Part3aud.transform.Rotate(0, 90f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment3();
                if (counter3 == 8)
                {
                    Win();
                }
            }
            else if (mouseButtonReleased && thisGameObjectName == "Pcipower" && thisGameObjectName == collisionGameObjectName)
            {
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment3();
                if (counter3 == 8)
                {
                    Win();
                }
            }
            else if (mouseButtonReleased && thisGameObjectName == "Ssatadatapowercable" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part3ssdcon = Instantiate(Ssdcon, new Vector3(-1.44f, -74.51f, -60.6f), transform.rotation);
                Part3ssdcon.transform.Rotate(0, 90f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment3();
                if (counter3 == 8)
                {
                    Win();
                }
            }
            else if (mouseButtonReleased && thisGameObjectName == "Hsatadatapowercable" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part3hddcon = Instantiate(Hddcon, new Vector3(-3.02f, -72.21f, -60.44f), transform.rotation);
                Part3hddcon.transform.Rotate(0, 90f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment3();
                if (counter3 == 8)
                {
                    Win();
                }
            }
            else if (mouseButtonReleased && thisGameObjectName == "Frontpanelconnectors" && thisGameObjectName == collisionGameObjectName)
            {
                GameObject Part3Frontp = Instantiate(FrontP, new Vector3(-4.04f, -72.08f, -60.61f), transform.rotation);
                Part3Frontp.transform.Rotate(0, 90f, 0f);
                mouseButtonReleased = false;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                Increment3();
                if (counter3 == 8)
                {
                    Win();
                }
            }

            else if(mouseButtonReleased && thisGameObjectName != collisionGameObjectName)
            {
                Incorrect.SetActive(true);
                Invoke("hideIncorrect", 2);
            }

        }

        catch (ArgumentOutOfRangeException)
        {
            //none
        }
    }

    public void hideIncorrect()
    {
        Incorrect.SetActive(false);
    }
}
