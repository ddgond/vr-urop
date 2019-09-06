using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTracker : MonoBehaviour
{
    public float rayDistance = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit rHit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out rHit, rayDistance) && rHit.transform.gameObject != null) {
            GameObject target = rHit.transform.gameObject;
            if (target.GetComponent<GazeableText>() != null) {
                GazeableText gt = target.GetComponent<GazeableText>();
                gt.ActivateText();
            }
        }
    }
}
