using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisualHandler : MonoBehaviour
{
    public abstract void Select();
    public abstract void Deselect();

    public abstract void Selectable();

    public abstract void Activate();

    public abstract void Deactivate();

    public abstract void Resetivate();
}
