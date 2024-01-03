using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragTower : MonoBehaviour
{
    public GameObject Tower;
    public GameObject StartButton;
    public int TowerToPlace;
    public GameObject[] Buttons;

    static int towertoPlace;

    public void Start()
    {
        towertoPlace = TowerToPlace;
    }
    public int Decrement()
    {
        towertoPlace = towertoPlace - 1;
        return towertoPlace;
    }

    //Level 5

    public void placePosition1()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-4.12f, 3.62f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if(towertoPlace == 0)
        {
            StartButton.SetActive(true);

            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void placePosition2()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(2.27f, 1f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void placePosition3()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(7.18f, -0.62f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    //Level 6

    public void place2Position1()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-9.81f, 5.57f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    public void place2Position2()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-9.81f, 2.4f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    public void place2Position3()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-2.71f, 1.79f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    public void place2Position4()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-2.71f, -0.97f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    public void place2Position5()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(5.26f, 4.89f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    public void place2Position6()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(5.26f, 2.15f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    public void place2Position7()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(5.26f, -3.48f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    public void place2Position8()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(12.01f, 1.27f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    public void place2Position9()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(12.01f, -1.68f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    //Level 7

    public void place3Position1()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-7.02999926f, 2.20000052f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }

    public void place3Position2()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-1.03000021f, 2.15000033f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position3()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(4.96999979f, 2.18000007f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position4()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(14.1999998f, 2.15000033f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position5()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-10.1299992f, -3.43999982f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position6()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-4.13000011f, -3.49000001f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position7()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(1.87f, -3.46000004f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position8()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(8.31000042f, -2.5999999f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position9()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-14.8800001f, -5.98999977f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position10()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-14.8800001f, -10.8699999f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position11()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-7.15999985f, -8.61999989f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position12()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(-1.15999985f, -8.59000015f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position13()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(8.31000042f, -7.48000002f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position14()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(16.0900002f, -5.98999977f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
    public void place3Position15()
    {
        GameObject tower1 = Instantiate(Tower, new Vector3(16.0900002f, -10.8699999f, -2.16f), Quaternion.identity);
        Decrement();
        gameObject.SetActive(false);
        if (towertoPlace == 0)
        {
            StartButton.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(false);
            }
        }
    }
}
