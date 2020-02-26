using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public Product[] ProductArray;

    private bool productExist;

    private List<GameObject> ExistProduct = new List<GameObject>();

    public void ProductInstatiation()
    {
        
        if (productExist == true) return;

        ShopManager panelArray = gameObject.GetComponent<ShopManager>();

  

        for (int i = 0; i < panelArray.ProductArray.Length; i++)
        {
            GameObject product = ProductArray[i].SetUpPrefab(this.transform);

            if (product == null) continue;

            ExistProduct.Add(product);

            ProductArray[i].Index = i;
            ProductArray[i].SetShopManagerComponent(this);  
            
        }

        productExist = true;

    }    

    public void DestroyProductsShopManager()
    {
        foreach (GameObject product in ExistProduct)
        {
            Destroy(product);
        }
        productExist = false;

        ShopManager panelArray = gameObject.GetComponent<ShopManager>();
    }

    public void DestroyProduct(int productIndex)
    {
        ExistProduct[productIndex].SetActive(false);
    }

    public Product[] GetProducts()
    {
          
        return ProductArray;
        
    }

    public void InstanceInterctibleButton()
    {

        DestroyProductsShopManager();

        ProductInstatiation();
    }



}

