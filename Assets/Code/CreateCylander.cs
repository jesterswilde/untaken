using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCylander : MonoBehaviour {

    [SerializeField]
    float _apothem;
    [SerializeField]
    float _numSides;
    [SerializeField]
    float _height;
    [SerializeField]
    float _width; 
	public void BuildCylander()
    {
        for(float i = 0; i < _numSides; i++)
        {
            BoxCollider _box = new GameObject().AddComponent<BoxCollider>();
            _box.transform.position = new Vector3(Mathf.Cos(i / _numSides * 2f * Mathf.PI) * _apothem, 0, Mathf.Sin(i / _numSides * 2f * Mathf.PI) * _apothem) +
                transform.position;
            _box.transform.forward = (transform.position - _box.transform.position).normalized;
            _box.size = new Vector3(2 * _apothem *Mathf.Tan(Mathf.PI / _numSides), _height, _width);
            _box.transform.SetParent(transform, true); 
        }
    }
}
