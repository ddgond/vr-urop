using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    private List<GameObject> children = new List<GameObject>();
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject != gameObject)
            {
                children.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        children[0].SetActive(true);
    }

    public void Toggle()
    {
        children[index].SetActive(false);
        index += 1;
        index %= children.Count;
        children[index].SetActive(true);
    }
}
