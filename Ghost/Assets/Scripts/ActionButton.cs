using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour {

    public int Index;
    public RoomObject RoomObjectComponent;

    private Button thisButton;

    private void Start()
    {
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TransferIndex);       
    }

    public void TransferIndex()
    {
        PlayerData.Singleton.IsActionNow = true;

        PlayerData.Singleton.SavePressRoomObjectName(RoomObjectComponent.name);

        GhostController.Singleton.Stop = true;

        thisButton.interactable = false;
        RoomObjectComponent.SetIndexes(Index);
    }
}
