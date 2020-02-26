using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fridge : MonoBehaviour {

    public static Fridge Singleton;

    public GameObject FridgePanel;
    public GameObject FoodInPanel;

    public ShopManager ShopManagerComp;

    public GameObject FridgeProduct;

    private List<GameObject> FridgeExistProduct = new List<GameObject>();

    private void Awake()
    {
        Singleton = this;
    }

    private void OnMouseDown()
    {
        if (UIManager.Singleton.EnythingOpen == true) return;

        else
        {
            UIManager.Singleton.OpenEnything();

            FridgePanel.SetActive(true);
            AddBoughtFood();
        }

        
    }

    public void AddBoughtFood()
    {

        Product[] fridgeItem = ShopManagerComp.GetProducts();

        foreach (Product food in fridgeItem)
        {
            if (PlayerData.Singleton.IsProductBought(food.Name))
            {
                GameObject boughtFood = Instantiate(FridgeProduct, FoodInPanel.transform);

                FridgeExistProduct.Add(boughtFood);

                FoodInFridgeItem item = boughtFood.GetComponent<FoodInFridgeItem>();

                item.Initialization(food as Food);
            }
        }
    }

    public void DestroyFood()
    {
        foreach (GameObject food in FridgeExistProduct)
        {
            Destroy(food);
        }
    }
}
