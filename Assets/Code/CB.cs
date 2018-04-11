using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 


//I am making this so I can store actions and time in one list. 
public class DoIn
{
    public Action Action;
    public float RemainingTime { get { return _remainingTime; } }
    float _remainingTime; 
    public void PassTime(float _delta)
    {
        _remainingTime -= _delta; 
    }
    public DoIn(Action _action, float _delay)
    {
        Action = _action;
        _remainingTime = _delay;
    }
}
public class CB  {
    //We have a list of the above structs
    List<DoIn> _cbs = new List<DoIn>();
    List<DoIn> _fixedCBs = new List<DoIn>(); 
    public void AddCB(Action _action, float _delay)
    {
        _cbs.Add(new DoIn(_action, _delay));
    }
    public void AddFixedCB(Action _action, float _delay)
    {
        _fixedCBs.Add(new DoIn(_action, _delay));
    }

    public void FixedUpdate()
    {
        bool _shouldFilter = false;
        _fixedCBs.ForEach((_cb) =>
        {
            _cb.PassTime(1);
            if (_cb.RemainingTime < 0)
            {
                _cb.Action();
                _shouldFilter = true;
            }
        });
        if (_shouldFilter)
        {
            _fixedCBs = _fixedCBs.Where((_cb) => _cb.RemainingTime >= 0).ToList();
        }
    }
    public void Update(float _delta)
    {
        bool _shouldFilter = false; 
        _cbs.ForEach((_cb) =>
        {
            _cb.PassTime(_delta);
            Debug.Log(_cb.RemainingTime + " | " + _delta); 
            if (_cb.RemainingTime < 0)
            {
                _cb.Action();
                 _shouldFilter = true; 
            }
        });
        if (_shouldFilter)
        {
           _cbs = _cbs.Where((_cb) => _cb.RemainingTime > 0).ToList(); 
        }
    }
	
}
