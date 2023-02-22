using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public static readonly Vector2Int MapSize = new Vector2Int(7,6);

    public enum Direction
    {
        UP,
        UP_RIGHT,
        DOWN_RIGHT,
        DOWN,
        DOWN_LEFT,
        UP_LEFT,
    }
    public enum BlockType
    {
        BLUE,
        RED,
        YELLOW,
        GREEN,
        ORANGE,
        PURPLE,
        TOP,
    }
}