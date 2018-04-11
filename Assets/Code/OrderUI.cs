using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class OrderUI : MonoBehaviour {



    public void SetText(string _item)
    {
        Text _text = GetComponentInChildren<Text>(); 
        _text.text = _item; 
    }
}
