using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    private Vector2 startPos;
    private Vector2 direction;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    startPos = touch.position;
                    break;

                case TouchPhase.Moved:

                    direction = touch.position - startPos;
                    startPos = touch.position;

                    CameraController.Singleton.MoveCamera(direction.x);
                    break;

                case TouchPhase.Ended:

                    break;
            }
        }
    }
}
