using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInputDisplay : MonoBehaviour
{
    private TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    public void SetText(string text) {
        tmp.text = text;
    }
}
