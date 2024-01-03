using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestionData", menuName = "QuestionsData", order = 1)]
public class QuizDataScriptTable : ScriptableObject
{
    public List<QuestionData> questions;
}
