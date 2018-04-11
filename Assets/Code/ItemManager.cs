using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System; 

public class ItemManager : MonoBehaviour {

    static ItemManager t; 
    [SerializeField]
    List<Item> _items = new List<Item>();
    [SerializeField]
    LayerMask _itemColMask; 
    public static LayerMask ItemColMask { get { return t._itemColMask; } }
    Dictionary<Food, Item> _itemDict = new Dictionary<Food, Item>(); 

    public static Item MakeItem(Food _food)
    {
        Item _itemObj; 
        if(t._itemDict.TryGetValue(_food, out _itemObj))
        {
            return Instantiate(_itemObj.gameObject).GetComponent<Item>(); 
        }else
        {
            return null; 
        }
    }

    void Awake()
    {
        t = this; 
        _items.ForEach((_item) => _itemDict[_item.Food] = _item); 
    }
}
    
