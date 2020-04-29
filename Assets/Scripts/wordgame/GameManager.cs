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
    public TextMeshPro scoreMesh;
    public TextAsset configJson;
    private GameConfig config;
    private string[] options;
    private bool win = false;
    private int sentIndex = 0;
    private int partIndex = 0;
    private int score = 0;
    private int optsize = 6;
    public AudioClip correct;
    public AudioClip incorrect;
    public AudioSource audioSource;
    private int lastAns = 0;


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

        // generate new kanji choices for RequestText
        options = new string[optsize];
        for (int i = 0; i < optsize - 1; i++)
        {
            options[i] = config.kanjiList[Random.Range(0, config.kanjiList.Length)].ToString();
        }
        options[optsize - 1] = config.sentences[sentIndex].parts[partIndex].answer;
        return true;
     }

    public void ChangeScore(int change)
    {
        if (change > 0)
        {
            audioSource.PlayOneShot(correct, 1f);
        } else
        {
            audioSource.PlayOneShot(incorrect, 0.6f);
        }

        score += change;
        scoreMesh.text = "Score: " + score;
    }


    public bool SubmitAnswer(string text)
    {
        if (win) return false;
        GameSentence sent = config.sentences[sentIndex];
        SentencePart part = sent.parts[partIndex];

        Debug.Log("Submitted: " + text);
        if (text == part.answer)
        {
            ChangeScore(10);
            if (!next())
            {
                question.text = "Final Score: " + score;
                win = true;
                return true;
            }

            question.text = config.sentences[sentIndex].getQuestion(partIndex);
            return true;
        }

        ChangeScore(-3);
        return false;
    }

    // Request some text to be applied to a ball
    public string RequestText()
    {
        if (win) return "";

        int index = Random.Range(0, optsize);

        // make it unlikely to have the correct kanji twice in a row
        if (lastAns == 0 && index == optsize - 1) index = Random.Range(0, optsize);

        if (index != optsize - 1) lastAns++;
        else lastAns = 0;

        // guarantee that the correct kanji is shown at least once per 'optsize'
        if (lastAns > optsize)
        {
            index = optsize - 1;
            lastAns = 0;
        }


        return options[index];
    }
}
