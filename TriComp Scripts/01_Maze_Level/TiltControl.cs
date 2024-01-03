using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltControl : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationX, rotationZ;
    public float tiltSpeedModifier;

    //tiltspeed standard = 0.03f

    // Update is called once per frame
    void Update()
    {
       if (Input.touchCount > 0)
        {
                touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        rotationX = Quaternion.Euler(0f,
                            0f,
                            -touch.deltaPosition.x * tiltSpeedModifier);
                        transform.rotation = rotationX * transform.rotation;


                    rotationZ = Quaternion.Euler(
                             touch.deltaPosition.y * tiltSpeedModifier,
                            0f,
                            0f);
                        transform.rotation = transform.rotation * rotationZ;
                        break;
                }
        } 
    }
    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void pause()
    {
        Time.timeScale = 0f;
    }

    public void resume()
    {
        Time.timeScale = 1f;
    }
}
