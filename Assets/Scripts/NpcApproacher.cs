using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcApproacher : MonoBehaviour
{
    public Transform player;
    public Transform NpcList;
    public int maxNpcDistance;
    public AudioSource source;

    private const int INACTIVE = 0, ACTIVE = 1, POINTING = 2, DISABLING = 3;
    private int state = INACTIVE;
    private Vector3 originalDirection;
    private Transform otherNpc;
    private Vector3 destLocation;
    private bool sourceWasPlaying;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float step = 8 * Time.deltaTime;
        switch (state)
        {
            case INACTIVE:
                // pass
                break;
            case ACTIVE:
                Vector3 targetDirection = player.position - transform.position;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f));
                break;
            case POINTING:
                Vector3 pointDirection = destLocation - transform.position;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, pointDirection, step, 0.0f));
                if (!source.isPlaying && sourceWasPlaying)
                    state = ACTIVE;
                sourceWasPlaying = source.isPlaying;
                break;
            case DISABLING:
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, originalDirection, step, 0.0f));
                if (Vector3.Distance(originalDirection, transform.forward) < 0.01f)
                {
                    state = INACTIVE;
                    otherNpc.gameObject.SetActive(true);
                    gameObject.SetActive(false);

                    NpcWander wander = otherNpc.gameObject.GetComponent<NpcWander>();
                    wander.active = true;
                }
                break;
            default:
                // pass
                break;
        }
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

        if (bestDist > maxNpcDistance)
            return false; // no nearby NPC available

        otherNpc = bestNpc;
        otherNpc.gameObject.SetActive(false);

        NpcWander wander = otherNpc.gameObject.GetComponent<NpcWander>();
        wander.active = false;

        transform.position = otherNpc.transform.position;
        transform.rotation = otherNpc.transform.rotation;
        gameObject.SetActive(true);
        state = ACTIVE;
        originalDirection = transform.forward;
        return true;
    }

    public void Point(Vector3 dest)
    {
        destLocation = dest;
        state = POINTING;
    }

    public void CancelPoint()
    {
        if (state == POINTING)
            state = ACTIVE;
    }

    public void Finish()
    {
        state = DISABLING;
    }
}
