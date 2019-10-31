using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using JsonData;
using TMPro;

public class DialogflowAPIScriptCustomTTS : DialogflowAPIScript {
    public override void ResolveText(string text)
    {
        // do nothing, let subclasses deal with resolving TTS by themselves
    }

    public override void SynthesizeSpeech(String text) {
        // Uses CustomTextToSpeech instead of ExampleTextToSpeech     
        dialogTextbox.text = text;
        CustomTextToSpeech tts = GameObject.FindWithTag("TTS").GetComponent<CustomTextToSpeech>();
        StartCoroutine(tts.Convert(text));
	}
}
