using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System; 

public static class Util  {
    public static void ForI(Action<int> _action, int _times)
    {
        for(int i = 0; i < _times; i++)
        {
            _action(i); 
        }
    }
    static TextInfo TI = new CultureInfo("en-US", false).TextInfo;
    public static string FoodToString(Food _food)
    {
        string result = Regex.Replace(_food.ToString(), "([a-z])([A-Z])", (Match _match) => _match.Groups[1] + " " +  _match.Groups[2]);
        return TI.ToTitleCase(result); 
    }

    public static Vector3 SetYToMyY(Vector3 _vec, Vector3 _myVec)
    {
       _vec.y = _myVec.y;
       return _vec; 
    }
    public static Vector3 RemoveY(Vector3 _vec)
    {
        _vec.y = 0;
        return _vec; 
    }
}
public static class MaskExten
{
    public static bool HasLayer(this LayerMask _mask, int _layer)
    {
        return ((1 << _layer) & _mask) == _mask;
    }
}
public static class EnumExten
{
    
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> _list, Action<T, int> _action)
    {
        int i = 0; 
        foreach(T _item in _list)
        {
            _action(_item, i++); 
        }
        return _list; 
    }
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> _list, Action<T> _action)
    {
        foreach (T _item in _list)
        {
            _action(_item);
        }
        return _list;
    }
}
