using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector2Int Coordinates;
    public Define.BlockType Type = Define.BlockType.BLUE;

    public void SetBlock(Vector2Int coord)
    {
        Coordinates = coord;
        transform.position = Managers.Tile.GetTile(coord).transform.position;
    }
}
