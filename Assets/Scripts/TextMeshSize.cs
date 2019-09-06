using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshSize : MonoBehaviour
{
    public int size;
    private TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        string currentText = tmp.text;
        string result = "";
        string[] lineArray = currentText.Split('\n');
        foreach (string line in lineArray) {
            if (line == " ") {
                continue;
            }
            string[] wordArray = line.Split(' ');
            int wordCountOnLine = 0;
            foreach (string word in wordArray) {
                if (word == "") {
                    continue;
                }
                result += word + " ";
                wordCountOnLine += 1;
                if (wordCountOnLine > size) {
                  result += "\n";
                  wordCountOnLine = 0;
                }
            }
        }
        tmp.text = result;
    }
}
