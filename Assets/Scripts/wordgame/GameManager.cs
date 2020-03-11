using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Main class for word game
 */
public class GameManager : MonoBehaviour
{
    public string[] questions;
    public string[] answers;
    public string[] options = { "あ", "い", "う", "え", "お" };
    public TextMeshPro question;
    private int qIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        question.text = questions[qIndex];   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool SubmitAnswer(string text)
    {
        Debug.Log("Submitted: " + text);
        if (text == answers[qIndex])
        {
            if (qIndex == questions.Length-1)
            {
                question.text = "win";
                return true;
            }

            question.text = questions[++qIndex];
            return true;
        }

        return false;
    }

    // Request some text to be applied to a ball
    public string RequestText()
    {
        return options[Random.Range(0, options.Length)];
    }
}
