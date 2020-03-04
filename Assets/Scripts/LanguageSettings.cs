using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSettings : MonoBehaviour
{
    public enum Language
    {
        ENGLISH,
        SPANISH,
        JAPANESE
    }

    public List<Language> languages = new List<Language>();

    private Language currentLanguage = 0;

    public Language GetCurrentLanguage()
    {
        return currentLanguage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGrip()
    {
        ToggleLanguage();
    }

    private void ToggleLanguage()
    {
        currentLanguage += 1;
        currentLanguage = (Language)((int)currentLanguage % System.Enum.GetNames(typeof(Language)).Length);
    }
}
