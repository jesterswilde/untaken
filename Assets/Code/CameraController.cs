using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    float _rotationSpeed = 180; 

	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Player.Position;
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime * RotationAmount()); 
	}
    float RotationAmount()
    {
        float _value = 0;
        _value += (Input.GetKey(KeyCode.Q))? 1 : 0;
        _value -= (Input.GetKey(KeyCode.E)) ? 1 : 0;
        return _value; 
    }
}
