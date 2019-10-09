using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWander : MonoBehaviour
{
    // NPC interpolates at constant speed between all nodes in NPCPath

    public Transform NPCPath;
    public float speed = 1.0f;
    public float angularSpeed = 1.0f;
    public int currentNodeIndex;

    private float startTime;
    private int totalNodes;
    private float segmentLength;

    private Vector3 currentNode 
    {
        get { return NPCPath.GetChild(currentNodeIndex).position; }
    }

    private Vector3 nextNode
    {
        get { return NPCPath.GetChild((currentNodeIndex + 1) % NPCPath.childCount).position; }
    }

    void Start()
    {
        totalNodes = NPCPath.childCount;
        configureNextSegment();
    }

    // Update is called once per frame
    void Update()
    {
        // interpolate position
        float distanceCovered = (Time.time - startTime) * speed;
        float progress = distanceCovered / segmentLength;
        transform.position = Vector3.Lerp(currentNode, nextNode, progress);

        // smooth rotate NPC direction if necessary
        Vector3 moveDirection = nextNode - transform.position;
        float step = angularSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, moveDirection, step, 0.0f));

        if (progress >= 1)
        {
            currentNodeIndex = (currentNodeIndex + 1) % NPCPath.childCount;
            configureNextSegment();
        }
    }

    void configureNextSegment()
    {
        startTime = Time.time;
        segmentLength = Vector3.Distance(currentNode, nextNode);
    }
}
