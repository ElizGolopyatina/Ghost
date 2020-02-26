using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupboardItem : MonoBehaviour {

    

    public void Initialization(Clothes product)
    {
        gameObject.transform.GetChild(1).GetComponent<Text>().text = product.Name;
    }
}
