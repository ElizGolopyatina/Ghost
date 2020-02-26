using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Singleton;

    private Camera _cameraComponent;

    public float Velocity;
    
    public Camera CameraComponent
    {
        get
        {
            if (!_cameraComponent)
            {
                _cameraComponent = GetComponent<Camera>();
            }
            return _cameraComponent;
        }
    }

    private void Awake()
    {
        Singleton = this;
    }


    public void MoveCamera(float delta)
    {
        float Vel = 0;

#if UNITY_EDITOR
        Vel = 30;

#else
        Vel = 0.8f;
#endif

        Vector3 pos = transform.position;

        pos.x += delta * Time.deltaTime * Vel;

        transform.position = pos;
    }

    public void StartCamera(float cameraPos)
    {

        Vector3 pos = transform.position;

        pos.x = cameraPos;

        transform.position = pos;
    }

}
