using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GhostSlider GhostSliderComponent;

    public static UIManager Singleton;

    public Image MoodSlider;
    public Image BellyfulSlider;
    public Image RestSlider;
    public Image CleanSlider;

    public Image LevelSlider;

    public GameObject ShopPanel;

    public GameObject[] ProductPanel;
    public GameObject[] ScrollPanel;
    public GameObject[] ConnectionImg;

    public int[] LotteryPrize = new int[] {150, 300, 400, 500, 600, 700, 800, 900, 1000, 2000};

    private GameObject LastPanel;
    private GameObject CurrentPanel;
    private GameObject CurrentScroll;
    private GameObject LastConnectionImg;

    public GameObject LotteryPanel;
    public GameObject YourWinnings;
    public Text Winnings;

    public Text CoinAmount;
    public Text LevelPointAmount;

    public GameObject CupboardPanel;
    public GameObject FridgePanel;

    public bool EnythingOpen;

    public Text DebugStartTimeText;
    public Text DebugExitTimeText;
    public Text DebugEndActionTimeText;

    public Text DebugLotteryText;
    public Text DebugActionAfterStartText;
    public Text DebugSliderAmountText;

    public Button LotteryPlay;

    private void Awake()
    {
        Singleton = this;
    }

    public void ChangeCoinText(int coinAmount)
    {
        CoinAmount.text = coinAmount.ToString();
    }

    public void ChangeLevelPointText(float pointAmount)
    {
        LevelPointAmount.text = pointAmount.ToString();
    }

    public void SliderMoodChanging(float deltaMood)
    {
        MoodSlider.fillAmount = deltaMood;
    }

    public void SliderBellyfulChanging(float deltaBellyful)
    {
        BellyfulSlider.fillAmount = deltaBellyful;
    }

    public void SliderRestChanging(float deltaRest)
    {
        RestSlider.fillAmount = deltaRest;
    }

    public void SliderCleanChanging(float deltaClean)
    {
        CleanSlider.fillAmount = deltaClean;
    }

    public void OpenShop()
    {
        if (EnythingOpen == true) return;

        else
        {
            ShopPanel.SetActive(true);

            PanelOpen(0);

            OpenEnything();
        }
        
    }

    public void PanelOpen(int index)
    {
        CurrentPanel = ProductPanel[index];
        CurrentScroll = ScrollPanel[index];
        CurrentScroll.SetActive(true);
        ConnectionImg[index].SetActive(true);
        
        if (LastPanel != null) LastPanel.SetActive(false);
        if (LastConnectionImg != null) LastConnectionImg.SetActive(false);

        ProductPanel[index].GetComponent<ShopManager>().ProductInstatiation();

        LastPanel = ScrollPanel[index];
        LastConnectionImg = ConnectionImg[index];
    }

    public void CloseShop()
    {
        ShopPanel.SetActive(false);

        DestroyProducts();

        ScrollPanel[0].SetActive(false);
        ScrollPanel[1].SetActive(false);
        ScrollPanel[2].SetActive(false);

        LastPanel = null;

        CloseEnything();
    }

    public void DestroyProducts()
    {
        foreach (GameObject product in ProductPanel)
        {
            product.GetComponent<ShopManager>().DestroyProductsShopManager();
        }
    }

    public void OpenLotteryPanel()
    {
        LotteryPanel.SetActive(true);
        LotteryPlay.interactable = true;

        OpenEnything();
    }

    public void LotteryPlaying()
    {
        int prize =  Random.Range(50, LotteryPrize[PlayerData.Singleton.CurrentLevel]);
        YourWinnings.SetActive(true);
        Winnings.text = prize.ToString();
        PlayerData.Singleton.ChangeCoinAmount(prize);
        
        PlayerData.Singleton.AddLevelPoint(5);

        PlayerData.Singleton.StartLotteryTime = PlayerData.Singleton.GetCurrentTime();

        LotteryPlay.interactable = false;
    }

    public void CloseLotteryPanel()
    {
        LotteryPanel.SetActive(false);

        Winnings.text = "";

        CloseEnything();
    }

    public void CloseCupboardPanel()
    {
        CupboardPanel.SetActive(false);

        Cupboard.Singleton.DestroyClothes();

        CloseEnything();
    }

    public void CloseFridgePanel()
    {
        FridgePanel.SetActive(false);

        Fridge.Singleton.DestroyFood();

        CloseEnything();
    }

    public void EndAction()
    {
        GhostSliderComponent.fill = false;
        GhostSliderComponent.fillAfterStart = false;
        GhostSliderComponent.GhostCanvas.SetActive(false);
        GhostController.Singleton.Stop = false;

        PlayerData.Singleton.ActionWasInterrupt = true;

        int endActionTime = PlayerData.Singleton.GetCurrentTime();
        PlayerPrefs.SetInt("endActionTime", endActionTime);
     
        PlayerData.Singleton.IsActionNow = false;
        Debug.Log("end action");
    }

    public void FillLevelSlider(float pointAmount, int currentLevel)
    {
        int maxLevelPoint = PlayerData.Singleton.LevelPoint[currentLevel];
        
        LevelSlider.fillAmount = (pointAmount / maxLevelPoint);
    }

    public void OpenEnything()
    {
        EnythingOpen = true;
    }

    public void CloseEnything()
    {
        EnythingOpen = false;
    }

    public void ToExit()
    {
        Application.Quit();
    }
}
