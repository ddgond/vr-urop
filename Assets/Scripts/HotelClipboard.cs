using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HotelClipboard : MonoBehaviour
{
    public TodoListItem disabledTodoListItem;

    private TodoList todoList;
    private bool hasEnabledFinalTodoListItem = false;
    private bool isHeld = false;
    private bool loading = false;

    // Start is called before the first frame update
    void Start()
    {
        todoList = GetComponent<TodoList>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasEnabledFinalTodoListItem && disabledTodoListItem.isComplete && !loading)
        {
            loading = true;
            StartCoroutine(LoadCityScene());
        }

        if (todoList.IsComplete() && !hasEnabledFinalTodoListItem)
        {
            hasEnabledFinalTodoListItem = true;
            todoList.Clear();
            todoList.AddItem(disabledTodoListItem);
            disabledTodoListItem.gameObject.SetActive(true);
        }
    }

    public void OnPickUp()
    {
        isHeld = true;
    }

    // Have to use Throwable's events to trigger this
    public void OnDrop()
    {
        isHeld = false;
    }

    IEnumerator LoadCityScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("city");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
