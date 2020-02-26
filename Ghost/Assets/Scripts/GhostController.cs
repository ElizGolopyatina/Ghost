using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostController : MonoBehaviour {

    public static GhostController Singleton;

    public bool HasTarget;

    public ActionManager Parameters;

    private Vector3 Target;

    private GhostSlider ghostSliderComponent;

    public Camera CameraComponent;

    public bool Stop;

    public bool NotFinishedAction;

    private void Awake()
    {
        Singleton = this;

        ghostSliderComponent = gameObject.GetComponent<GhostSlider>();
    }

    private void Start()
    {
        if (NotFinishedAction)
        {           
            float posX = CameraComponent.transform.position.x;
            Vector3 posGhost = new Vector3(posX, transform.position.y, transform.position.z);
            transform.position = posGhost;

            NotFinishedAction = false;
        }
    }

    void LateUpdate ()

    {
        int timeOfAction = Parameters.ActionTime;
        
        if (HasTarget)
        {
            Vector3 ghostTarget = new Vector3(Target.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, ghostTarget, 3f * Time.deltaTime);

            Vector3 camPos = CameraComponent.transform.position;
            camPos.x = transform.position.x;
            CameraComponent.transform.position = camPos;

            if (Mathf.Abs(Target.x - transform.position.x) <= 0.01)
                {
                    HasTarget = false;
                    FillSlider();
                }
                return;           
        }

        if (!Stop)
        {
            float targetX = CameraController.Singleton.transform.position.x;
            Vector3 target = new Vector3(targetX, transform.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, target, 3f * Time.deltaTime);
        }
               
	}

    public void MoveToObject(Vector3 objTarget)
    {
        HasTarget = true;
        Target = objTarget;
    }

    public void SetParameters()
    {
        PlayerData.Singleton.changingBellyful(Parameters.BellyfulParameter);
        PlayerData.Singleton.changingClean(Parameters.CleanParameter);
        PlayerData.Singleton.changingMood(Parameters.MoodParameter);
        PlayerData.Singleton.changingRest(Parameters.RestParameter);
    }

    public void FillSlider()
    {
        ghostSliderComponent.SliderFilling(Parameters.ActionTime);
    }

    public void FillSliderAfterStart(int pastActionTime, int leftActionTime)
    {
        ghostSliderComponent.SliderFillingAfterStart(pastActionTime, leftActionTime);
    }
}
