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
    public HotelRoom[] rooms;

    private bool finished = false;
    private bool gaveDirections = false;
    private int roomNumber = -1;

    public override void SendMessage(String message) {
        if (!gaveDirections) {
            if (finished) {
                GiveDirections();
                return;
            }
    		base.SendMessage(message);
        }
	}

    public override void ProcessResult(string result) {
        ResponseBody content = (ResponseBody)JsonUtility.FromJson<ResponseBody>(result);
        Debug.Log(content.queryResult.fulfillmentText);
        print("Intent name: " + content.queryResult.intent.displayName);
        if (content.queryResult.allRequiredParamsPresent && content.queryResult.intent.displayName == "Renting a Room - yes") {
            Debug.Log("Registration complete!");
            Instantiate(keyPrefab, keySpawn.position, Quaternion.Euler(-90f,0f,0f));
            GenerateRoomNumber();
        }
    }

    private void GenerateRoomNumber() {
        roomNumber = (int) (UnityEngine.Random.value * rooms.Length) + 1;
        finished = true;
        rooms[roomNumber - 1].Activate();
    }

    private void GiveDirections() {
        int numDoorsDown = rooms.Length - roomNumber + 1;
        string directions = "Your room is room number " + roomNumber + ". To get there, exit this building, turn to your right, and it'll be " + numDoorsDown;
        if (numDoorsDown == 1) {
            directions += " door down.";
        } else {
            directions += " doors down.";
        }
        SynthesizeSpeech(directions);
        gaveDirections = true;
    }
}
