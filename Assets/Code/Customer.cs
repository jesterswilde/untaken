using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour, IAttackable {

    Item _desiredFood;
    TableSpot _spot;
    NavMeshAgent _agent;
    Bubble _bubble;
    Rigidbody _rigid; 
    bool _goingToSeat = true; 


    public string DecideOnFood()
    {
        _desiredFood = Menu.RandomItem();
        string _foodName = Util.FoodToString(_desiredFood.Food); 
        _bubble = GameManager.MakeBubbleUI(_foodName, transform);
        return Util.FoodToString(_desiredFood.Food); 
    }
    public void AssignSpot(TableSpot _newSpot)
    {
        if(_newSpot != null)
        {
            _spot = _newSpot;
            _spot.SpotTaken(this); 
            _agent.destination = Util.SetYToMyY(_newSpot.CustomerDestination,transform.position); 
        }
        else
        {
            RemoveSelf(); 
        }
    }
    void RemoveSelf()
    {
        if(_spot != null)
        {
            _spot.SpotVacant(); 
        }
        CustomerManager.RemoveCustomer(this);
        if (_bubble != null)
        {
            Destroy(_bubble.gameObject);
        }
        Destroy(gameObject); 
    }
    public void ContainerRecieved(Container _cont)
    {
        ItemRecieved(_cont.Items.FirstOrDefault((_item) => _item.Name == _desiredFood.Name)); 
    }
    public void ItemRecieved(Item _item)
    {
        if(_item != null && _item.Name == _desiredFood.Name)
        {
            GameManager.AddPoints(_desiredFood.Profit);
            _item.Consume(); 
            Debug.Log("Got Points : " + _desiredFood.Profit);
        }else
        {
            Debug.Log("This was not what was ordered, go fuck yourself"); 
        }
        RemoveSelf(); 
    }
    void Update()
    {
        if(_goingToSeat && (_agent.destination - transform.position).sqrMagnitude < 0.01f)
        {
            _agent.Stop();
            _spot.SitDown();
            _rigid.velocity = Vector3.zero; 
            _goingToSeat = false;
        }
    }
    void Awake()
    {
        _rigid = GetComponent<Rigidbody>(); 
        _agent = GetComponent<NavMeshAgent>();
    }

    public void HitBy(IWeapon _weapon)
    {
        Item _chunk = ItemManager.MakeItem(Food.humanChunk);
        _chunk.DropItem(transform.position + Vector3.up);
        RemoveSelf(); 
    }
}
