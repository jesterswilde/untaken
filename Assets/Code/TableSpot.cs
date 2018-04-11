using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSpot : MonoBehaviour {

    /// <summary>
    /// The way containers are implemented is a quick stop gap, willl have to be re-evaluated
    /// Consider using a list for containers. 
    /// </summary>

    Customer _customer;
    [SerializeField]
    Transform _customerDestination; 
    public Vector3 CustomerDestination { get { return _customerDestination.position; } }
    Container _currentContainer;
    bool _isAvailable = true; 
    public bool IsAvailable { get { return _isAvailable; } }
    bool _customerSeated; 


    public void SpotTaken(Customer _newCustomer)
    {
        _customer = _newCustomer;
        _isAvailable = false; 
    }
    public void SitDown()
    {
        _customerSeated = true; 
        if(_currentContainer != null)
        {
            _customer.ContainerRecieved(_currentContainer); 
        }
    }
    public void SpotVacant()
    {
        _customer = null;
        _isAvailable = true;
        _customerSeated = false; 
    }
    void SetContainer(Container _cont)
    {
        _currentContainer = _cont; 
        if(_customer != null && _customerSeated)
        {
            _customer.ContainerRecieved(_cont);
        }
    }

    void SetFood(Item _item)
    {
        if (_item.Food.IsFood())
        {
            _customer.ItemRecieved(_item); 
        }
    }
    void RemoveContainer(Container _cont)
    {
        _currentContainer = null; 
    }
    void Start()
    {
        CustomerManager.RegisterSpot(this); 
    }
    void OnTriggerEnter(Collider _coll)
    {
        Container _cont =  _coll.gameObject.GetComponent<Container>();
        if (_cont != null)
        {
            SetContainer(_cont);
            return; 
        }
        Item _item = _coll.gameObject.GetComponent<Item>(); 
        if(_item != null && _item.Food.IsFood())
        {
            SetFood(_item); 
        }
    }
}
