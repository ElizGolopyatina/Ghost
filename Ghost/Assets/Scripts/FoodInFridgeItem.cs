using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodInFridgeItem : MonoBehaviour {

    private Food LinkedProduct;

    public Text ProductAmount;

    private void Start()
    {
        ProductAmount.text = PlayerData.Singleton.GetSavedProductAmount(LinkedProduct.Name).ToString();
    }

    public void Initialization(Food product)
    {
        gameObject.transform.GetChild(1).GetComponent<Text>().text = product.Name;

        LinkedProduct = product;

        GetComponent<Button>().onClick.AddListener(AnotherMethod);
    }

    public void AnotherMethod()
    {
        LinkedProduct.ToEatFromFridge();

        PlayerData.Singleton.RemoveBoughtProduct(LinkedProduct.Name);

        ProductAmount.text = PlayerData.Singleton.GetSavedProductAmount(LinkedProduct.Name).ToString();

        if (PlayerData.Singleton.GetSavedProductAmount(LinkedProduct.Name) == 0)
        {
            Destroy(gameObject);
        }
    }
}
