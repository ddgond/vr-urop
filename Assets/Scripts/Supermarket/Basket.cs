using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private List<GameObject> itemLabelsWithin = new List<GameObject>();

    void OnTriggerEnter(Collider collider)
    {
        ItemLabel itemLabel = collider.GetComponentInParent<ItemLabel>();
        if (itemLabel != null && !itemLabelsWithin.Contains(itemLabel.gameObject))
        {
            itemLabelsWithin.Add(itemLabel.gameObject);
            if (itemLabel.IsFalling())
            {
                itemLabel.StickToBasket();
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        ItemLabel itemLabel = collider.GetComponentInParent<ItemLabel>();
        if (itemLabel != null && itemLabelsWithin.Contains(itemLabel.gameObject))
        {
            itemLabelsWithin.Remove(itemLabel.gameObject);
        }
    }

    public bool Contains(GameObject go)
    {
        return itemLabelsWithin.Contains(go);
    }
}
