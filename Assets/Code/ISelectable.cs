using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable {

    void Hovered();
    void UnHovered();
    void Activated(); 
    string Name { get; }
    Transform transform { get; }
}
