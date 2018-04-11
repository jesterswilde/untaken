using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttackable {

    [SerializeField]
    float _heatlh = 10;
    [SerializeField]
    float _weight;
    Rigidbody _rigid; 


    public void HitBy(IWeapon _weapon)
    {
        _heatlh -= _weapon.Damage; 
        if(_heatlh <= 0)
        {
            Destroy(gameObject);
        }else
        {
            Vector3 _dir = (transform.position - Player.Position).normalized;
            _dir.y = 0.6f; 
            _rigid.AddForce(_dir * _weapon.Damage * GameManager.WeaponForceMultiplier, ForceMode.Impulse); 
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Awake()
    {
        _rigid = GetComponent<Rigidbody>(); 
    }
}
