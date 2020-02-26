using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class PlayerData : MonoBehaviour {

    public static PlayerData Singleton;

    public Resources Parameters;

    public Resources Coefficients;

    public int CoinAmount;
    public int CrystalAmount;

    public int CurrentLevel;
    public float CurrentLevelPoint;
    public Text LevelText;

    public int[] LevelPoint = new int[] { 10, 40, 100, 300, 800, 2000, 4000, 10000, 20000, 30000 };
    public GameObject[] ProductsOnLevel;

    public int[] AddCoinOnCurrentLevel = new int[] { 0, 50, 100, 200, 400, 800, 1600, 3200, 6400, 12800 };

    public int StartLotteryTime;

    public bool ActionWasInterrupt;

    public int ProductAmount;

    private bool isDataLoaded;

    public ShopManager ShopManagerFood;
    public ShopManager ShopManagerClothes;
    public ShopManager ShopManagerFurniture;

    public bool IsActionNow;

    private void Awake()
    {
        Singleton = this;
    }

    void Start()
    {
        DataLoading();
    }

    void Update()
    {
        ChangingResources(Coefficients * -Time.deltaTime);
    }

    public void AddLevelPoint(int pointAmount)
    {
        CurrentLevelPoint += pointAmount;

        LevelIncrease();

        UIManager.Singleton.FillLevelSlider(CurrentLevelPoint, CurrentLevel);

        UIManager.Singleton.ChangeLevelPointText(CurrentLevelPoint);
    }

    public void LevelIncrease()
    {
        if (CurrentLevelPoint > LevelPoint[CurrentLevel])
        {
            ChangeCoinAmount(AddCoinOnCurrentLevel[CurrentLevel]);

            CurrentLevelPoint = 0;
            CurrentLevel += 1;
            LevelText.text = CurrentLevel.ToString();

            CallInstanceInterctibleButton();
        }

    }

    public void ChangeCoinAmount(int someCoin)
    {
        CoinAmount += someCoin;
        UIManager.Singleton.ChangeCoinText(CoinAmount);

        PlayerPrefs.SetInt("CoinAmount", CoinAmount);

        CallInstanceInterctibleButton();

        AnalyticsManager.Singleton.ReportBuying(someCoin);  
    }

    
    public void CallInstanceInterctibleButton()
    {
        ShopManagerFood.InstanceInterctibleButton();
        ShopManagerClothes.InstanceInterctibleButton();
        ShopManagerFurniture.InstanceInterctibleButton();
    }

    public void ChangeCrystalAmount(int someCrystal)
    {
        CrystalAmount += someCrystal;
    }

    public void ChangingCoef(Resources delta)
    {
        Coefficients.Mood += delta.Mood;
        Coefficients.Bellyful += delta.Bellyful;
        Coefficients.Rest += delta.Rest;
        Coefficients.Clean += delta.Clean;
    }

    public void ChangingResources(Resources delta)
    {
        changingMood(delta.Mood);
        changingBellyful(delta.Bellyful);
        changingRest(delta.Rest);
        changingClean(delta.Clean);
    }

    public void SaveBoughtProduct(string productKey)
    {
        int productAmount = PlayerPrefs.GetInt(productKey, 0);
        productAmount += 1;
        PlayerPrefs.SetInt(productKey, productAmount);
    }

    public int GetSavedProductAmount(string productKey)
    {
        return PlayerPrefs.GetInt(productKey, 0);
    }

    public void RemoveBoughtProduct(string productKey)
    {
        int productAmount = PlayerPrefs.GetInt(productKey, 0);
        if (productAmount >= 1)
        {
            productAmount -= 1;
            PlayerPrefs.SetInt(productKey, productAmount);
        }
        
    }

    public bool IsProductBought(string productKey)
    {       
        if (PlayerPrefs.GetInt(productKey) == 0) return false;

        return true;
    }

    //private void OnDestroy()
    //{
    //    int exitTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerSecond);
    //    PlayerPrefs.SetInt("exitTime", exitTime);
    //    PlayerPrefs.SetInt("StartLotteryTime", StartLotteryTime);

    //    PlayerPrefs.SetFloat("moodParameter", Parameters.Mood);
    //    PlayerPrefs.SetFloat("bellyfulParameter", Parameters.Bellyful);
    //    PlayerPrefs.SetFloat("restParameter", Parameters.Rest);
    //    PlayerPrefs.SetFloat("cleanParameter", Parameters.Clean);

    //    PlayerPrefs.SetFloat("moodCoef", Coefficients.Mood);
    //    PlayerPrefs.SetFloat("bellyfulCoef", Coefficients.Bellyful);
    //    PlayerPrefs.SetFloat("restCoef", Coefficients.Rest);
    //    PlayerPrefs.SetFloat("cleanCoef", Coefficients.Clean);

    //    PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
    //    PlayerPrefs.SetFloat("CurrentLevelPoint", CurrentLevelPoint);
    //}

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            DataSaving();

            
        }    

        else
        {
            DataLoading();

                      
        }
    }

    private void OnApplicationQuit()
    {
        DataSaving();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            DataLoading();

        }

        else
        {
            DataSaving();

            UIManager.Singleton.DebugLotteryText.text = "";
            UIManager.Singleton.DebugActionAfterStartText.text = "";
        }
    }

    private void DataSaving()
    {
        int exitTime = GetCurrentTime();
        PlayerPrefs.SetInt("exitTime", exitTime);
        PlayerPrefs.SetInt("StartLotteryTime", StartLotteryTime);

        PlayerPrefs.SetFloat("moodParameter", Parameters.Mood);
        PlayerPrefs.SetFloat("bellyfulParameter", Parameters.Bellyful);
        PlayerPrefs.SetFloat("restParameter", Parameters.Rest);
        PlayerPrefs.SetFloat("cleanParameter", Parameters.Clean);

        PlayerPrefs.SetFloat("moodCoef", Coefficients.Mood);
        PlayerPrefs.SetFloat("bellyfulCoef", Coefficients.Bellyful);
        PlayerPrefs.SetFloat("restCoef", Coefficients.Rest);
        PlayerPrefs.SetFloat("cleanCoef", Coefficients.Clean);

        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.SetFloat("CurrentLevelPoint", CurrentLevelPoint);

        isDataLoaded = false;
    }

    private void DataLoading()
    {
        if (isDataLoaded)
        {         
            int timeBetweenLotteries = 10800;

            int lastTime = PlayerPrefs.GetInt("exitTime");
            int currentTime = GetCurrentTime();

            int startTime = PlayerPrefs.GetInt("startTime", currentTime);

            startTime = currentTime;
            PlayerPrefs.SetInt("startTime", startTime);

            int passedTime = currentTime - lastTime;

            StartLotteryTime = PlayerPrefs.GetInt("StartLotteryTime", GetCurrentTime() - timeBetweenLotteries);

            int passedLotteryTime = currentTime - StartLotteryTime;

            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
            CurrentLevelPoint = PlayerPrefs.GetFloat("CurrentLevelPoint", 0);

            Parameters.Mood = PlayerPrefs.GetFloat("moodParameter", 80);
            Parameters.Bellyful = PlayerPrefs.GetFloat("bellyfulParameter", 80);
            Parameters.Rest = PlayerPrefs.GetFloat("restParameter", 80);
            Parameters.Clean = PlayerPrefs.GetFloat("cleanParameter", 80);

            Coefficients.Mood = PlayerPrefs.GetFloat("moodCoef", 0.005f);
            Coefficients.Bellyful = PlayerPrefs.GetFloat("bellyfulCoef", 0.007f);
            Coefficients.Rest = PlayerPrefs.GetFloat("restCoef", 0.003f);
            Coefficients.Clean = PlayerPrefs.GetFloat("cleanCoef", 0.007f);

            if (passedTime >= 0)
            {
                ChangingResources(Coefficients * -passedTime);
            }

            LevelText.text = CurrentLevel.ToString();

            CoinAmount = PlayerPrefs.GetInt("CoinAmount", 0);
            UIManager.Singleton.ChangeCoinText(CoinAmount);
            UIManager.Singleton.FillLevelSlider(CurrentLevelPoint, CurrentLevel);

            int endActionTime = PlayerPrefs.GetInt("endActionTime", startTime);
           
            int startActionTime = PlayerPrefs.GetInt("startActionTime", startTime);
        
            int exitTime = PlayerPrefs.GetInt("exitTime", 0);

            UIManager.Singleton.DebugExitTimeText.text = (exitTime - endActionTime).ToString() + " exitTime - endActionTime";
            UIManager.Singleton.DebugStartTimeText.text = (startTime - endActionTime).ToString() + " startTime - endActionTime";

            string lastRoomObjectName1 = PlayerPrefs.GetString("roomObjectName");
            string lastActionName1 = PlayerPrefs.GetString("actionName");

            //ActionManager lastAction = RoomObjectManager.Singleton.GetRoomObjectAction(lastRoomObjectName1, lastActionName1);


            

            if (endActionTime <= exitTime && endActionTime <= startTime)
            {

            }

            if (endActionTime > exitTime && endActionTime < startTime)
            {

                string lastRoomObjectName = PlayerPrefs.GetString("roomObjectName");
                string lastActionName = PlayerPrefs.GetString("actionName");

                FindLastAction();
            }

            if (endActionTime > exitTime && endActionTime > startTime)          
            {

                GhostController.Singleton.NotFinishedAction = true;

                string lastRoomObjectName = PlayerPrefs.GetString("roomObjectName");
                string lastActionName = PlayerPrefs.GetString("actionName");

                float targetObjPosX = RoomObjectManager.Singleton.GetTargetObjPos(lastRoomObjectName);

                CameraController.Singleton.StartCamera(targetObjPosX);

                int leftActionTime = endActionTime - startTime;

                int pastActionTime = startTime - startActionTime;

                GhostController.Singleton.FillSliderAfterStart(pastActionTime, leftActionTime);

                UIManager.Singleton.DebugActionAfterStartText.text = "Action end after start";

            }

            if (passedLotteryTime >= timeBetweenLotteries)
            {
                UIManager.Singleton.OpenLotteryPanel();

            }

        }

        isDataLoaded = true;
    }

    public void FindLastAction()
    {
        string lastRoomObjectName = PlayerPrefs.GetString("roomObjectName");
        string lastActionName = PlayerPrefs.GetString("actionName");

        ActionManager lastAction = RoomObjectManager.Singleton.GetRoomObjectAction(lastRoomObjectName, lastActionName);

        SetBackgroundParameters(lastAction);
    }

    public void SetBackgroundParameters(ActionManager lastAction)
    {
        float moodParameter = lastAction.MoodParameter;
        float cleanParameter = lastAction.CleanParameter;
        float bellyfulParameter = lastAction.BellyfulParameter;
        float restParameter = lastAction.RestParameter;

        changingMood(moodParameter);
        changingClean(cleanParameter);
        changingBellyful(bellyfulParameter);
        changingRest(restParameter);
    }

    public void SavePressRoomObjectName(string roomObjectName)
    {
        PlayerPrefs.SetString("roomObjectName", roomObjectName);
    }

    public void changingMood(float moodCoef)
    {
        Parameters.Mood += moodCoef;

        if (Parameters.Mood >= 100)
        {
            Parameters.Mood = 100;
        }

        if (Parameters.Mood <= 0)
        {
            Parameters.Mood = 0;
        }

        UIManager.Singleton.SliderMoodChanging(Parameters.Mood / 100);      
    }

    public void changingBellyful(float bellyfulCoef)
    {
        Parameters.Bellyful += bellyfulCoef;

        if (Parameters.Bellyful >= 100)
        {
            Parameters.Bellyful = 100;
        }

        if (Parameters.Bellyful <= 0)
        {
            Parameters.Bellyful = 0;
        }

        UIManager.Singleton.SliderBellyfulChanging(Parameters.Bellyful / 100);
    }

    public void changingRest(float restCoef)
    {
        Parameters.Rest += restCoef;

        if (Parameters.Rest >= 100)
        {
            Parameters.Rest = 100;
        }

        if (Parameters.Rest <= 0)
        {
            Parameters.Rest = 0;
        }

        UIManager.Singleton.SliderRestChanging(Parameters.Rest / 100);
    }

    public void changingClean(float cleanCoef)
    {
        Parameters.Clean += cleanCoef;

        if (Parameters.Clean >= 100)
        {
            Parameters.Clean = 100;
        }

        if (Parameters.Clean <= 0)
        {
            Parameters.Clean = 0;
        }

        UIManager.Singleton.SliderCleanChanging(Parameters.Clean / 100);
    }

    public int GetCurrentTime()
    {
        return (int)(DateTime.Now.Ticks / TimeSpan.TicksPerSecond);
    }
}






