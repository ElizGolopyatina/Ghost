using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furniture : Product {

    private GameObject productPrefab;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(gameObject.name, 0) == 0) gameObject.SetActive(false);
        else gameObject.SetActive(true);        
    }


    public override GameObject SetUpPrefab(Transform parent)
    {

        if (!PlayerData.Singleton.IsProductBought(Name))
        {
            productPrefab = base.SetUpPrefab(parent);

            Button toPutOn = productPrefab.transform.GetChild(0).GetComponent<Button>();
           
            Text furnitureName = productPrefab.transform.GetChild(1).GetComponent<Text>();
            Text furniturePrice = productPrefab.transform.GetChild(2).GetComponent<Text>();

            if (LevelAccess > PlayerData.Singleton.CurrentLevel || CoinPrice > PlayerData.Singleton.CoinAmount)
            {
                toPutOn.interactable = false;
            }

            toPutOn.onClick.AddListener(ToPutOn);

            furnitureName.text = Name;
            furniturePrice.text = CoinPrice.ToString();

            return productPrefab;
        }

        else return null;

    }

    public virtual void ToPutOn()
    {
        if (!ToBuy()) return;

        gameObject.SetActive(true);

        Destroy(productPrefab);

        PlayerPrefs.SetInt(gameObject.name, 1);

        PlayerData.Singleton.ChangingCoef(Coefficients);

        //ShopManagerComponent.InstanceInterctibleButton();
    }
}
