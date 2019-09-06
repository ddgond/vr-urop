using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GazeableTextBox : MonoBehaviour
{
    public TextMeshPro textMeshPrefab;
    private TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = Instantiate(textMeshPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
    }

    public void SetText(string text)
    {
        tmp.text = text;
    }

    public void SetOpacity(float opac)
    {
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, opac);
    }
}
