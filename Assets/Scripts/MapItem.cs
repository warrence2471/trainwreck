using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem
{
    public MapItemType Type { get; set; }
    public Vector3 Location { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector2Int PreviousDirection { get; set; }

    public MapItem(MapItemType type, int x, int z, int rot, Vector2Int previousDirection)
    {
        Type = type;
        Location = new Vector3(x, 0, z);
        if (rot == 0)
        {
            Rotation = Quaternion.identity;
        }
        else
        {
            Rotation = Quaternion.Euler(0, rot, 0);
        }
        PreviousDirection = previousDirection;
    }
}

public enum MapItemType
{
    Track,
    Turn,
    Broken
}
