using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class Recipe : MonoBehaviour {

    [SerializeField]
    List<Food> _ingredients = new List<Food>();
    public List<Food> Ingredients { get { return _ingredients; } }
    [SerializeField]
    Statuses[] _statuses;
    public Statuses[] Statuses { get { return _statuses; } }
    [SerializeField]
    float _duration;
    public float CookDuration { get { return _duration; } }
    [SerializeField]
    Food _output;  
    public Food Output { get { return _output;  } }
    string _key; 
    public string Key { get { return _key; } }

    public static string CreateKey(List<Food> _ingred)
    {
        return _ingred.Select((item) => item.ToString()).Aggregate((total, next) => total + next);
    }
    void Awake()
    {
        _ingredients.Sort((_a, _b)=> (int)_a - (int)_b);
        _key = CreateKey(_ingredients); 
    }
}
