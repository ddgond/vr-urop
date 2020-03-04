using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Sign : MonoBehaviour
{
    public List<string> text = new List<string>();

    private List<TMPro.TextMeshPro> tms;
    private LanguageSettings languageSettings;

    // Start is called before the first frame update
    void Start()
    {
        tms = new List<TMPro.TextMeshPro>(GetComponentsInChildren<TMPro.TextMeshPro>());
        languageSettings = FindObjectOfType<LanguageSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (text.Count > 0)
        {
            int language = (int)languageSettings.GetCurrentLanguage();
            language = Mathf.Clamp(language, 0, text.Count-1);
            foreach (TMPro.TextMeshPro tm in tms)
            {
                tm.text = text[language];
            }
        }
    }
}
