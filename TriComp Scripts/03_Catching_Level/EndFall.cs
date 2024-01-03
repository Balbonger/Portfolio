using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFall : MonoBehaviour
{
    public GameObject GameM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Binary")
        {
            GameM.GetComponent<GameM>().MinusLife();
            Destroy(other.gameObject);
            Debug.Log("Taken Damege: " + GameM.GetComponent<GameM>().Lives);
        }

        else if(other.gameObject.tag == "Nonbinary")
        {
            Destroy(other.gameObject);
        }
    }
}
