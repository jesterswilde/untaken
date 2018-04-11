using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Item : MonoBehaviour, ISelectable, IAttackable {
    [SerializeField]
    Food _food;
    [SerializeField]
    string _nonFoodName; 
    [SerializeField]
    ChoppedTo[] _choppedTo;
    public Food Food { get { return _food; } }
    public string Name { get { return (_food != Food.none)? Util.FoodToString(_food) : _nonFoodName; } }
    bool _isVisible;
    IWeapon _weapon; 
    public IWeapon Weapon { get { return _weapon; } }
    public bool IsInUse { get { return (_weapon != null) ? _weapon.IsInUse : false; } }
    int[] _baseLayers; 
    Rigidbody _rigid;
    Container _containerChild;
    Quaternion _defaultRotation; 

    List<Collider> _colliders;
    List<Renderer> _renderers;
    ISelectable _hovered;
    Color[] _baseColors;
    bool[] _baseTriggerStates;
    [SerializeField]
    float _profit = 10; 
    public float Profit { get { return _profit; } }
    float _damageTaken = 0;
    float _boundMagnitude;
    bool _isSleeping = false;
    float _baseDrag;
    Vector3 _baseScale;
    Vector3 _boundsOffset; 

    void ChangeVisiblity(bool _visibility)
    {
        if(_visibility != _isVisible)
        {
            _colliders.ForEach((_coll) => _coll.enabled = _visibility);
            _renderers.ForEach((_rend) => _rend.enabled = _visibility); 
            _isVisible = _visibility; 
        }
    }
 
	void Awake()
    {
        _rigid = GetComponent<Rigidbody>(); 
        _renderers = GetComponentsInChildren<Renderer>().ToList();
        _renderers.Add(GetComponent<Renderer>());
        _renderers.RemoveAll((_rend) => _rend == null); 
        _colliders = GetComponentsInChildren<Collider>().ToList();
        _colliders.Add(GetComponent<Collider>());
        _colliders.RemoveAll((_coll) => _coll == null); 
        _baseColors = _renderers.Select((_rend) => (_rend.material.HasProperty("_Color")) ? _rend.material.color : Color.clear).ToArray();
        _baseLayers = _colliders.Select((_coll) => _coll.gameObject.layer).ToArray();
        _baseTriggerStates = _colliders.Select((_coll) => _coll.isTrigger).ToArray();
        _weapon = GetComponents<Component>().FirstOrDefault((_comp) => _comp is IWeapon) as IWeapon;
        _containerChild = GetComponentInChildren<Container>();
        _defaultRotation = transform.rotation;
        _baseScale = transform.localScale;
        _baseDrag = _rigid.drag;
        _boundsOffset = new Vector3(0,
            transform.position.y - _colliders.Min((_coll) => _coll.bounds.min.y), 0); 
        _boundMagnitude = _colliders.Aggregate(Mathf.NegativeInfinity, (_total, _coll) => Mathf.Max(_total, _coll.bounds.extents.magnitude));
    }

    public void Consume()
    {
        Destroy(gameObject); 
    }
    public void AddStatusToContents(Statuses _status)
    {
        if(_containerChild != null)
        {
            _containerChild.AddStatuses(_status); 
        }
    }
    public void Hovered()
    {
        _renderers.ForEach((_rend) => _rend.material.color = GameManager.SelectedColor); 
    }
    public void Hovered(Color _color)
    {
        _renderers.ForEach((_rend) => _rend.material.color = _color);
    }

    public void UnHovered()
    {
        _renderers.ForEach((_rend, i) => {
            if (_rend != null && _baseColors[i] != Color.clear) {
                _rend.material.color = _baseColors[i];
            }});
    }
    //Called by a container it is in. 
    public void FreezeItemTemporarily()
    {
        DisablePhysics();
        GameManager.CB.AddFixedCB(EnablePhysics, 3); 
    }

    public void DisablePhysics()
    {
        _rigid.isKinematic = true;
        _rigid.useGravity = false;
    }

    public void EnablePhysics()
    {
        _rigid.velocity = Vector3.zero; 
        _rigid.useGravity = true;
        _rigid.isKinematic = false;
        transform.SetParent(null); 
        transform.localScale = _baseScale;
    }
    public void Activated()
    {
        if(_containerChild != null)
        {
            _containerChild.LockContentsIn(); 
        }
        transform.position = Player.ItemPivot.position;
        transform.SetParent(Player.ItemPivot, true);
        transform.localRotation = Quaternion.identity; 
        DisablePhysics();
        UnHovered(); 
    }

    public void DropItem(Vector3 _pos)
    {
        if(_containerChild != null)
        {
            _containerChild.LockContentsIn(); 
        }
        transform.rotation = _defaultRotation;
        transform.position = _pos + _boundsOffset; 
        EnablePhysics(); 
    }
    public void BecomeItem(Food _food)
    {
        Item _item = ItemManager.MakeItem(_food);
        if(_item != null)
        {
            _item.DropItem(transform.position);
            UnHovered(); 
            Destroy(gameObject); 
        }
    }
    public void BecomeItem(ChoppedTo _chopped)
    {
        if(_chopped != null)
        {
            BecomeItem(_chopped.Food); 
        }
    }
    public void HitBy(IWeapon _weapon)
    {
        _damageTaken += _weapon.Damage;
        BecomeItem(_choppedTo.FirstOrDefault((_chop) => _damageTaken > _chop.DamageRequired)); 
    }
}
