using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class DresserDoor : MonoBehaviour
{
    public GameObject luggage;

    private Vector3 absoluteDresserCenter;
    private Vector3 inDresserCenter = new Vector3(0.335879f, 0.9348772f, -0.5081707f);
    private Vector3 inDresserScale = new Vector3(0.671758f, 1.869755f, 0.8117406f);

    private LinearMapping lm;

    // Start is called before the first frame update
    void Start()
    {
        lm = GetComponent<LinearMapping>();
        absoluteDresserCenter = transform.TransformPoint(inDresserCenter);
    }

    // Update is called once per frame
    void Update()
    {
        if (lm.value > 0.95f && IsLuggageInDresser()) {
            GetComponent<TodoListItem>().MarkAsComplete();
            Destroy(this);
        }
    }

    bool IsLuggageInDresser() {
        Collider[] hitColliders = Physics.OverlapBox(absoluteDresserCenter, inDresserScale / 2, transform.rotation);
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject == luggage) {
                return true;
            }
        }
        return false;
    }
}
