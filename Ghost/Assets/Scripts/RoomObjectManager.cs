using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObjectManager : MonoBehaviour
{
    public static RoomObjectManager Singleton;

    public RoomObject[] RoomObjectArray;

    private void Awake()
    {
        Singleton = this;
    }

    public ActionManager GetRoomObjectAction(string roomObjectName, string actionName)
    {
        foreach (RoomObject roomObject in RoomObjectArray)
        {
            Debug.Log(roomObjectName);
            if (roomObject.name == roomObjectName)
            {                
                return roomObject.GetAction(actionName);
            }
        }
        return null;
    }

    public float GetTargetObjPos(string roomObjectName)
    {
        foreach (RoomObject roomObject in RoomObjectArray)
        {

            if (roomObject.name == roomObjectName)
            {
                return roomObject.TargetObj.transform.position.x;
            }
        }
        return 0;
    }
}
