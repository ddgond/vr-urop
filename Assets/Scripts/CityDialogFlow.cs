using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using JsonData;

public class CityDialogFlow : DialogflowAPIScriptCustomTTS
{
    public GameObject mainNPC;

    private string lastMessage = "";
    private bool inProgress = false;

    public override void ProcessResult(string result) {
        ResponseBody content = (ResponseBody)JsonUtility.FromJson<ResponseBody>(result);
        Debug.Log(content.queryResult.fulfillmentText);
        Debug.Log("DialogFlow Response:" + result);
        string intent = content.queryResult.intent.displayName;

        if (intent == "introduction")
        {
            AttractNPC(content.queryResult.fulfillmentText);
        } else if (inProgress) {
            if (intent == "direction-query")
            {
                GiveDirections(result);
            } else if (intent == "direction-query - repeat")
            {
                SynthesizeSpeech(lastMessage);
            } else if (intent == "Default Fallback Intent")
            {
                    SynthesizeSpeech(content.queryResult.fulfillmentText);
            } else if (intent == "end")
            {
                    SynthesizeSpeech(content.queryResult.fulfillmentText);
                    ReleaseNPC();
            }
        }
    }

    public void AttractNPC(string greeting) {
        SynthesizeSpeech(greeting);
        NpcApproacher npc = mainNPC.GetComponent<NpcApproacher>();
        inProgress = npc.Approach();
  	}

    public void ReleaseNPC() {
        NpcApproacher npc = mainNPC.GetComponent<NpcApproacher>();
        npc.Finish();
        inProgress = false;
    }

  	public void GiveDirections(string json) {
        // this is the nastiest workaround
        int outputContextsIndex = json.IndexOf("outputContexts");
        if (outputContextsIndex == -1) {
                return;
        }
        int parametersIndex = json.IndexOf("parameters", outputContextsIndex);
        int openBracketIndex = json.IndexOf("{", parametersIndex);
        int closedBracketIndex = json.IndexOf("}", openBracketIndex);
        json = json.Substring(openBracketIndex, closedBracketIndex - openBracketIndex + 1);
        DirectionsParameters orderParams = (DirectionsParameters)JsonUtility.FromJson<DirectionsParameters>(json);

        string place = orderParams.Place;
        Debug.Log("Giving directions to " + place);
        lastMessage = place + "‚Í‚ ‚»‚±‚Å‚·";
        SynthesizeSpeech(lastMessage);
  	}
}
