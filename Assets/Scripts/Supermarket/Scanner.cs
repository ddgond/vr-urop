using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public Vector3 postScanPosition = Vector3.zero;

    private List<List<string>> scannedItems = new List<List<string>>();
    private List<int> amounts = new List<int>();
    private SupermarketClipboard clipboard;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + postScanPosition, 0.1f);
    }

    void Start()
    {
        clipboard = FindObjectOfType<SupermarketClipboard>();
    }

    void OnTriggerEnter(Collider collider)
    {
        ItemLabel itemLabel = collider.GetComponentInParent<ItemLabel>();
        if (itemLabel != null && clipboard.Contains(itemLabel.label) && GetAmount(itemLabel.label) < clipboard.GetAmount(itemLabel.label))
        {
            Add(itemLabel.label);
            itemLabel.transform.position = transform.position + postScanPosition;
            collider.attachedRigidbody.velocity = Vector3.zero;
            GetComponentInChildren<AudioSource>().Play();
        }
    }

    void Add(List<string> other)
    {
        for (int i = 0; i < scannedItems.Count; i++)
        {
            List<string> item = scannedItems[i];
            if (item.SequenceEqual(other))
            {
                amounts[i] += 1;
                return;
            }
        }
        scannedItems.Add(other);
        amounts.Add(1);
        clipboard.UpdateStatus(scannedItems, amounts);
    }

    int GetAmount(List<string> other)
    {
        for (int i = 0; i < scannedItems.Count; i++)
        {
            List<string> item = scannedItems[i];
            if (item.SequenceEqual(other))
            {
                return amounts[i];
            }
        }
        return 0;
    }
}
