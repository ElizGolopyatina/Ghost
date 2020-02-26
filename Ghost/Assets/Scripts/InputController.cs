using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	void Start () {
		
	}

    void Update()
    {
        KeyboardInput();
    }

    public void KeyboardInput()
    {
        float axis = Input.GetAxis("Horizontal");
        if (axis != 0)
        {
            CameraController.Singleton.MoveCamera(axis);
        }
    }


}
