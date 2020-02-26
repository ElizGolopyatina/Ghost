using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Product: MonoBehaviour{

    private ShopManager ShopManagerComponent;

    public GameObject ProductPrefab; 

    public int CoinPrice;
    public int CrystalPrice;

    public string Name;

    public int Index;

    public int LevelPoint;

    public int LevelAccess;

    public Resources Coefficients;

    public virtual bool ToBuy()
    {
        if (PlayerData.Singleton.CoinAmount < CoinPrice) return false;

        PlayerData.Singleton.ChangeCoinAmount(-CoinPrice);
        PlayerData.Singleton.AddLevelPoint(LevelPoint);
        PlayerData.Singleton.SaveBoughtProduct(Name);

        return true;
    }

    public bool ToBuyAndEat()
    {
        if (PlayerData.Singleton.CoinAmount < CoinPrice) return false;

        PlayerData.Singleton.ChangeCoinAmount(-CoinPrice);

        PlayerData.Singleton.AddLevelPoint(LevelPoint);

        return true;
    }

    public virtual GameObject SetUpPrefab(Transform parent)
    {
        GameObject product = Instantiate(ProductPrefab, parent);
        return product;
    }

    public void SetShopManagerComponent(ShopManager component)
    {
        ShopManagerComponent = component;
    }
}
