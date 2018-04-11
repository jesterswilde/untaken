using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class CollChecker : MonoBehaviour {
    List<Collider> _colls = new List<Collider>();
    [SerializeField]
    LayerMask _mask; 

    public bool IsBlocked { get { return _colls.Count > 0; } }

    void OnTriggerEnter(Collider _coll)
    {
        if(!_colls.Contains(_coll))
        {
            _colls.Add(_coll);
        }
    }
    void OnTriggerExit(Collider _coll)
    {
        if (_colls.Contains(_coll))
        {
            _colls.Remove(_coll);
        }
    }
    
}
