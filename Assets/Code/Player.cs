using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System; 

public class Player : MonoBehaviour {

    static Player t; 
    [SerializeField]
    float _moveSpeed = 5; 
    public float Speed { get { return _moveSpeed; } set { _moveSpeed = value;} }
    [SerializeField]
    Transform _itemPivot;

    [SerializeField]
    Transform _collParent; 
    [SerializeField]
    CollChecker _forward;
    [SerializeField]
    CollChecker _right;
    [SerializeField]
    CollChecker _back;
    [SerializeField]
    CollChecker _left;
    PlayerColl _playerColl;
    Inventory _inventory;
    Animator _anim; 
    public static Transform ItemPivot { get { return t._itemPivot; } }
    public static int Layer { get { return t.gameObject.layer; } }
    public static Vector3 Position { get { return t.transform.position; } }
    Rigidbody _rigid;
    

	// Use this for initialization
	void Awake () {
        t = this;
        _rigid = GetComponent<Rigidbody>(); 
        _inventory = GetComponent<Inventory>();
        _anim = GetComponentInChildren<Animator>();
        _playerColl = new PlayerColl(_collParent, _forward, _right, _back, _left);
	}

    public static void PlayAnim(string _anim)
    {
        t._anim.Play(_anim); 
    }
    public static void TriggerAnim(string _anim)
    {
        t._anim.SetTrigger(_anim); 
    }
    public static void InteractWithItem(ISelectable _selectable)
    {
        if (_selectable == null)
        {
            t._inventory.PickUpItem(null);
        }
        else if(_selectable is Item)
        {
            t._inventory.PickUpItem(_selectable as Item); 
        }
        else if(_selectable is Container)
        {
            t._inventory.PlaceIntoContainer(_selectable as Container); 
        }
        else
        {
            t._inventory.PickUpItem(null); 
        }
    }

	// Update is called once per frame
	void Update () {
        PlayerMovement.ShouldAttack(()=> _inventory.Attack());
	}
    void FixedUpdate()
    {
        //_rigid.velocity = PlayerMovement.CalcMovement(_moveSpeed, Time.fixedDeltaTime);
        _playerColl.Update(transform.position); 
        transform.position += PlayerMovement.CalcMovement(_moveSpeed, Time.fixedDeltaTime,
            _forward, _right, _back, _left);
        transform.Rotate(new Vector3(0,PlayerMovement.CalcFacing(this),0));
    }
    void OnCollisionEnter(Collision _coll)
    {
        if (_coll.gameObject.layer == 15 || _coll.gameObject.layer == 8)
        {
            _rigid.velocity = Vector3.zero; 
        }
    }
}
