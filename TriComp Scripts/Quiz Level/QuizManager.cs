using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public TMPro.TextMeshProUGUI QuestionTxt;
    public TMPro.TextMeshProUGUI ScoreTxt;
    public TMPro.TextMeshProUGUI CorrectScoreTxt;

    int TotalQuestions = 0;
    public int Score = 0;

    public GameObject MainPanel;
    public GameObject ScorePanel;
    public GameObject CorrectScorePanel;

    public Image[] Monitor;
    public Sprite Correct;
    public Sprite Wrong;

    public AudioSource YouWinAudio;
    public AudioSource YouFailAudio;
    public AudioSource BGM;
    public AudioSource PlusAudio;
    public AudioSource MinusAudio;

    

    private void Start()
    {
        TotalQuestions = QnA.Count;
        ScorePanel.SetActive(false);
        CorrectScorePanel.SetActive(false);
        generateQuestion();

        //Debug.Log("Build " + SceneManager.GetActiveScene().buildIndex);

        TotalFinishedLevel();
    }
    
    void GameOver()
    {      
        if(Score >= TotalQuestions-1)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;

            if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
            }

            if(currentLevel == 5)
            {
                if (currentLevel >= PlayerPrefs.GetInt("MobolevelsUnlocked"))
                {
                    PlayerPrefs.SetInt("MobolevelsUnlocked", 5);
                    //Debug.Log("Mobo Level: " + PlayerPrefs.GetInt("MobolevelsUnlocked"));
                }
            }else if (currentLevel == 9)
            {
                if (currentLevel >= PlayerPrefs.GetInt("ProcessorlevelsUnlocked"))
                {
                    PlayerPrefs.SetInt("ProcessorlevelsUnlocked", 5);
                    //Debug.Log("Cpu Level: " + PlayerPrefs.GetInt("ProcessorlevelsUnlocked"));
                }
            }
            else if (currentLevel == 13)
            {
                if (currentLevel >= PlayerPrefs.GetInt("RamlevelsUnlocked"))
                {
                    PlayerPrefs.SetInt("RamlevelsUnlocked", 5);
                    //Debug.Log("Gpu Level: " + PlayerPrefs.GetInt("RamlevelsUnlocked"));
                }
            }else if (currentLevel == 17)
            {
                if (currentLevel >= PlayerPrefs.GetInt("GpulevelsUnlocked"))
                {
                    PlayerPrefs.SetInt("GpulevelsUnlocked", 5);
                    //Debug.Log("Gpu Level: " + PlayerPrefs.GetInt("GpulevelsUnlocked"));
                }
            }else if (currentLevel == 21)
            {
                if (currentLevel >= PlayerPrefs.GetInt("InputlevelsUnlocked"))
                {
                    PlayerPrefs.SetInt("InputlevelsUnlocked", 5);
                    //Debug.Log("Input Level: " + PlayerPrefs.GetInt("InputlevelsUnlocked"));
                }
            }else if (currentLevel == 25)
            {
                if (currentLevel >= PlayerPrefs.GetInt("OutputlevelsUnlocked"))
                {
                    PlayerPrefs.SetInt("OutputlevelsUnlocked", 5);
                    //Debug.Log("Output Level: " + PlayerPrefs.GetInt("OutputlevelsUnlocked"));
                }
            }else if (currentLevel == 29)
            {
                if (currentLevel >= PlayerPrefs.GetInt("StoragelevelsUnlocked"))
                {
                    PlayerPrefs.SetInt("StoragelevelsUnlocked", 5);
                    //Debug.Log("Storage Level: " + PlayerPrefs.GetInt("StoragelevelsUnlocked"));
                }
            }else if (currentLevel == 33)
            {
                if (currentLevel >= PlayerPrefs.GetInt("PsulevelsUnlocked"))
                {
                    PlayerPrefs.SetInt("PsulevelsUnlocked", 5);
                    //Debug.Log("Psu Level: " + PlayerPrefs.GetInt("PsulevelsUnlocked"));
                }
            }

            BGM.Stop();
            YouWinAudio.Play();
            MainPanel.SetActive(false);
            CorrectScorePanel.SetActive(true);
            CorrectScoreTxt.text = Score + " / " + TotalQuestions;
            TotalFinishedLevel();
        }
        else
        {
            PlayerPrefs.SetInt("currentEnergy", PlayerPrefs.GetInt("currentEnergy") - 1);
            BGM.Stop();
            YouFailAudio.Play();
            MainPanel.SetActive(false);
            CorrectScorePanel.SetActive(false);
            ScorePanel.SetActive(true);
            ScoreTxt.text = Score + " / " + TotalQuestions;
        }
    }

    int checker = 0;
    public void correct()
    {
        PlusAudio.Play();
        Score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
        Monitor[checker].sprite = Correct;
        checker++;
    }

    public void wrong()
    {
        MinusAudio.Play();
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
        Monitor[checker].sprite = Wrong;
        checker++;
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;

            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            //Debug.Log("Out of Questions");
            GameOver();
        }

    }

    public void TotalFinishedLevel()
    {
        int mm = 0;
        int cc = 0;
        int rr = 0;
        int gg = 0;
        int ii = 0;
        int oo = 0;
        int ss = 0;
        int pp = 0;

        if (PlayerPrefs.GetInt("MobolevelsUnlocked") >= 1)
        {
            mm = PlayerPrefs.GetInt("MobolevelsUnlocked") - 1;
            //Debug.Log("Total finished mm: " + mm);
        }
        if (PlayerPrefs.GetInt("ProcessorlevelsUnlocked") >= 1)
        {
            cc = PlayerPrefs.GetInt("ProcessorlevelsUnlocked") - 1;
            //Debug.Log("Total finished cc: " + cc);
        }
        if (PlayerPrefs.GetInt("RamlevelsUnlocked") >= 1)
        {
            rr = PlayerPrefs.GetInt("RamlevelsUnlocked") - 1;
            //Debug.Log("Total finished rr: " + rr);
        }
        if (PlayerPrefs.GetInt("GpulevelsUnlocked") >= 1)
        {
            gg = PlayerPrefs.GetInt("GpulevelsUnlocked") - 1;
            //Debug.Log("Total finished gg: " + gg);
        }
        if (PlayerPrefs.GetInt("InputlevelsUnlocked") >= 1)
        {
            ii = PlayerPrefs.GetInt("InputlevelsUnlocked") - 1;
            //Debug.Log("Total finished ii: " + ii);
        }
        if (PlayerPrefs.GetInt("OutputlevelsUnlocked") >= 1)
        {
            oo = PlayerPrefs.GetInt("OutputlevelsUnlocked") - 1;
            //Debug.Log("Total finished oo: " + oo);
        }
        if (PlayerPrefs.GetInt("StoragelevelsUnlocked") >= 1)
        {
            ss = PlayerPrefs.GetInt("StoragelevelsUnlocked") - 1;
            //Debug.Log("Total finished ss: " + ss);
        }
        if (PlayerPrefs.GetInt("PsulevelsUnlocked") >= 1)
        {
            pp = PlayerPrefs.GetInt("PsulevelsUnlocked") - 1;
            //Debug.Log("Total finished pp: " + pp);
        }

        int tt = mm + cc + rr + gg + ii + oo + ss + pp;

        PlayerPrefs.SetInt("TotalFin",tt);
        //Debug.Log("Finished Levels: " + (tt));

    }
}
