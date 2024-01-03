using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QManager : MonoBehaviour
{
    public static QManager instance;

    [SerializeField] private GameObject gameover;
    [SerializeField] private GameObject correct;

    [SerializeField] private QuizDataScriptTable questionData;

    [SerializeField] private Image questionImage;

    [SerializeField] private WordData[] answerWordArray;
    [SerializeField] private WordData[] optionsWordArray;

    private char[] charArray = new char[18];
    private int currentAnswerIndex = 0;
    private bool correcAnswer = true;
    private List<int> selectedWordIndex;
    private int currentQuestionIndex = 0;
    private GameStatus gameStatus = GameStatus.Playing;
    private string answerWord;

    public AudioSource YouWinAudio;
    public AudioSource BGM;
    public AudioSource PlusAudio;
    public AudioSource MinusAudio;
    
    int sceneLoad;
    int nextsceneLoad;

    private void Awake()
    {
        if(instance == null) instance = this;
        else
            Destroy(gameObject);

        selectedWordIndex = new List<int>();
    }


    private void Start()
    {
        sceneLoad = SceneManager.GetActiveScene().buildIndex;
        nextsceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SetQuestion();
        Time.timeScale = 1f;
    }
    private void SetQuestion()
    {
        currentAnswerIndex = 0;
        selectedWordIndex.Clear();

        questionImage.sprite = questionData.questions[currentQuestionIndex].QuesionImage;
        answerWord = questionData.questions[currentQuestionIndex].answer;

        ResetQuestion();

        for (int i = 0; i < answerWord.Length; i++)
        {
            charArray[i] = char.ToUpper(answerWord[i]);
        }

        for(int i = answerWord.Length; i < optionsWordArray.Length; i++)
        {
            charArray[i] = (char)UnityEngine.Random.Range(65, 91);
        }

        charArray = ShuffleList.ShuffleListItems<char>(charArray.ToList()).ToArray();

        for(int i = 0; i < optionsWordArray.Length; i++)
        {
            optionsWordArray[i].SetChar(charArray[i]);
        }

        currentQuestionIndex++;
        gameStatus = GameStatus.Playing;
    }

    public void SelectedOption(WordData wordData)
    {
        if(gameStatus == GameStatus.Next|| currentAnswerIndex >= answerWord.Length) return;

        selectedWordIndex.Add(wordData.transform.GetSiblingIndex());
        answerWordArray[currentAnswerIndex].SetChar(wordData.charValue);
        wordData.gameObject.SetActive(false);
        currentAnswerIndex++;

        if(currentAnswerIndex >= answerWord.Length)
        {
            correcAnswer = true;

            for(int i = 0; i < answerWord.Length; i++)
            {
                if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordArray[i].charValue))
                {
                    correcAnswer = false;
                    break;
                }
            }

            if (correcAnswer)
            {
                //Debug.Log("correct answer");

                Invoke("Correct", 0f);

                gameStatus = GameStatus.Next;

                CancelInvoke("Hide");

                if(currentQuestionIndex < questionData.questions.Count)
                {
                    Invoke("SetQuestion", 1f);
                }
                else
                {
                    gameover.SetActive(true);

                    int currentLevel = SceneManager.GetActiveScene().buildIndex;
                    int exclusicecurrentLevel = SceneManager.GetActiveScene().buildIndex - 16;

                    BGM.Stop();
                    YouWinAudio.Play();

                    if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
                    {
                        PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
                    }

                    if (exclusicecurrentLevel >= PlayerPrefs.GetInt("InputlevelsUnlocked"))
                    {
                        PlayerPrefs.SetInt("InputlevelsUnlocked", exclusicecurrentLevel);
                        PlayerPrefs.SetInt("TotalReplays", PlayerPrefs.GetInt("TotalReplays") + 1);
                    }

                    //Debug.Log("Input Level " + PlayerPrefs.GetInt("InputlevelsUnlocked") + " UNLOCKED");
                    //Debug.Log("Total Replays = " + PlayerPrefs.GetInt("TotalReplays"));
                }
            }
            else if (!correcAnswer)
            {
                //Debug.Log("Wrong Answer");
                MinusAudio.Play();

            }
        }
    }

    private void ResetQuestion()
    {
        for(int i = 0; i < answerWordArray.Length; i++)
        {
            answerWordArray[i].gameObject.SetActive(true);
            answerWordArray[i].SetChar('_');
        }
        
        for(int i = answerWord.Length; i < answerWordArray.Length; i++)
        {
            answerWordArray[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < optionsWordArray.Length; i++)
        {
            optionsWordArray[i].gameObject.SetActive(true);
        }
    }

    public void ResetLastWord()
    {
        if(selectedWordIndex.Count > 0)
        {
            int index = selectedWordIndex[selectedWordIndex.Count - 1];
            optionsWordArray[index].gameObject.SetActive(true);

            selectedWordIndex.RemoveAt(selectedWordIndex.Count - 1);
            currentAnswerIndex--;
            answerWordArray[currentAnswerIndex].SetChar('_');
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(sceneLoad);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextsceneLoad);

    }

    public void Correct()
    {
        PlusAudio.Play();
        correct.SetActive(true );
        Invoke("Hide", 1f);
    }
    public void Hide()
    {
        correct.SetActive(false);
        CancelInvoke("Correct");
    }
}

[System.Serializable]
public class QuestionData
{
    public Sprite QuesionImage;
    public string answer;
}

public enum GameStatus
{
    Playing,
    Next
}
