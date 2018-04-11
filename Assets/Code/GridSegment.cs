using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSegment : MonoBehaviour {

    BoxCollider _coll;
    float _segmentHeight = 0.5f;
    float _totalHeight = 5;
    int _layer = 15;
    Renderer _rend;
    Color _baseColor;
    List<GridChecker> _checkers = new List<GridChecker>();


    void Awake()
    {
        _coll = GetComponent<BoxCollider>();
        _rend = GetComponent<Renderer>();
        _baseColor = _rend.material.color; 
        MakeCaptureColldiers(); 
    }

    public Vector3 PlaceOn()
    {
        return _checkers.Find((_checker) => !_checker.IsOccupied()).Position; 
    }
    public void Select()
    {
        _rend.material.color = _baseColor * 1.3f; 
    }
    public void Deselect()
    {
        _rend.material.color = _baseColor; 
    }
    void MakeCaptureColldiers()
    {
        Util.ForI((i) =>
        {
            GameObject _go = new GameObject();
            _go.transform.SetParent(transform);
            _go.transform.position = transform.position;
            BoxCollider _childColl = _go.AddComponent<BoxCollider>();
            GridChecker _checker = _go.AddComponent<GridChecker>();
            _childColl.isTrigger = true;
            _childColl.size = new Vector3(transform.lossyScale.x, _segmentHeight, transform.lossyScale.z);
            _go.transform.position = new Vector3(transform.position.x,
                transform.position.y + (_segmentHeight + transform.lossyScale.y) * 0.5f + i * _segmentHeight,
                transform.position.z);
            _go.layer = 16;
            _checkers.Add(_checker); 
        }, Mathf.FloorToInt(_totalHeight / _segmentHeight)); 

    }
}
