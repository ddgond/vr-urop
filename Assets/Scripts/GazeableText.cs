using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableText : MonoBehaviour
{
    public GazeableTextBox textBoxPrefab;
    public Vector3 textBoxOffset;
    public string text;
    public int cooldownBaseDuration = 30;

    private GazeableTextBox tb;
    private int cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        tb = Instantiate(textBoxPrefab, transform);
        tb.transform.position += textBoxOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0) {
            cooldown -= 1;
        } else {
            DeactivateText();
        }
        tb.SetOpacity((float)cooldown / (float)cooldownBaseDuration);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + textBoxOffset, new Vector3(0.1f,0.1f,0.1f));
    }

    public void ActivateText()
    {
        print("Text activated");
        tb.SetText(text);
        cooldown = cooldownBaseDuration;
    }

    public void DeactivateText()
    {
        tb.SetText("");
    }
}
