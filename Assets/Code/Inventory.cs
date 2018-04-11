using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    [SerializeField]
    List<Item> _items;
    [SerializeField]
    float _scrollSensitivity = 1; 
    [SerializeField]
    float _selectedPos = 0;
    int _selectedIndex = 0;
    [SerializeField]
    Item _selectedItem = null; 
    public Item SelectedItem { get { return _selectedItem; } }
    Vector3 _prevForward; 

    public void MixHeld()
    {
        if(_prevForward != transform.forward && _selectedItem != null)
        {
            _selectedItem.AddStatusToContents(Statuses.Mixing); 
        }
        _prevForward = transform.forward; 
    }
    public void Attack()
    {
        if(_selectedItem != null && _selectedItem.Weapon != null)
        {
            _selectedItem.Weapon.StartAttack(); 
        }
    }
    void SelectItem(Item _item)
    {
        _selectedItem = _item;
        _items[_selectedIndex] = _item;
        if(_item != null && _item.Weapon != null)
        {
            _item.Weapon.Selected(); 
        }
    }
    void ChangeItem()
    {
        if(_selectedItem != null)
        {
            _selectedItem.EnablePhysics(); 
        }
        _selectedIndex = Mathf.FloorToInt(_selectedPos);
        _selectedItem = _items[_selectedIndex];
        if(_selectedItem != null)
        {
            _selectedItem.DisablePhysics();  
        }
    }
    public void PlaceIntoContainer(Container _container)
    {
        if(_selectedItem != null)
        {
            _selectedItem.DropItem(_container.DropTrans.position);
            SelectItem(null); 
        }
    }
    public void PickUpItem(Item _item)
    {
        if(_selectedItem != null)
        {
            _selectedItem.DropItem(GlobalCasts.GroundPlacePos); 
        }
        SelectItem(_item); 
        if(_selectedItem != null)
        {
            _selectedItem.Activated(); 
        }
    }
    void Update()
    {
        if(_selectedItem == null || !_selectedItem.IsInUse)
        {
            _selectedPos += Input.GetAxis("Mouse ScrollWheel") * _scrollSensitivity;
            _selectedPos = (_selectedPos > _items.Count) ? 0 : _selectedPos;
            _selectedPos = (_selectedPos < 0) ? _items.Count - .1f : _selectedPos; 
            if (Mathf.FloorToInt(_selectedPos) != _selectedIndex)
            {
                ChangeItem();
            } 
        }
        MixHeld(); 
    }


    
}
