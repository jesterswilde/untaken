using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    static UIManager t;

    Dictionary<Customer, OrderUI> _customerOrders = new Dictionary<Customer, OrderUI>(); 

    [SerializeField]
    Transform _orderParent;
    [SerializeField]
    OrderUI _orderPrefab; 

    public static void MakeUIOrder(Customer _customer, string _orderString)
    {
        OrderUI _order = Instantiate(t._orderPrefab).GetComponent<OrderUI>();
        _order.SetText(_orderString);
        _order.transform.SetParent(t._orderParent);
        t._customerOrders.Add(_customer, _order); 
    }
    public static void RemoveOrderUI(Customer _customer)
    {
        Destroy(t._customerOrders[_customer].gameObject);
        t._customerOrders.Remove(_customer); 
    }

	void Awake()
    {
        t = this; 
    }
}
