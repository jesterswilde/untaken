using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColl {

    CollChecker _forward;
    CollChecker _right;
    CollChecker _back;
    CollChecker _left;
    Transform _trans; 

    Vector3 _camForward;  

    public PlayerColl(Transform _swivelTrans, CollChecker forward, CollChecker right, CollChecker back, CollChecker left)
    {
        _trans = new GameObject().transform;
        _trans = _swivelTrans; 
        _forward = forward;
        _right = right;
        _back = back;
        _left = left;
    }

    public void Update(Vector3 _playerPos)
    {
        Vector3 _camForward = Camera.main.transform.forward;
        _camForward = new Vector3(_camForward.x, 0, _camForward.z).normalized;
        _trans.forward = _camForward;
        _trans.position = _playerPos; 
    }
}
