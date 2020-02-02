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
    public GameObject cow;
    [SerializeField]
    public GameObject finish;
    [SerializeField]
    public GameObject[] decos;
    [SerializeField]
    public int size = 14;
    [SerializeField]
    public int maxIterations = 500;
    [SerializeField]
    public int maxBrokenTrails = 5;
    [SerializeField]
    public int initialUnbrokenTrails = 10;

    private const int CTurn = 90;
    private int brokenTrailsSoFar = 0;

    private void Awake()
    {
        var layout = MakeMapLayout();
        BuildMap(layout);
    }

    private IEnumerable<MapItem> MakeMapLayout()
    {
        var layout = new List<MapItem>();
        InitRandom();

        var loc = new Vector2Int(-size, 0);
        var dir = Vector2Int.right;

        layout.Add(CreateTrack(loc, dir));
        loc += dir;
        layout.Add(CreateTrack(loc, dir));
        loc += dir;

        int j = 0; 
        while (j <= maxIterations)
        {
            j++;
            for (int i = 0; i < Random.Range(1, 12); i++)
            {
                // Stop and backtrack if placement invalid 
                if (!CheckValidPlacementStraight(layout, loc))
                {
                    Debug.Log($"Will not place straight rail at {loc.ToString()} because of invalid placement.");
                    Backtrack(layout, ref loc, ref dir, 2);
                    break;
                }

                if (layout.Count > initialUnbrokenTrails && brokenTrailsSoFar < maxBrokenTrails && Random.value < 0.05) {
                    layout.Add(CreateBroken(loc, dir));
                    brokenTrailsSoFar += 1;
                } else {
                    layout.Add(CreateTrack(loc, dir));
                }
                loc += dir;
                
                // Stop if we're about to hit the edge
                if (loc.x > size || loc.x < -size || loc.y > size || loc.y < -size)
                {
                    Debug.Log($"Will not place straight rail at {loc.ToString()} because of the edge.");
                    Backtrack(layout, ref loc, ref dir, 2);
                    break;
                }
            }

            if (j < maxIterations)
            {
                if (!CheckValidPlacementTurn(layout, loc))
                {
                    Debug.Log($"Will not place curved rail at {loc.ToString()} because of invalid placement.");
                    Backtrack(layout, ref loc, ref dir, 2);
                }

                var prevDir = dir;
                if (Random.value < 0.5)
                {
                    dir.Set(dir.y, -dir.x);
                    layout.Add(CreateTurn(loc, GetRotation(dir) + CTurn, prevDir));
                }
                else
                {
                    dir.Set(-dir.y, dir.x);
                    layout.Add(CreateTurn(loc, GetRotation(dir), prevDir));
                }
                loc += dir;
            }
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

    private void InitRandom()
    {
        if (PlayerVars.Name != null)
        {
            Debug.Log($"Generating a map for player '{PlayerVars.Name}'.");
            var seed = PlayerVars.Name.GetHashCode();
            Debug.Log($"Seed is {seed}");
            Random.InitState(seed);
        }
        else
        {
            Debug.Log("No seed for map generation");
        }
    }

    private bool CheckValidPlacementStraight(List<MapItem> layout, Vector2Int loc)
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

    private bool CheckValidPlacementTurn(List<MapItem> layout, Vector2Int loc)
    {
        for (int k = 0; k < layout.Count; k++)
        {
            // Stop if a turn is hitting anything
            if (layout[k].Location.x == loc.x && layout[k].Location.z == loc.y)
            {
                return false;
            }
        }
        return true;
    }

    private void Backtrack(List<MapItem> layout, ref Vector2Int loc, ref Vector2Int dir, int steps)
    {
        int i = 1;
        while (i < steps + 1 || layout[layout.Count - 1].Type == MapItemType.Turn)
        {
            var item = layout[layout.Count - 1];
            layout.RemoveAt(layout.Count - 1);
            loc -= dir;
            if (item.Type == MapItemType.Turn)
            {
                dir = item.PreviousDirection;
            }
            //Debug.Log($"Backtracked {(i - 1).ToString()} of {steps.ToString()} times, stopping early.");
            //return false;
            i++;
        }
        Debug.Log($"Backtracked {i.ToString()} times.");
        //return true;
    }
        
    private MapItem CreateTrack(Vector2Int loc, Vector2Int dir)
    {
        return new MapItem(MapItemType.Track, loc.x, loc.y, GetRotation(dir), dir);
    }

    private MapItem CreateTurn(Vector2Int loc, int rot, Vector2Int dir)
    {
        return new MapItem(MapItemType.Turn, loc.x, loc.y, rot, dir);
    }

    private MapItem CreateBroken(Vector2Int loc, Vector2Int dir)
    {
        return new MapItem(MapItemType.Broken, loc.x, loc.y, GetRotation(dir), dir);
    }
}
