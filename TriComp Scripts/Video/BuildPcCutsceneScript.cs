using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class BuildPcCutsceneScript : MonoBehaviour
{
    public int duration;
    public GameObject EndPlayer;
    [SerializeField] private VideoPlayer EndCutscene;
    [HideInInspector] public static bool congratspanel;
    // Start is called before the first frame update
    void Start()
    {
        EndCutscene.Stop();
        EndPlayer.SetActive(false);
    }

    public void Farewell()
    {
        EndPlayer.SetActive(true);
        EndCutscene.Play();
        StartCoroutine(Sandali());

    }

    IEnumerator Sandali()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
        congratspanel = true;
        SceneManager.LoadScene("MapSelector");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
