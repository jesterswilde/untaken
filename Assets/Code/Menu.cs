using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    static Menu t;
    [SerializeField]
    Item[] _menuItems; 


    void Awake()
    {
        t = this; 
    }

    public static Item RandomItem()
    {
        return t._menuItems[Random.Range(0, t._menuItems.Length)]; 
    }
}
