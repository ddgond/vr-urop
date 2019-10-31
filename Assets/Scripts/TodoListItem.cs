using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoListItem : MonoBehaviour
{
    public string taskName;
    public bool isComplete = false;

    public void MarkAsComplete() {
        isComplete = true;
    }
}
