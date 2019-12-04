using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TodoList : MonoBehaviour
{
    public List<TodoListItem> todoListItems = new List<TodoListItem>();

    private TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
        string todoListText = "TODO LIST";
        foreach (TodoListItem item in todoListItems)
        {
            todoListText += "\n- " + item.taskName;
        }
        tmp.text = todoListText;
    }

    // Update is called once per frame
    void Update()
    {
        string todoListText = "TODO LIST";
        foreach (TodoListItem item in todoListItems)
        {
            if (item.isComplete)
            {
                todoListText += "\n<s>- " + item.taskName + "</s>";
            }
            else
            {
                todoListText += "\n- " + item.taskName;
            }
        }
        tmp.text = todoListText;
    }

    public bool IsComplete()
    {
        foreach (TodoListItem item in todoListItems)
        {
            if (!item.isComplete)
            {
                return false;
            }
        }
        return true;
    }

    public void AddItem(TodoListItem item)
    {
        todoListItems.Add(item);
    }

    public void Clear()
    {
        todoListItems.Clear();
    }
}
