using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    public GameObject[] objects;
    private Dictionary<GameObject,bool> isDetected = new Dictionary<GameObject,bool>();

    void Start() {
        foreach (GameObject obj in objects) {
            isDetected.Add(obj, false);
        }
    }

    void CheckAllDetected() {
        foreach (bool b in isDetected.Values) {
            if (!b) {
                return;
            }
        }
        GetComponent<TodoListItem>().MarkAsComplete();
        Destroy(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDetected.ContainsKey(other.gameObject)) {
            isDetected[other.gameObject] = true;
        }
        CheckAllDetected();
    }

    void OnTriggerExit(Collider other)
    {
        if (isDetected.ContainsKey(other.gameObject)) {
            isDetected[other.gameObject] = false;
        }
    }
}
