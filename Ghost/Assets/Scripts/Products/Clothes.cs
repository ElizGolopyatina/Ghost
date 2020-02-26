using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clothes : Product {

    public GameObject CupboardProduct;
    public GameObject CupboardPanel;

    private GameObject productPrefab;

    public override GameObject SetUpPrefab(Transform parent)
    {
        if (!PlayerData.Singleton.IsProductBought(Name))

        {
            productPrefab = base.SetUpPrefab(parent);

            Button toTryOn = productPrefab.transform.GetChild(0).GetComponent<Button>();
            Button toPutIn = productPrefab.transform.GetChild(1).GetComponent<Button>();

            Text clothesName = productPrefab.transform.GetChild(2).GetComponent<Text>();
            Text clothesPrice = productPrefab.transform.GetChild(3).GetComponent<Text>();

            if (LevelAccess > PlayerData.Singleton.CurrentLevel || CoinPrice > PlayerData.Singleton.CoinAmount)
            {
                toTryOn.interactable = false;
                toPutIn.interactable = false;
            }

            toTryOn.onClick.AddListener(ToTryOn);
            toPutIn.onClick.AddListener(ToPutIn);

            clothesName.text = Name;
            clothesPrice.text = CoinPrice.ToString();

            return productPrefab;
        }

        else return null;
        
    }

    public void ToTryOn()
    {

    }

    public void ToPutIn()
    {
        if (!ToBuy()) return;

        PlayerData.Singleton.ChangingResources(Coefficients);

        Destroy(productPrefab);

    }

    public void ToPutOn()
    {

    }
}
