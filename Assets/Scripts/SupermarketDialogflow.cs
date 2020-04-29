using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonData;

public class SupermarketDialogflow : DialogflowAPIScript
{
    private Dictionary<string, string> location = new Dictionary<string, string>()
    {
        ["green peppers"] = "in the vegetables section on the right side of the store",
        ["red peppers"] = "in the vegetables section on the right side of the store",
        ["tomatoes"] = "in the vegetables section on the right side of the store",
        ["carrots"] = "in the vegetables section on the right side of the store",
        ["asparagus"] = "in the vegetables section on the right side of the store",
        ["garlic"] = "in the vegetables section on the right side of the store",
        ["leeks"] = "in the vegetables section on the right side of the store",
        ["onions"] = "in the vegetables section on the right side of the store",
        ["zucchinis"] = "in the vegetables section on the right side of the store",
        ["artichokes"] = "in the vegetables section on the right side of the store",
        ["eggplants"] = "in the vegetables section on the right side of the store",
        ["avocados"] = "in the fruit section on the right side of the store",
        ["oranges"] = "in the fruit section on the right side of the store",
        ["apples"] = "in the fruit section on the right side of the store",
        ["bananas"] = "in the fruit section on the right side of the store",
        ["pears"] = "in the fruit section on the right side of the store",
        ["melons"] = "in the fruit section on the right side of the store"
    };

    public override void ResolveText(string text)
    {
        if (text.StartsWith(".")) {
            string item = text.Substring(1);
            text = "You can find " + item + " " + location[item];
        }
        SynthesizeSpeech(text);
    }
}
