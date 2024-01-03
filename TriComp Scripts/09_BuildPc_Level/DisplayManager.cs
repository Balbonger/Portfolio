using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayManager : MonoBehaviour
{
    public Text DiagnosticText;
    public GameObject z790;
    public GameObject b660;
    public GameObject x670;
    public GameObject b550;

    public GameObject i13900k;
    public GameObject i12900;
    public GameObject r7900x;
    public GameObject r5900x;

    public GameObject ddr4;
    public GameObject ddr5;

    public GameObject rtx4090;
    public GameObject rtx3050;
    public GameObject rx7900xtx;
    public GameObject rx6500xt;

    public GameObject psu500w;
    public GameObject psu1000w;

    public Transform SpawnMobo;
    public Transform SpawnCpu;
    public Transform SpawnRam;
    public Transform SpawnGpu;
    public Transform SpawnPsu;

    GameObject M;

    public GameObject Motherboard;
    public GameObject Processor;
    public GameObject RAM;
    public GameObject GraphicsCard;
    public GameObject PowerSupply;

    public GameObject GuidePanel;
    public GameObject CorrectPanel;
    public GameObject WrongPanel;

    public Button mobo;
    public Button cpu;
    public Button ram;
    public Button gpu;
    public Button psu;

    int SceneLoad;
    int board;
    bool gddr5;
    int proce;
    bool rando;
    int graph;
    public int power;

    public int pp;
    public int gg;

    public AudioSource YouFailAudio;
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        SceneLoad = SceneManager.GetActiveScene().buildIndex;
        GuidePanel.SetActive(true);
        WrongPanel.SetActive(false);
        CorrectPanel.SetActive(false);
        
        if (PlayerPrefs.GetInt("TotalFin") == 34 )
        {
            Randomizer();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneLoad);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MapSelector");
    }

    public void ConfirmComponents()
    {
        if (board != 0 && board == proce && gddr5 == rando && graph <= power)
        {
            CorrectPanel.SetActive(true);
            WrongPanel.SetActive(false);
            //Debug.Log("correct");
        }
        else
        {
            WrongPanel.SetActive(true);
            CorrectPanel.SetActive(false);
            //Debug.Log("Wrong");

            if (board == 0)
            {
                DiagnosticText.text = DiagnosticText.text + "\n" + "* No Motherboard were selected.";
            }
            if (pp == 0)
            {
                DiagnosticText.text = DiagnosticText.text + "\n" + "* No Processor were selected.";
            }
            if (RAM == null)
            {
                DiagnosticText.text = DiagnosticText.text + "\n" + "* No RAM were selected.";
            }
            if (gg == 0)
            {
                DiagnosticText.text = DiagnosticText.text + "\n" + "* No Graphics card were selected.";
            }
            if (power == 0)
            {
                DiagnosticText.text = DiagnosticText.text + "\n" + "* No Power Supply were selected.";
            }
            if (board != proce && Motherboard != null && Processor != null)
            {
                DiagnosticText.text = DiagnosticText.text + "\n"+ "* Motherboard and processor are incompactible with one another.";
            }
            if(gddr5 != rando && Motherboard != null & RAM !=null)
            {
                DiagnosticText.text = DiagnosticText.text + "\n" + "* Motherboard and RAM are incompactible with one another.";
            }
            if(graph > power && GraphicsCard != null & PowerSupply != null)
            {
                DiagnosticText.text = DiagnosticText.text + "\n" + "* GPU isn't compatible with the Power supply.";
            }

            BGM.Stop();
            YouFailAudio.Play();
        }
    }

    public void Randomizer()
    {
        int component = Random.Range(0, 3);

        if (component == 0) //Motherboard Randomizer
        {
            int moboard = Random.Range(0, 3);

            if (moboard == 0)
            {
                ChooseZ790();
            } else if (moboard == 1)
            {
                ChooseB660();
            } else if (moboard == 2)
            {
                ChooseX670();
            } else if (moboard == 3)
            {
                ChooseB550();
            }

        } else if (component == 1) //Processor Randomizer
        {
            int prossor = Random.Range(0, 3);

            if (prossor == 0)
            {
                Choose13900k();
            } else if (prossor == 1)
            {
                Choosei12900();
            } else if (prossor == 2)
            {
                Chooser7900x();
            } else if (prossor == 3)
            {
                Chooser5900x();
            }

        } else if (component == 2) //Graphics Card Randomizer
        {
            int Gracard = Random.Range(0, 3);

            if (Gracard == 0)
            {
                Choosertx4090();
            } else if (Gracard == 1)
            {
                Choosertx3050();
            } else if (Gracard == 2)
            {
                Chooserx7900xtx();
            } else if (Gracard == 3)
            {
                Chooserx6500xt();
            }

        } else if (component == 3) //Power Supply Randomizer
        {
            int Poply = Random.Range(0, 1);

            if (Poply == 0)
            {
                Choosepsu500w();
            } else if (Poply == 1)
            {
                Choosepsu1000w();
            }
        }
    }

    //SpawnMobo
    public void ChooseZ790()
    {
        GameObject Mz790 = Instantiate(z790, SpawnMobo.position, z790.transform.rotation);
        Mz790.transform.Rotate(90f, 0f, 180f);
        mobo.enabled = false;
        board = 1;
        gddr5 = true;

        Motherboard = Mz790;
        M = z790;
    }
    public void ChooseB660()
    {
        GameObject Mb660 = Instantiate(b660, SpawnMobo.position, b660.transform.rotation);
        Mb660.transform.Rotate(90f, 0f, 180f);
        mobo.enabled = false;
        board = 1;
        gddr5 = true;

        Motherboard = Mb660;
        M = b660;

    }
    public void ChooseX670()
    {
        GameObject Mx670 = Instantiate(x670, SpawnMobo.position, x670.transform.rotation);
        Mx670.transform.Rotate(90f, 0f, 180f);
        mobo.enabled = false;
        board = 2;
        gddr5 = true;

        Motherboard = Mx670;
        M = x670;
    }
    public void ChooseB550()
    {
        GameObject Mb550 = Instantiate(b550, SpawnMobo.position, b550.transform.rotation);
        Mb550.transform.Rotate(90f, 0f, 180f);
        mobo.enabled = false;
        board = 3;
        gddr5 = false;

        Motherboard = Mb550;
        M = b550;
    }

    //Spawn CPU
    public void Choose13900k()
    {
        GameObject Mi13900k = Instantiate(i13900k, SpawnCpu.position, i13900k.transform.rotation);
        Mi13900k.transform.Rotate(90f, 0f, 180f);
        cpu.enabled = false;
        proce = 1;
        pp = 1;

        Processor = Mi13900k;
       
    }
    public void Choosei12900()
    {
        GameObject Mi12900 = Instantiate(i12900, SpawnCpu.position, i12900.transform.rotation);
        Mi12900.transform.Rotate(90f, 0f, 180f);
        cpu.enabled = false;
        proce = 1;
        pp = 2;

        Processor = Mi12900;
        
    }
    public void Chooser7900x()
    {
        GameObject Mr7900x = Instantiate(r7900x, SpawnCpu.position, r7900x.transform.rotation);
        Mr7900x.transform.Rotate(90f, 0f, 180f);
        cpu.enabled = false;
        proce = 2;
        pp = 3;

        Processor = Mr7900x;
        
    }
    public void Chooser5900x()
    {
        GameObject Mr5900x = Instantiate(r5900x, SpawnCpu.position, r5900x.transform.rotation);
        Mr5900x.transform.Rotate(90f, 0f, 180f);
        cpu.enabled = false;
        proce = 3;
        pp = 4;

        Processor = Mr5900x;
        
    }

    //Spawn RAM
    public void ChooseGddr4()
    {
        GameObject Mddr4 = Instantiate(ddr4, SpawnRam.position, ddr4.transform.rotation);
        Mddr4.transform.Rotate(120f, 0f, 90f);
        ram.enabled = false;
        rando = false;

        RAM = Mddr4;
    }
    public void ChooseGddr5()
    {
        GameObject Mddr5 = Instantiate(ddr5, SpawnRam.position, ddr5.transform.rotation);
        Mddr5.transform.Rotate(120f, 0f, 90f);
        ram.enabled = false;
        rando = true;

        RAM = Mddr5;
    }

    //Spawn GPU
    public void Choosertx4090()
    {
        GameObject Mrtx4090 = Instantiate(rtx4090, SpawnGpu.position, rtx4090.transform.rotation);
        Mrtx4090.transform.Rotate(180f, -90f, 90f);
        gpu.enabled = false;
        graph = 2;
        gg = 1;

        GraphicsCard = Mrtx4090;
    }
    public void Choosertx3050()
    {
        GameObject Mrtx3050 = Instantiate(rtx3050, SpawnGpu.position, rtx3050.transform.rotation);
        Mrtx3050.transform.Rotate(180f, -90f, 90f);
        gpu.enabled = false;
        graph = 1;
        gg = 2;

        GraphicsCard = Mrtx3050;
    }
    public void Chooserx7900xtx()
    {
        GameObject Mrx7900xtx = Instantiate(rx7900xtx, SpawnGpu.position, rx7900xtx.transform.rotation);
        Mrx7900xtx.transform.Rotate(90f, -90f, 90f);
        gpu.enabled = false;
        graph = 2;
        gg = 3;

        GraphicsCard = Mrx7900xtx;
    }
    public void Chooserx6500xt()
    {
        GameObject Mrx6500xt = Instantiate(rx6500xt, SpawnGpu.position, rx6500xt.transform.rotation);
        Mrx6500xt.transform.Rotate(90f, -90f, 90f);
        gpu.enabled = false;
        graph = 1;
        gg = 4;

        GraphicsCard = Mrx6500xt;
    }

    //Spawn PSU
    public void Choosepsu500w()
    {
        GameObject Mpsu500w = Instantiate(psu500w, SpawnPsu.position, psu500w.transform.rotation);
        psu.enabled = false;
        power = 1;

        PowerSupply = Mpsu500w;
    }
    public void Choosepsu1000w()
    {
        GameObject Mpsu1000w = Instantiate(psu1000w, SpawnPsu.position, psu1000w.transform.rotation);
        psu.enabled = false;
        power = 2;

        PowerSupply = Mpsu1000w;
    }

    public void ReadyPart1()
    {
        GameObject Part1Mobo = Instantiate(M, new Vector3(20.4f, 1f, -78.7f), transform.rotation);
        Part1Mobo.transform.Rotate(90f, -0f, 0f);
    }

    public void destroy()
    {
        Destroy(Motherboard);
        Destroy(Processor);
        Destroy(RAM);
        Destroy(GraphicsCard);
        Destroy(PowerSupply);
    }

}   
