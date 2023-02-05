using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EventTile : TileType
{
    public List<EventObject> events;

    public override bool OnEnd()
    {
        EventObject e = events[Random.Range(0, events.Count)];
        return e.Effect();
    }
}
