using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Bubble : MonoBehaviour {

    Transform _attachedObj;
    Vector3 _offset;
    Text _text; 
    public GameObject SetUpBubble(Transform _trans, string _words)
    {
        _text.text = _words;
        transform.forward = Camera.main.transform.forward;
        _attachedObj = _trans;
        return gameObject; 
    }
    void Awake()
    {
        _text = GetComponentInChildren<Text>(); 
    }
    void Update()
    {
        transform.position = _attachedObj.position + transform.up; 
    }
}
