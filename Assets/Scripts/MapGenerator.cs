using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject railtrack;
    [SerializeField]
    public GameObject railturn;
    [SerializeField]
    public GameObject railtrackBroken;
    [SerializeField]
    public GameTrain[] train;
    [SerializeField]
    public GameObject finish;
    [SerializeField]
    public int size = 14;

    private const int CTurn = 90;

    private void Awake()
    {
        var layout = MakeMapLayout();
        BuildMap(layout);
    }

    private IEnumerable<MapItem> MakeMapLayout()
    {
        var layout = new List<MapItem>();
        //Random.InitState(42);

        var loc = new Vector2Int(-size, 0);
        var dir = Vector2Int.right;
        var edge = size - 1;
        var stop = false;

        layout.Add(CreateTrack(loc, GetRotation(dir)));
        loc += dir;
        layout.Add(CreateTrack(loc, GetRotation(dir)));
        loc += dir;

        while (!stop)
        {
            for (int i = 0; i < Random.Range(1, 12); i++)
            {
                // Stop and backtrack if placement invalid 
                if (!CheckValidPlacement(layout, loc))
                {
                    stop = !Backtrack(layout, ref loc, ref dir, 2);
                    break;
                }

                layout.Add(CreateTrack(loc, GetRotation(dir)));
                loc += dir;
                
                // Stop if we're about to hit the edge
                if (loc.x > edge || loc.x < -edge || loc.y > edge || loc.y < -edge)
                {
                    break;
                }
            }

            if (stop)
            {
                break;
            }

            //if (!CheckValidPlacement(layout, loc))
            //{
            //    stop = !Backtrack(layout, ref loc, ref dir, 2);
            //    break;
            //}

            if (Random.value < 0.5)
            {
                dir.Set(dir.y, -dir.x);
                layout.Add(CreateTurn(loc, GetRotation(dir) + CTurn));
            }
            else
            {
                dir.Set(-dir.y, dir.x);
                layout.Add(CreateTurn(loc, GetRotation(dir)));
            }
            loc += dir;
        }

        return layout;

        // TODO Spawning the train creates problems with the wagon links, no idea what's going on...
        //for (int i = 0; i < train.Length; i++)
        //{
        //    var follower = Instantiate(train[i], new Vector3(-2.5f - i * GameTrain.CWagonDistance, 0, 5), Quaternion.Euler(0, 90, 0));
        //    if (i > 0)
        //    {
        //        //follower.GetComponent<GameTrain>().preceding = train[i - 1];
        //    }
        //}
    }

    private void BuildMap(IEnumerable<MapItem> layout)
    {
        foreach (var item in layout)
        {
            Instantiate(GetGameObject(item.Type), item.Location, item.Rotation);
        }
    }

    private GameObject GetGameObject(MapItemType type)
    {
        switch (type)   
        {
            case MapItemType.Track:
                return railtrack;
            case MapItemType.Turn:
                return railturn;
            case MapItemType.Broken:
                return railtrackBroken;
            default:
                return null;
        }
    }

    private int GetRotation(Vector2Int direction)
    {
        return Mathf.RoundToInt(Vector2.SignedAngle(direction, Vector2Int.right) / CTurn) * CTurn;
    }

    private bool CheckValidPlacement(List<MapItem> layout, Vector2Int loc)
    {
        for (int k = 0; k < layout.Count; k++)
        {
            // Stop if a straight is hitting...
            if ((layout[k].Location.x == loc.x && layout[k].Location.z == loc.y)
                // ... the first 3 items or a turn
                && (k < 3 || layout[k].Type == MapItemType.Turn))
            {
                return false;
            }
        }
        return true;
    }

    private bool Backtrack(List<MapItem> layout, ref Vector2Int loc, ref Vector2Int dir, int steps)
    {
        for (int i = 1; i < steps + 1; i++)
        {
            if (layout[layout.Count - 1].Type == MapItemType.Track || layout[layout.Count - 1].Type == MapItemType.Broken)
            {
                layout.RemoveAt(layout.Count - 1);
                loc -= dir;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
        
    private MapItem CreateTrack(Vector2Int loc, int rot)
    {
        return new MapItem(MapItemType.Track, loc.x, loc.y, rot);
    }

    private MapItem CreateTurn(Vector2Int loc, int rot)
    {
        return new MapItem(MapItemType.Turn, loc.x, loc.y, rot);
    }

    private MapItem CreateBroken(Vector2Int loc, int rot)
    {
        return new MapItem(MapItemType.Broken, loc.x, loc.y, rot);
    }
}
