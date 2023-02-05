using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventObject : ScriptableObject
{
    public abstract bool Effect(); //return value is for if the game needs to stop
    public abstract void End();
}
