using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostSlider : MonoBehaviour {

    private float timer;
    private float startTimer;

    private float timeAction;
    private float lastSessionTimePart;

    public Image Slider;
    public GameObject GhostCanvas;

    public bool fill;
    public bool fillAfterStart;

	void Update ()
    {
        if (fill)
        {
            timer -= Time.deltaTime;

            Slider.fillAmount = (startTimer - timer) / startTimer;

            if (timer <= 0)
            {
                fill = false;

                GhostController.Singleton.Stop = false;
                GhostCanvas.SetActive(false);
                GhostController.Singleton.SetParameters();
                PlayerData.Singleton.ActionWasInterrupt = false;

                PlayerData.Singleton.IsActionNow = false;
            }
        }

        if (fillAfterStart)
        {
            UIManager.Singleton.DebugLotteryText.text = "NotJustFull";

            timer -= Time.deltaTime;

            float fillPart = lastSessionTimePart + (startTimer - timer) / timeAction;

            Slider.fillAmount = fillPart;

            Debug.Log(fillPart + " fillPart");
            Debug.Log((startTimer - timer) / (startTimer) + " вторая часть уравнения");
            Debug.Log(lastSessionTimePart + " last Session time part");
            Debug.Log(timer + " timer");

            UIManager.Singleton.DebugSliderAmountText.text = fillPart.ToString();            

            if (timer <= 0)
            {
                fillAfterStart = false;

                GhostController.Singleton.Stop = false;
                GhostCanvas.SetActive(false);

                PlayerData.Singleton.FindLastAction();

                PlayerData.Singleton.IsActionNow = false;
                PlayerData.Singleton.ActionWasInterrupt = false;
            }
        }
        
	}

    public void SliderFilling(int fillingTime)
    {
        
    
        fill = true;
        GhostCanvas.SetActive(true);

        timer = fillingTime;
        startTimer = fillingTime;     
    }

    public void SliderFillingAfterStart(int pastTimeAction, int leftTimeAction)
    {
        fillAfterStart = true;
        fill = false;

        GhostCanvas.SetActive(true);

        timer = leftTimeAction;
        startTimer = leftTimeAction;

        timeAction = leftTimeAction + pastTimeAction;

        lastSessionTimePart = pastTimeAction / (timeAction);
    }
}
