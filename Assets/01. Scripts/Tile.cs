using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int Coordinates;

    public void SetTile(Vector2Int coord)
    {
        Coordinates = coord;
        transform.position = new Vector3(Coordinates.x * 0.47f, (Coordinates.x % 2 == 0 ? 0 : 0.32f) + Coordinates.y * 0.64f, 0f);
    }
}
