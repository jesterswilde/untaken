using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class StatusSource : MonoBehaviour, ISelectable {

    [SerializeField]
    Statuses _status; 
    List<Container> _containers = new List<Container>();
    List<Renderer> _renderers;
    List<Color> _baseColors;

    [SerializeField]
    string _name; 
    public string Name { get { return _name; } }

    void OnTriggerEnter(Collider _coll)
    {
        Container _cont = _coll.GetComponent<Container>();
        if(_cont != null && !_containers.Contains(_cont))
        {
            _containers.Add(_cont); 
        }
    }
    void OnTriggerExit(Collider _coll)
    {
        Container _cont = _coll.GetComponent<Container>();
        if(_cont != null)
        {
            _containers.Remove(_cont); 
        }
    }

    void Update()
    {
        _containers.ForEach((_cont) => _cont.AddStatuses(_status));
    }
    void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>().ToList();
        Renderer _rend = GetComponent<Renderer>(); 
        if(_rend != null)
        {
            _renderers.Add(_rend);
        }
        _baseColors = _renderers.Select((_render) => _render.material.color).ToList(); 
    }

    public void Hovered()
    {
        _renderers.ForEach((_rend) => _rend.material.color = GameManager.StatusColor); 
    }

    public void UnHovered()
    {
        _renderers.ForEach((_rend, i) => _rend.material.color = _baseColors[i]);
    }

    public void Activated()
    {
    }
}
