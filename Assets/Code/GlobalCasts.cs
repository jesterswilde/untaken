using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalCasts {

    static RaycastHit _groundHit; 
    public static RaycastHit GroundHit { get { return _groundHit; } }
    static bool _didFindGround; 
    public static bool DidFindGround { get { return _didFindGround; } }
    static RaycastHit _objHit; 
    public static RaycastHit ObjHit { get { return _objHit; } }
    static bool _didFindObj; 
    public static bool DidFindObj { get { return _didFindObj; } }
    static bool _isOverGround; 
    public static bool IsOverGround { get { return _isOverGround; } }
    static GridSegment _hoveredSegment;
    static GridSegment _previousSegment; 
    public static Vector3 GroundPlacePos { get { return (_hoveredSegment != null) ? _hoveredSegment.PlaceOn() : Player.Position; } }

    public static void GetCasts()
    {
        _previousSegment = _hoveredSegment; 
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _didFindGround = Physics.Raycast(_ray, out _groundHit, 100f, GameManager.GroundMask);
        _didFindObj = Physics.Raycast(_ray, out _objHit, 100f, GameManager.SelectableMask);
        if (_didFindGround && _groundHit.collider.gameObject.layer == 8)
        {
            GridSegment _segment = _groundHit.collider.gameObject.GetComponent<GridSegment>(); 
            if(_segment == null)
            {
                _isOverGround = false;
            }else
            {
                _isOverGround = true;
                _hoveredSegment = _segment;
            }
        }else
        {
            _isOverGround = false; 
            _didFindGround = false; 
        }
        ActivateSegment(); 
    }
    static void ActivateSegment()
    {
        if(_hoveredSegment == _previousSegment)
        {
            return; 
        }
        if(_previousSegment != null)
        {
            _previousSegment.Deselect();
        }
        if(_hoveredSegment != null)
        {
            _hoveredSegment.Select(); 
        }
    }
	
}
