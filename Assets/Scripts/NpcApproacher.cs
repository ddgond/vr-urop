using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcApproacher : MonoBehaviour
{
    public Transform player;
    public Transform NpcList;

    private Transform otherNpc;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool Approach()
    {
        Transform bestNpc = null;
        float bestDist = float.PositiveInfinity;
        for (int i = 0; i < NpcList.childCount; i++)
        {
            Transform npc = NpcList.GetChild(i);
            float dist = Vector3.Distance(npc.position, player.position);
            if (dist < bestDist)
            {
                bestNpc = npc;
                bestDist = dist;
            }
        }

        otherNpc = bestNpc;
        otherNpc.gameObject.SetActive(false);

        NpcWander wander = otherNpc.gameObject.GetComponent<NpcWander>();
        wander.active = false;


        this.gameObject.SetActive(true);
        return true;
    }

    public void Finish()
    {
        otherNpc.gameObject.SetActive(true);
        this.gameObject.SetActive(false);

        NpcWander wander = otherNpc.gameObject.GetComponent<NpcWander>();
        wander.active = true;
    }
}
