using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotelRoom : MonoBehaviour
{
    public LockedDoor door;

    // Start is called before the first frame update
    void Start()
    {
        door.Deactivate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate()
    {
        door.Activate();
    }
}
