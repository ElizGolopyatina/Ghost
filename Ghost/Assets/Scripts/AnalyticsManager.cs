using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Singleton;

    private void Awake()
    {
        Singleton = this;
    }

    public void ReportBuying(int buyingSize)
    {
        AnalyticsEvent.Custom("something bought", new Dictionary<string, object>
    {
        { "coin spent", buyingSize },
    });
    }


}
