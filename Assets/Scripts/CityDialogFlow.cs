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
    public Transform map;
    public Transform player;

    public Transform hotel;
    public Transform restaurant;

    private string lastMessage = "";
    private bool inProgress = false;
    private const int THRESH = 7;

    public string Directions()
    {
        Transform dest = hotel;
        Transform cur = player;
        Transform prev = player;
        Transform next = player;
        string output = "";

        float xdestdist = Math.Abs(cur.position.x - dest.position.x);
        float zdestdist = Math.Abs(cur.position.z - dest.position.z);

        // can the player simply go straight to the destination?
        if (xdestdist < THRESH || zdestdist < THRESH)
        {
            next = dest;
        }

        print("START");

        while (next != dest)
        {
            prev = cur;
            cur = next;

            float best = 99999999f; 

            // find the first node to go to (may just be go straight to destination)
            for (int i = 0; i < map.childCount; i++)
            {
                Transform node = map.GetChild(i);
                float xdist = Math.Abs(cur.position.x - node.position.x);
                float zdist = Math.Abs(cur.position.z - node.position.z);

                // check that this node is on the same street as the player
                if (xdist < THRESH || zdist < THRESH)
                {
                    float dist = Vector3.Distance(node.position, dest.position);
                    if (dist < best)
                    {
                        best = dist;
                        next = node;
                    }
                }
            }

            print(cur.name);
            output += translateDirections(prev, cur, next, dest) + "、";;
        }

        output += translateDirections(prev, dest, null, dest);
        return output;
    }

    private string translateDirections(Transform prev, Transform cur, Transform next, Transform dest)
    {
        if (prev == cur)
        {
            if (Vector3.Distance(cur.position, next.position) > 30)
                return "この道をまっすぐ行って";
            return ""; // on first iteration, pass
        }

        if (dest == cur)
        {
            return "まっすぐ行ったらホテルがあります";
        }

        float xdist = Math.Abs(cur.position.x - prev.position.x);
        float zdist = Math.Abs(cur.position.z - prev.position.z);
        int blocks = 1;
        if (zdist > 40 || xdist > 60)
        {
            blocks = 2;
        }

        Vector3 cross = Vector3.Cross(cur.position - prev.position, next.position - cur.position);
        string turnDir = cross.y > 0 ? "右" : "左";

        string nextText = next == dest ? "" : "、まっすぐ行って";
        if (cur.name.Contains("corner"))
        {
            return "角で" + turnDir + "に曲がって" + nextText;
        } else
        {
            if (blocks == 1)
            {
                return "ひとつ目の交差点で" + turnDir + "に曲がって" + nextText;
            } else
            {
                return "ふたつ目の交差点で" + turnDir + "に曲がって" + nextText;
            }
        }
    }
        

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
                // GiveDirections(result);
                string dir = Directions();
                print(dir);
                SynthesizeSpeech(dir);
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

  	public void GiveDirectionsLegacy(string json) {
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
        lastMessage = place + "はあそこです";
        SynthesizeSpeech(lastMessage);
  	}
}
