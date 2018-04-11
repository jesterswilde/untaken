using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class CustomerManager : MonoBehaviour {
    [SerializeField]
    UnityEngine.Object _prefab;
    static CustomerManager t; 
    [SerializeField]
    Transform _doorEntrance;
    List<TableSpot> _tableSpots;
    [SerializeField]
    Vector2 _customerFrequencyRange;
    [SerializeField]
    AudioClip _customerEntering; 

    float _timeUntilNextCustomer = -1f; 

    public static void RegisterSpot(TableSpot _spot)
    {
        t._tableSpots.Add(_spot); 
    }
    float RollTime()
    {
        return Random.Range(_customerFrequencyRange.x, _customerFrequencyRange.y); 
    }
    TableSpot PickSpot()
    {
        return _tableSpots.FirstOrDefault((_table) => _table.IsAvailable); 
    }
    void CreateCustomer()
    {
        SFX.Create(_customerEntering, _doorEntrance.position); 
        Customer _customer = ((GameObject) Instantiate(_prefab, _doorEntrance.position, _doorEntrance.rotation)).GetComponent<Customer>();
        _customer.AssignSpot(PickSpot());
        string _dishName =  _customer.DecideOnFood();
        UIManager.MakeUIOrder(_customer, _dishName);
    }
    public static void RemoveCustomer(Customer _customer)
    {
        UIManager.RemoveOrderUI(_customer);
    }

    void Countdown()
    {
        _timeUntilNextCustomer -= Time.deltaTime; 
        if(_timeUntilNextCustomer < 0)
        {
            CreateCustomer();
            _timeUntilNextCustomer = RollTime(); 
        }
    }
    void Update()
    {
        Countdown();
    }
    void Awake()
    {
        t = this;
        _tableSpots = new List<TableSpot>(); 
    }
}
