using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject background0;
    public GameObject background1;


    void Start()
    {
        background0.gameObject.SetActive(true);
        background1.gameObject.SetActive(false);
        Checker();

    }

    public void Checker()
    {
        int sum = PlayerPrefs.GetInt("TotalFin");
        if(sum >= 33)
        {
            background1.gameObject.SetActive(true);
            background0.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
