using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]

public class ActionManager
{
    public Product NewActionObj;
    public Product MinusableObject;

    public float MoodParameter;
    public float CleanParameter;
    public float BellyfulParameter;
    public float RestParameter;

    public string ActionName;

    public int ActionTime;

    public float TimeDelay;
    private long pressTime;

    public void SetPressTime()
    {
        pressTime = DateTime.Now.Ticks;

        int startActionTime = PlayerData.Singleton.GetCurrentTime();
        PlayerPrefs.SetInt("startActionTime", startActionTime);

        int endActionTime = startActionTime + ActionTime;
        PlayerPrefs.SetInt("endActionTime", endActionTime);
        //Debug.Log(PlayerPrefs.GetInt("endActionTime") + " end action time1");

        PlayerPrefs.SetString("actionName", ActionName);

    }

    public bool CheckButtonAccess()
    {
        //Debug.Log(PlayerPrefs.GetInt("endActionTime") + " end action time2");
        
        
        

        long currentTime = DateTime.Now.Ticks;

        bool actionDelayIsGone = currentTime - pressTime >= TimeDelay * TimeSpan.TicksPerSecond;

        Debug.Log(PlayerData.Singleton.GetCurrentTime());
        Debug.Log(PlayerPrefs.GetInt("endActionTime"));
        bool actionWasInterrupt = PlayerData.Singleton.ActionWasInterrupt;

        Debug.Log(actionWasInterrupt + " interrupt");
       
        bool actionInProcess = PlayerData.Singleton.IsActionNow;
        

        return ((actionDelayIsGone || actionWasInterrupt) && !actionInProcess);        
    }

    public bool CheckActionNewObject()
    {
        if (NewActionObj == null) return true;

        if (PlayerData.Singleton.GetSavedProductAmount(NewActionObj.Name) == 0) return false;

        return true;
    }

}
