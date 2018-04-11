using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerMovement {


	public static Vector3 CalcMovement(float _speed, float _delta, CollChecker _forward, CollChecker _right, 
        CollChecker _back, CollChecker _left)
    {
        Vector3 _camForward = Camera.main.transform.forward;
        _camForward = new Vector3(_camForward.x, 0, _camForward.z).normalized;
        Vector3 _camRight = Camera.main.transform.right;
        _camRight = new Vector3(_camRight.x, 0, _camRight.z).normalized;
        Vector3 _input = new Vector3();

        if (Input.GetKey(KeyCode.W) && !_forward.IsBlocked)
        {
            _input.z += 1; 
        }
        if (Input.GetKey(KeyCode.A) && !_left.IsBlocked)
        {
            _input.x -= 1;
        }
        if (Input.GetKey(KeyCode.S) && !_back.IsBlocked)
        {
            _input.z -= 1;
        }
        if (Input.GetKey(KeyCode.D) && !_right.IsBlocked)
        {
            _input.x += 1;
        }
        _camForward *= _input.z;
        _camRight *= _input.x; 
        return (_camRight + _camForward) * _speed * _delta; 
    }

    public static float CalcFacing(Player _player)
    {
        Ray _mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (GlobalCasts.DidFindGround) {
            Vector3 _facing = GlobalCasts.GroundHit.point - _player.transform.position;
            _facing.y = 0;
            _facing.Normalize();
            float _angle = Vector3.Angle(_player.transform.forward, _facing);
            _angle = (Vector3.Cross(_player.transform.forward, _facing).y > 0) ? 
                Mathf.Min(_angle, GameManager.MaxTurnSpeed) : Mathf.Max(-_angle, -GameManager.MaxTurnSpeed);
            return _angle; 
        }
        return 0; 
    }
    public static void ShouldAttack(Action _attackCB)
    {
        if (Input.GetMouseButton(1))
        {
            _attackCB(); 
        }
    }
}
