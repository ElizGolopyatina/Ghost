using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : Product {

    public Sprite FoodImage;

    public override GameObject SetUpPrefab(Transform parent)
    {
        GameObject productPrefab = base.SetUpPrefab(parent);

        Image foodImage = productPrefab.transform.GetChild(4).GetComponent<Image>();

        Button toEat = productPrefab.transform.GetChild(2).GetComponent<Button>();
        Button toPut = productPrefab.transform.GetChild(3).GetComponent<Button>();

        Text foodName = productPrefab.transform.GetChild(7).GetComponent<Text>();
        Text foodPrice = productPrefab.transform.GetChild(6).GetComponent<Text>();      

        if (LevelAccess > PlayerData.Singleton.CurrentLevel || CoinPrice > PlayerData.Singleton.CoinAmount)
        {
            toEat.interactable = false;
            toPut.interactable = false;
        }

        foodImage.sprite = FoodImage;

        toEat.onClick.AddListener(ToEat);
        toPut.onClick.AddListener(ToPut);

        foodName.text = Name;
        foodPrice.text = CoinPrice.ToString();

        return productPrefab;
    }

    public void ToEat()
    {
        if (!ToBuyAndEat()) return;

        PlayerData.Singleton.ChangingResources(Coefficients);

        //ShopManagerComponent.InstanceInterctibleButton();
    }

    public void ToPut()
    {
        if (!ToBuy()) return;
    }

    public void ToEatFromFridge()
    {
        PlayerData.Singleton.ChangingResources(Coefficients);
    }
    
}
