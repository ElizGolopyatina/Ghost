using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorActivity : MonoBehaviour {

    int i = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            i += 1;
        }
    }

    //    void Update()
    //    {
    //#if UNITY_EDITOR
    //        DebugText.text = "Method start";
    //        Linker.Singleton.OverCanvas = EventSystem.current.IsPointerOverGameObject();
    //#endif

    //    }
}
