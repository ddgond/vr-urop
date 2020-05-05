using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Credit_Card")
        {
            GetComponentInChildren<AudioSource>().Play();
        } else
        {
            Debug.Log(collider.name + " " + collider.gameObject.name);
        }
    }
}
