using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Catcher : MonoBehaviour
{
    private Vector3 mOffset;
    public GameObject GameM;

    private void Start()
    {
            
    }

    private void OnMouseDown()
    {
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = 0;
        mousePoint.y = gameObject.transform.position.y;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Binary")
        {
            GameM.GetComponent<GameM>().Increment();
            Destroy(other.gameObject);
            Debug.Log("Hit: " + GameM.GetComponent<GameM>().count);

        }

        else if(other.gameObject.tag == "Nonbinary")
        {
            GameM.GetComponent<GameM>().MinusLife();
            Destroy(other.gameObject);
            Debug.Log("Taken Damege: " + GameM.GetComponent<GameM>().Lives);

        }
    }

    
}
