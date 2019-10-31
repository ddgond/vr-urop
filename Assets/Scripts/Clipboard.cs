using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clipboard : MonoBehaviour
{
    public void OnPickUp() {
        GetComponent<TodoListItem>().MarkAsComplete();
        Destroy(this);
    }
}
