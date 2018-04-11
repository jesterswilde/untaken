using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;

[Serializable]
public class ChoppedTo {

    [SerializeField]
    Food _food;
    public Food Food{ get { return _food; } }
    [SerializeField]
    float _damage; 
    public float DamageRequired { get { return _damage; } }
}
