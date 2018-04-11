using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GridChecker : MonoBehaviour {
    BoxCollider _coll;
    List<Collider> _overlapping = new List<Collider>(); 

    public bool IsOccupied()
    {
        return (_overlapping.Count > 0); 
    }
    public Vector3 Position { get {
            return new Vector3(transform.position.x,
            transform.position.y - _coll.size.y * 0.5f, transform.position.z); } }

    void Awake()
    {
        _coll = GetComponent<BoxCollider>(); 
        if(_coll == null)
        {
            _coll = gameObject.AddComponent<BoxCollider>(); 
        }
        _coll.isTrigger = true; 
    }
    public void OnTriggerEnter(Collider _other)
    {
        if (!_overlapping.Contains(_other))
        {
            _overlapping.Add(_other); 
        }
    }
    public void OnTriggerExit(Collider _other)
    {
        _overlapping.Remove(_other); 
    }
}
