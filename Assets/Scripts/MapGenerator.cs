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
    public float brokenTrailChance = 0.05f;
    [SerializeField]
    public float cowChance = 0.02f;
    [SerializeField]
    public int maxCows = 5;
    [SerializeField]
    public int initialUnbrokenTrails = 10;
    [SerializeField]
    public float decoChance = 0.05f;
    [SerializeField]
    public int finishOffset = 3;

    private const int CTurn = 90;

    private void Awake()
    {
        var layout = MakeMapLayout();
        //var layout = MakeStupidMap();
        AddNonRails(layout);
        BuildMap(layout);
    }

    private List<MapItem> MakeStupidMap()
    {
        var layout = new List<MapItem>();
        for (int i = 0; i < size; i++)
        {
            layout.Add(CreateTrack(new Vector2Int(-size + i, 0), Vector2Int.right));
        }
        var loc = layout[layout.Count - finishOffset].Location;
        layout.Add(CreateFinish(new Vector2Int(Mathf.RoundToInt(loc.x), Mathf.RoundToInt(loc.z)), Vector2Int.right));
        return layout;
    }

    private List<MapItem> MakeMapLayout()
    {
        var layout = new List<MapItem>();
        InitRandom();

        int brokenTrailsSoFar = 0;

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

                if (layout.Count > initialUnbrokenTrails && brokenTrailsSoFar < maxBrokenTrails && Random.value < brokenTrailChance)
                {
                    layout.Add(CreateBroken(loc, dir));
                    brokenTrailsSoFar += 1;
                }
                else
                {
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

        var item = layout[layout.Count - finishOffset];
        if (item.Type == MapItemType.Turn)
        {
            item = layout[layout.Count - finishOffset - 1];
        }
        layout.Add(CreateFinish(new Vector2Int(Mathf.RoundToInt(item.Location.x), Mathf.RoundToInt(item.Location.z)), item.PreviousDirection));

        return layout;
    }

    private void AddNonRails(List<MapItem> layout)
    {
        // Init the array
        MapItemType[,] map = new MapItemType[2 * size + 1, 2 * size + 1];
        for (int i = 0; i < 2 * size + 1; i++)
        {
            for (int j = 0; j < 2 * size + 1; j++)
            {
                map[i, j] = MapItemType.Nothing;
            }
        }

        // Collect the rail locations
        foreach (var item in layout)
        {
            map[Mathf.RoundToInt(item.Location.x) + size, Mathf.RoundToInt(item.Location.z) + size] = item.Type;
        }

        // Add cows to layout
        int cowsSoFar = 0;
        for (int i = 0; i < layout.Count; i++)
        {
            if (i > initialUnbrokenTrails && cowsSoFar < maxCows && Random.value < cowChance)
            {
                var loc = layout[i].Location;
                layout.Add(CreateCow(new Vector2Int(Mathf.RoundToInt(loc.x), Mathf.RoundToInt(loc.z))));
                cowsSoFar += 1;
            }
        }

        for (int i = 0; i < 2 * size + 1; i++)
        {
            for (int j = 0; j < 2 * size + 1; j++)
            {
                if (map[i, j] == MapItemType.Nothing && Random.value < decoChance)
                {
                    layout.Add(CreateDeco(new Vector2Int(i - size, j - size)));
                }
            }
        }
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
            case MapItemType.Cow:
                return cow;
            case MapItemType.Deco:
                return decos[Random.Range(0, decos.Length)];
            case MapItemType.Finish:
                return finish;
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

    private MapItem CreateCow(Vector2Int loc)
    {
        return new MapItem(MapItemType.Cow, loc.x, loc.y, Random.Range(0, 359), Vector2Int.zero);
    }

    private MapItem CreateDeco(Vector2Int loc)
    {
        return new MapItem(MapItemType.Deco, loc.x, loc.y, Random.Range(0, 359), Vector2Int.zero);
    }

    private MapItem CreateFinish(Vector2Int loc, Vector2Int dir)
    {
        return new MapItem(MapItemType.Finish, loc.x, loc.y, GetRotation(dir), dir);
    }
}