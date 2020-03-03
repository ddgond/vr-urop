using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Sign : MonoBehaviour
{
    public string text = "Text";

    private List<TMPro.TextMeshPro> tms;

    // Start is called before the first frame update
    void Start()
    {
        tms = new List<TMPro.TextMeshPro>(GetComponentsInChildren<TMPro.TextMeshPro>());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (TMPro.TextMeshPro tm in tms)
        {
            tm.text = text;
        }
    }
}
