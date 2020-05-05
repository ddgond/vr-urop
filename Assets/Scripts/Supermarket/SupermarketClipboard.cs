using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class SupermarketClipboard : MonoBehaviour
{
    public List<string> itemCounterText = new List<string>();

    private const int NUM_ITEMS = 3;
    private List<List<string>> itemsToGrab = new List<List<string>>();
    private List<int> itemCount = new List<int>();
    private List<bool> itemCompletions = new List<bool>();
    private LanguageSettings languageSettings;
    private TextMeshPro textMesh;
    private bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        languageSettings = FindObjectOfType<LanguageSettings>();
        textMesh = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (itemsToGrab.Count == 0)
        {
            Setup();
        }
        string text = "";
        for (int i = 0; i < itemsToGrab.Count; i++)
        {
            if (itemCompletions[i])
            {
                text += "<color=#007700>";
            }
            int language = (int)languageSettings.GetCurrentLanguage();
            language = Mathf.Clamp(language, 0, itemCounterText.Count - 1);
            text += itemCount[i] + itemCounterText[language] + itemsToGrab[i][language] + "\n";
            if (itemCompletions[i])
            {
                text += "</color>";
            }
        }
        textMesh.text = text;
    }

    private void Setup()
    {
        List<List<string>> itemsInStore = new List<List<string>>();
        foreach (ItemLabel item in FindObjectsOfType<ItemLabel>())
        {
            string checkLabel = item.GetLabels()[0];
            bool alreadyExists = false;
            foreach (List<string> savedItem in itemsInStore)
            {
                if (savedItem[0] == checkLabel)
                {
                    alreadyExists = true;
                    break;
                }
            }
            if (!alreadyExists)
            {
                itemsInStore.Add(item.GetLabels());
            }
        }
        int numbers = 0;
        while (numbers < NUM_ITEMS)
        {
            int index = (int)(Random.value * (itemsInStore.Count - numbers));
            itemsToGrab.Add(itemsInStore[index]);
            itemCount.Add((int)(Random.value * 3 + 1));
            itemCompletions.Add(false);
            itemsInStore[index] = itemsInStore[itemsInStore.Count - 1 - numbers];
            numbers++;
        }
    }

    public bool Contains(List<string> other)
    {
        foreach(List<string> item in itemsToGrab)
        {
            if (item.SequenceEqual(other))
            {
                return true;
            }
        }
        return false;
    }

    public int GetAmount(List<string> other)
    {
        for (int i = 0; i < itemsToGrab.Count; i++)
        {
            List<string> item = itemsToGrab[i];
            if (item.SequenceEqual(other))
            {
                return itemCount[i];
            }
        }
        return 0;
    }

    public List<List<string>> GetItems()
    {
        return new List<List<string>>(itemsToGrab);
    }

    public void UpdateStatus(List<List<string>> scannedItems, List<int> scannedAmounts)
    {
        for (int i = 0; i < itemsToGrab.Count; i++)
        {
            List<string> item = itemsToGrab[i];
            for (int j = 0; j < scannedItems.Count; j++)
            {
                List<string> scannedItem = scannedItems[j];
                if (item[0] == scannedItem[0])
                {
                    if (itemCount[i] == scannedAmounts[j])
                    {
                        itemCompletions[i] = true;
                    }
                    break;
                }
            }
        }
        CheckCompletion();
    }

    void CheckCompletion()
    {
        foreach (bool complete in itemCompletions)
        {
            if (!complete)
            {
                return;
            }
        }
        Complete();
    }

    void Complete()
    {
        if (isComplete)
        {
            return;
        }
        isComplete = true;
        Debug.Log("You did it!");
        // Do things
    }
}
