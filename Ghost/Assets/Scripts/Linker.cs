using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour {

    public static Linker Singleton;

    public bool EnythingOpen;

    private void Awake()
    {
        Singleton = this;
    }
}
