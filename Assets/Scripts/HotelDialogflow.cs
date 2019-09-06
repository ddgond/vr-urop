using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using JsonData;

public class HotelDialogflow : DialogflowAPIScript
{
    public GameObject keyPrefab;
    public Transform keySpawn;

    public override void ProcessResult(string result) {
        ResponseBody content = (ResponseBody)JsonUtility.FromJson<ResponseBody>(result);
        Debug.Log(content.queryResult.fulfillmentText);
        print("Intent name: " + content.queryResult.intent.displayName);
        if (content.queryResult.allRequiredParamsPresent && content.queryResult.intent.displayName == "Renting a Room - yes") {
            Debug.Log("Registration complete!");
            Instantiate(keyPrefab, keySpawn.position, Quaternion.Euler(-90f,0f,0f));
        }
    }
}
