using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedFurniture : Furniture {

    public GameObject OldItem;

    public override void ToPutOn()
    {
        base.ToPutOn();

        OldItem.SetActive(false);
    }
}

