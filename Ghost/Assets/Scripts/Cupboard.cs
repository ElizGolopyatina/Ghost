using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cupboard : MonoBehaviour {

    public static Cupboard Singleton;

    public GameObject CupboardPanel;

    public GameObject ClothesInPanel;

    public Clothes[] BoughtClothes;

    public ShopManager ShopManagerComp;

    public GameObject CupboardProduct;

    private List<GameObject> CupboardExistProduct = new List<GameObject>();

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

            CupboardPanel.SetActive(true);
            AddBoughtClothes();
        }
            
    }

    public void AddBoughtClothes()
    {
        Product[] cupboardItem = ShopManagerComp.GetProducts();

        foreach (Product product in cupboardItem)
        {
            if (PlayerData.Singleton.IsProductBought(product.Name))
            {
                GameObject boughtProduct = Instantiate(CupboardProduct, ClothesInPanel.transform);

                CupboardExistProduct.Add(boughtProduct);

                CupboardItem item = boughtProduct.GetComponent<CupboardItem>();

                item.Initialization(product as Clothes);
            }
        }
    }

    public void DestroyClothes()
    {
        foreach (GameObject clothes in CupboardExistProduct)
        {
            Destroy(clothes);
        }
    }
    
}
