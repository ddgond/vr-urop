using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public Transform keyTarget;

    private float keyMoveDuration = 0.5f;
    private float keyTurnDuration = 0.5f;
    private float doorOpenDuration = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DoorKey>() != null) {
            Destroy(other.GetComponent<DoorKey>());
            Destroy(other.GetComponent<Valve.VR.InteractionSystem.Throwable>());
            Destroy(other.GetComponent<Valve.VR.InteractionSystem.Interactable>());
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Rigidbody>().detectCollisions = false;
            StartCoroutine(BringKeyToDoor(other.transform));
        }
    }

    IEnumerator BringKeyToDoor(Transform key)
    {
        float startTime = Time.time;
        float endTime = startTime + keyMoveDuration;
        key.parent = transform;
        Vector3 startPosition = key.localPosition;
        Quaternion startRotation = key.localRotation;
        while (Time.time < endTime) {
            key.parent = transform;
            key.GetComponent<Rigidbody>().isKinematic = true;
            key.GetComponent<Rigidbody>().detectCollisions = false;
            key.localPosition = Vector3.Lerp(startPosition, keyTarget.transform.localPosition, (Time.time - startTime) / keyMoveDuration);
            key.localRotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0, -90, 0), (Time.time - startTime) / keyMoveDuration);
            yield return null;
        }
        key.localPosition = keyTarget.transform.localPosition;
        key.localRotation = Quaternion.Euler(0, -90, 0);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(TurnKey(key));
    }

    IEnumerator TurnKey(Transform key) {
        float startTime = Time.time;
        float endTime = startTime + keyTurnDuration;
        Quaternion startRotation = key.localRotation;
        while (Time.time < endTime) {
            key.localRotation = Quaternion.Lerp(startRotation, Quaternion.Euler(90, -90, 0), (Time.time - startTime) / keyTurnDuration);
            yield return null;
        }
        key.localRotation = Quaternion.Euler(90, -90, 0);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        float startTime = Time.time;
        float endTime = startTime + doorOpenDuration;
        while (Time.time < endTime) {
            transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, -90, 0), (Time.time - startTime) / doorOpenDuration);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }
}
