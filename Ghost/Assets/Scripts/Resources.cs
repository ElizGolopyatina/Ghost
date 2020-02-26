using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Resources

{
    public float Bellyful;
    public float Mood;
    public float Clean;
    public float Rest;

    public static Resources operator *(Resources c1, float val)
    {
        Resources c2 = new Resources(c1.Bellyful * val, c1.Mood * val, c1.Clean * val, c1.Rest * val);

        return  c2;
    }

    public Resources()
    {
        //дефолтный конструктор 
    }

    public Resources (float belly, float mood, float clean, float rest)
    {      
        Bellyful = belly;
        Mood = mood;
        Clean = clean;
        Rest = rest;
    }
}
