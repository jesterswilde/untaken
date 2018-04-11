using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class MeleeWeapon : MonoBehaviour, IWeapon {

    [SerializeField]
    string _animName;
    [SerializeField]
    float _speed;
    [SerializeField]
    float _damage;
    public float Damage { get { return _damage; } }
    [SerializeField]
    float _timeBetweenSwings = 2; 
    bool _isAttacking = false;
    public bool IsInUse { get { return _isAttacking; } }
    float _timeSinceSwing = 0;
    Container _childContainer; 

    List<IAttackable> _thingsHit;  

    public void Selected()
    {
        if(_animName != "")
        {
            Player.PlayAnim(_animName); 
        }
        else
        {
            Player.PlayAnim("club"); 
        }
    }
    public void StartAttack()
    {
        if (!_isAttacking)
        {
            _thingsHit.Clear(); 
            _isAttacking = true;
            Player.TriggerAnim("Attack");
            /*
            if(_childContainer != null)
            {
                _childContainer.LockChildren(false); 
            } */
        }
    }
     void EndAttack()
    {
        if (_isAttacking)
        {
            _timeSinceSwing += Time.deltaTime;
            if(_timeSinceSwing > _timeBetweenSwings)
            {
                /*
                if(_childContainer != null)
                {
                    _childContainer.LockChildren(true); 
                }*/
                _timeSinceSwing = 0;
                _isAttacking = false; 
            }
        }
    }

    void OnCollisionEnter(Collision _coll)
    {
        if(_isAttacking && (GameManager.WeaponMask == (GameManager.WeaponMask| (1 << _coll.gameObject.layer))))
        {
            IAttackable _hit = _coll.gameObject.GetComponentsInParent<Component>().OfType<IAttackable>().FirstOrDefault();
            if (_hit != null &&  !_thingsHit.Contains(_hit))
            {
                _thingsHit.Add(_hit);
                _hit.HitBy(this); 
            }
        }
    }
    void Awake()
    {
        _childContainer = GetComponentInChildren<Container>(); 
    }
    // Use this for initialization
    void Start () {
        _thingsHit = new List<IAttackable>(); 
	}
	
	// Update is called once per frame
	void Update () {
        EndAttack(); 
	}
}
