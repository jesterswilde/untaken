using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour {


    Inventory _inventory; 

    void Awake()
    {
        _inventory = GetComponentInParent<Inventory>(); 
    }
	public void LockContents()
    {

    }
    public void UnlockContents()
    {

    }
}
