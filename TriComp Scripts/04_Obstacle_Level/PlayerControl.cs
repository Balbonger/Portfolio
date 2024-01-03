using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    public GameObject FailPanel;
    public static bool iscutscene;
    public GameObject Cutscene;
    [SerializeField] private VideoPlayer videoPlayer;

    public AudioSource YouFailAudio;
    public AudioSource BGM;
    public AudioSource Jump;

    public float jumpForce;
    bool canJump;
    int SceneLoad;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneLoad = SceneManager.GetActiveScene().buildIndex;
        if (iscutscene == true)
        {
            Cutscene.SetActive(false);
            videoPlayer.Stop();
            iscutscene = false;

        }
    }

    bool ok = false;

    public void hehe()
    {
        ok = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canJump && ok)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Jump.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            PlayerPrefs.SetInt("currentEnergy", PlayerPrefs.GetInt("currentEnergy") - 1);
            Time.timeScale = 0;
            FailPanel.SetActive(true);
            BGM.Stop();
            YouFailAudio.Play();
            //SceneManager.LoadScene(SceneLoad);
        }
    }

    public void cutscene()
    {
        SceneManager.LoadScene(SceneLoad);
        iscutscene = true;
    }
}
