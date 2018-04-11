using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon {

    void StartAttack();
    bool IsInUse { get; }
    float Damage { get; }
    void Selected(); 
}
