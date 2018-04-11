using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : MonoBehaviour {

    static GameManager t;
    [SerializeField]
    float _weaponForceMultiplier = 5; 
    public static float WeaponForceMultiplier { get { return t._weaponForceMultiplier; } }

    [SerializeField]
    LayerMask _weaponMask; 
    public static LayerMask WeaponMask { get { return t._weaponMask; } }
    [SerializeField]
    LayerMask _groundLayer; 
    public static LayerMask GroundMask { get { return t._groundLayer; } }
    float _score;

    [SerializeField]
    LayerMask _selectable;
    public static LayerMask SelectableMask { get { return t._selectable; } }
    [SerializeField]
    int _itemLayer;
    public static int ItemLayer { get { return t._itemLayer; } }
    ISelectable _hovered; 
    public static ISelectable Hovered { get { return t._hovered; } }
    [SerializeField]
    Color _selectedColor;
    [SerializeField]
    Color _hoverColor;
    public static Color HoverColor { get { return t._hoverColor; } }
    public static Color SelectedColor { get { return t._selectedColor; } }
    [SerializeField]
    Color _containerColor; 
    public static Color ContainerColor { get { return t._containerColor; } }
    [SerializeField]
    Color _statusColor; 
    public static Color StatusColor { get { return t._statusColor; } }
    [SerializeField]
    UnityEngine.Object _bubblePrefab;
    [SerializeField]
    float _maxTurnSpeed = 10; 
    public static float MaxTurnSpeed { get { return t._maxTurnSpeed; } }
    GameObject _activeBubble;
    [SerializeField]
    LayerMask _tableAccepts; 
    public static LayerMask TableAccepts { get { return t._tableAccepts; } }
    CB _cb = new CB(); 
    public static CB CB { get { return t._cb; } }

    public static void AddPoints(float _profit)
    {
        t._score += _profit;
    }
    public static Bubble MakeBubbleUI(string _name, Transform _parent)
    {
        Bubble _bubble = (Instantiate(t._bubblePrefab) as GameObject).GetComponent<Bubble>();
        _bubble.SetUpBubble(_parent, _name);
        return _bubble; 
    }
    void Hovering()
    {
        ISelectable _nextHovered  = null; 
        if(GlobalCasts.DidFindObj)
        {
            ISelectable[] _selectables = GlobalCasts.ObjHit.collider.gameObject.GetComponentsInParent<Component>().OfType<ISelectable>().ToArray();
            _nextHovered = (_selectables.Length > 0) ? _selectables[0] : null; 
        }
        if(_hovered == _nextHovered)
        {
            return; 
        }
        if(_hovered != null)
        {
            if(_activeBubble != null)
            {
                Destroy(_activeBubble); 
            }
            _hovered.UnHovered(); 
        }
        if (_nextHovered != null)
        {
            _nextHovered.Hovered();
            _activeBubble = MakeBubbleUI(_nextHovered.Name, _nextHovered.transform).gameObject; 
        }
        _hovered = _nextHovered; 
    }
    void Activating()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Player.InteractWithItem(_hovered); 
            _hovered = null; 
            if(_activeBubble != null)
            {
                Destroy(_activeBubble); 
            }
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	void FixedUpdate()
    {
        _cb.FixedUpdate(); 
    }
	// Update is called once per frame
	void Update () {
        _cb.Update(Time.deltaTime); 
        GlobalCasts.GetCasts(); 
        Hovering();
        Activating(); 
	}
    void Awake()
    {
        t = this;
    }
}
