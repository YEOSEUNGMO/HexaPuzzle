using System.Collections.Generic;
using UnityEngine;

public class TileManager
{
    Transform _tileParent = null;
    bool[,] _mapShape = new bool[Define.MapSize.x, Define.MapSize.y];
    public List<Tile> Tiles = new List<Tile>();
    public Tile GetTile(Vector2Int coord) { return Tiles.Find(x => x.Coordinates == coord); }
    public void Init()
    {
        GameObject tileParentObj = new GameObject { name = "TileParent" };
        _tileParent = tileParentObj.transform;
        SetMapShape();
        CreateMap();
    }
    void SetMapShape()
    {
        /*                  Erase
        (0,5) / (1,5) /                         (5,5) / (6,5)
        (0,1) /                                       / (6,1) 
        (0,0) / (1,0) / (2,0) /       / (4,0) / (5,0) / (6,0)*/

        for (int i = 0; i < Define.MapSize.x; i++)
        {
            for (int j = 0; j < Define.MapSize.y; j++)
            {
                if (i != 3 && j == 0)
                    continue;
                if ((i == 0 || i == Define.MapSize.x - 1) && j == 1)
                    continue;
                if ((i < 2 || i > 4) && j == Define.MapSize.y - 1)
                    continue;
                _mapShape[i, j] = true;
            }
        }
    }
    void CreateMap()
    {
        for (int i = 0; i < Define.MapSize.x; i++)
        {
            for (int j = 0; j < Define.MapSize.y; j++)
            {
                // if (_mapShape[i, j])
                    Tiles.Add(CreateTile(new Vector2Int(i, j)));
            }
        }
    }

    Tile CreateTile(Vector2Int coord)
    {
        GameObject tileObj = Managers.Resource.Instantiate("Tile", _tileParent);
        Tile tile = tileObj.GetComponent<Tile>();
        tile.SetTile(coord);

        return tile;
    }

}
