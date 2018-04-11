using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Container : MonoBehaviour, ISelectable {

    [SerializeField]
    string _name; 
    public string Name { get { return _name; } }
    List<Item> _items = new List<Item>();
    public List<Item> Items { get { return _items; } }
    List<Statuses> _statuses = new List<Statuses>();
    float _progress = 0;
    Recipe _currentlyCooking = null;
    Item _itemParent;
    [SerializeField]
    Transform _dropPos; 
    public Transform DropTrans { get { return _dropPos; } }
    Renderer[] _renderers;
    Color[] _baseColors;

    void CheckWhatImCooking()
    {
        if(_items.Count > 0)
        {
            _progress = 0; 
            Recipe _recipe = RecipeManager.WhatAmICooking(_items.Select((_item)=> _item.Food).ToList());
            if (_recipe != null)
            {
                _currentlyCooking = _recipe; 
            }
        }
    }
    void OnTriggerEnter(Collider _coll)
    {
        Item _item = _coll.gameObject.GetComponentInParent<Item>();
        if (_item != null && !_items.Contains(_item))
        {
            _items.Add(_item);
            CheckWhatImCooking(); 
        }
    }
    void OnTriggerExit(Collider _coll)
    {
        Item _item = _coll.gameObject.GetComponentInParent<Item>();
        if(_item != null)
        {
            Debug.Log("removed item"); 
            _items.Remove(_item);
            CheckWhatImCooking(); 
        }
    }
    public void AddStatuses(Statuses _status)
    {
        _statuses.Add(_status); 
    }
    public void LockContentsIn()
    {
        _items.ForEach((_child) =>
        {
            _child.transform.SetParent(transform, true);
            _child.FreezeItemTemporarily();
        });
    }
    void Update()
    {
        if(_items.Count > 0 && _statuses.Count > 0 && _currentlyCooking != null)
        {
            if(_currentlyCooking.Statuses.All((_status) => _statuses.Contains(_status)))
            {
                _progress += Time.deltaTime; 
                if(_progress > _currentlyCooking.CookDuration)
                {
                    _items.ForEach((_usedItem) => Destroy(_usedItem.gameObject)); 
                    _items.Clear();
                    Item _item = ItemManager.MakeItem(_currentlyCooking.Output);
                    _item.DropItem(_dropPos.position); 
                    _currentlyCooking = null;
                    _progress = 0; 
                }
            } 
        }
        _statuses.Clear(); 
    }
    void Awake()
    {
        _itemParent = GetComponentInParent<Item>();
        _renderers = GetComponentsInChildren<Renderer>();
        _baseColors = _renderers.Select((_rend) => _rend.material.color).ToArray();

    }

    public void Hovered()
    {
        if(_itemParent != null)
        {
            _itemParent.Hovered(GameManager.ContainerColor);
        }else
        {
            _renderers.ForEach((_rend) => _rend.material.color = GameManager.ContainerColor);
        }
    }

    public void UnHovered()
    {
        if(_itemParent != null)
        {
            _itemParent.UnHovered(); 
        }
        else
        {
            _renderers.ForEach((_rend, i) => _rend.material.color = _baseColors[i]);
        }
    }

    public void Activated()
    {

    }
}
