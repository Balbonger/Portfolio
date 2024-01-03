using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public int gridRows;
    public int gridCols;
    public float offsetX;
    public float offsetY;
    public int[] numberss;
    //public const int gridRows = 2;
    //public const int gridCols = 4;
    //public const float offsetX = 9f;
    //public const float offsetY = 10f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;
    public AudioSource PlusAudio;
    public AudioSource MinusAudio;

    public GameObject YouFailPanel;
    public Text TimerLabel;


    int sceneLoad;
    int nextsceneLoad;

    private void Start()
    {
        Time.timeScale = 1f;
        Vector3 startPos = originalCard.transform.position;
        _score = numberss.Length / 2;

        scoreLabel.text = "Pairs Left: " + _score;

        sceneLoad = SceneManager.GetActiveScene().buildIndex;
        nextsceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        YouWinPanel.SetActive(false);

        //int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};
        int[] numbers = numberss;
        numbers = ShuffleArray(numbers);

        for(int i = 0; i < gridCols; i++)
        {
            for(int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if(i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetX * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private bool stop;
    public void Update()
    {
        if (_score == 0 && !stop)
        {
            YouWinPanel.SetActive(true);
            Time.timeScale = 0f;
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int exclusicecurrentLevel = SceneManager.GetActiveScene().buildIndex - 28;


            if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
            }
            if (exclusicecurrentLevel >= PlayerPrefs.GetInt("PsulevelsUnlocked"))
            {
                PlayerPrefs.SetInt("PsulevelsUnlocked", exclusicecurrentLevel);
                PlayerPrefs.SetInt("TotalReplays", PlayerPrefs.GetInt("TotalReplays") + 1);
            }
            //Debug.Log("Psu Level " + PlayerPrefs.GetInt("PsulevelsUnlocked") + " UNLOCKED");

            BGM.Stop();
            YouWinAudio.Play();

            stop = true;

        }

        if (TimerOn)
        {
            if (timeleft > 0)
            {
                timeleft -= Time.deltaTime;
                updateTimer(timeleft);
                TimerLabel.text = "Time Left: " + Mathf.Floor(timeleft / 60) + ":" + Mathf.Floor(timeleft % 60) + " min";
                //Debug.Log(timeleft);
            }
            else
            {

                PlayerPrefs.SetInt("currentEnergy", PlayerPrefs.GetInt("currentEnergy") - 1);
                TimerLabel.text = "Time's up";
                //Debug.Log("time is up");
                timeleft = 0;
                TimerOn = false;
                YouFailPanel.SetActive(true);

                BGM.Stop();
                MinusAudio.Play();

                Time.timeScale = 0f;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float seconds = Mathf.FloorToInt(currentTime % 60);
    }

    public void istart()
    {
        TimerOn = true;
    }
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score;
    [SerializeField] private TextMesh scoreLabel;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if(_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    public GameObject YouWinPanel;

    public AudioSource YouWinAudio;
    public AudioSource BGM;

    bool TimerOn;
    public float timeleft;

    private IEnumerator CheckMatch()
    {
        
        if(_firstRevealed.id == _secondRevealed.id)
        {
            _score--;
            scoreLabel.text = "Pairs Left: " + _score;
            PlusAudio.Play();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
            //MinusAudio.Play();

        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

    public void Restart()
    {
        if (PlayerPrefs.GetInt("currentEnergy") != 0)
        {
            SceneManager.LoadScene(sceneLoad);
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MapSelector");
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextsceneLoad);
        Time.timeScale = 1f;
    }
}
