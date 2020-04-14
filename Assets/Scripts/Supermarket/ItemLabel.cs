using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLabel : MonoBehaviour
{
    public List<string> label = new List<string>();

    private Rigidbody rb;
    private LanguageSettings languageSettings;
    private Basket basket;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        languageSettings = FindObjectOfType<LanguageSettings>();
        basket = FindObjectOfType<Basket>();
        GetComponent<Valve.VR.InteractionSystem.Throwable>().onDetachFromHand.AddListener(CheckInBasket);
    }

    public string GetLabel()
    {
        int language = (int)languageSettings.GetCurrentLanguage();
        language = Mathf.Clamp(language, 0, label.Count - 1);
        return label[language];
    }

    public List<string> GetLabels()
    {
        return new List<string>(label);
    }

    void CheckInBasket()
    {
        if (basket.Contains(gameObject))
        {
            StickToBasket();
        } else
        {
            rb.isKinematic = false;
            rb.transform.parent = null;
        }
    }

    public bool IsFalling()
    {
        return !rb.isKinematic && rb.velocity.y < -0.2;
    }

    public void StickToBasket()
    {
        rb.isKinematic = true;
        rb.transform.parent = basket.transform;
    }
}
