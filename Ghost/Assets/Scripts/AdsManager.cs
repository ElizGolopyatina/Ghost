using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Singleton;

    private string gameID = "3436297";

    bool testMode = false; 

    private void Awake()
    {
        Singleton = this;
    }

    void Start()
    {
        Advertisement.Initialize(gameID, testMode);
    }

    public void ShowAds()
    {
        Advertisement.Show();
    }

}
