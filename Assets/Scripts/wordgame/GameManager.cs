using TMPro;
using UnityEngine;

[System.Serializable]
public class SentencePart
{
    public bool isQuestion;
    public string content;
    public string answer;
}

[System.Serializable]
public class GameSentence
{
    public SentencePart[] parts;

    public string getQuestion(int partIndex)
    {
        string output = "";
        for (int i = 0; i < parts.Length; i++)
        {
            SentencePart part = parts[i];
            if (i == partIndex)
            {
                // highlight the current question
                output += "(" + part.content + ")";
            } else if (i < partIndex && part.isQuestion)
            {
                output += part.answer;
            } else
            {
                output += part.content;
            }
        }

        return output;
    }
}

[System.Serializable]
public class GameConfig
{
    public GameSentence[] sentences;
    public string kanjiList;
}

/*
 * Main class for word game
 */
public class GameManager : MonoBehaviour
{
    public TextMeshPro question;
    public TextAsset configJson;
    private GameConfig config;
    private bool win = false;
    private int sentIndex = 0;
    private int partIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        config = JsonUtility.FromJson<GameConfig>(configJson.text);
        next();
        question.text = config.sentences[sentIndex].getQuestion(partIndex);
    }

    public bool next()
    {
        // scroll and find the next question
        do
        {
            partIndex++;
            if (partIndex == config.sentences[sentIndex].parts.Length)
            {
                partIndex = 0;
                sentIndex++;

                if (sentIndex == config.sentences.Length)
                {
                    return false;
                }
            }
        }
        while (!config.sentences[sentIndex].parts[partIndex].isQuestion);
        return true;
     }


    public bool SubmitAnswer(string text)
    {
        if (win) return false;
        GameSentence sent = config.sentences[sentIndex];
        SentencePart part = sent.parts[partIndex];

        Debug.Log("Submitted: " + text);
        if (text == part.answer)
        {
            if (!next())
            {
                question.text = "win";
                win = true;
                return true;
            }

            question.text = config.sentences[sentIndex].getQuestion(partIndex);
            return true;
        }

        return false;
    }

    // Request some text to be applied to a ball
    public string RequestText()
    {
        if (win) return "";

        if (Random.Range(0, 6) == 0)
        {
            return config.sentences[sentIndex].parts[partIndex].answer;
        }

        return config.kanjiList[Random.Range(0, config.kanjiList.Length)].ToString();
    }
}
