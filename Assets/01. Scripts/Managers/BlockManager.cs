using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BlockManager
{
    Transform _blockParent = null;
    int _topCount = 10;
    public List<Block> Blocks = new List<Block>();
    public Block GetBlock(Vector2Int coord) { return Blocks.Find(x => x.Coordinates == coord); }
    public Block GetNearBlock(Vector2Int coord, Define.Direction dir)
    {
        Vector2Int nearCoord = coord;
        switch (dir)
        {
            case Define.Direction.UP:
                nearCoord.y += 1;
                break;
            case Define.Direction.UP_RIGHT:
                nearCoord.x += 1;
                nearCoord.y += 1;
                break;
            case Define.Direction.DOWN_RIGHT:
                nearCoord.x += 1;
                break;
            case Define.Direction.DOWN:
                nearCoord.y -= 1;
                break;
            case Define.Direction.DOWN_LEFT:
                nearCoord.x -= 1;
                break;
            case Define.Direction.UP_LEFT:
                nearCoord.x -= 1;
                nearCoord.y += 1;
                break;
        }
        return GetBlock(nearCoord);
    }
    public void Init()
    {
        GameObject blockParentObj = new GameObject { name = "BlockParent" };
        _blockParent = blockParentObj.transform;
        CreateBlocks();
    }

    void CreateBlocks()
    {
        foreach (var tile in Managers.Tile.Tiles)
        {
            CreateRandom(tile.Coordinates);
        }
    }

    void CreateRandom(Vector2Int coord)
    {
        int maxCount = _topCount > 0 ? Enum.GetValues(typeof(Define.BlockType)).Length : Enum.GetValues(typeof(Define.BlockType)).Length - 1;
        Define.BlockType type = (Define.BlockType)UnityEngine.Random.Range(0, maxCount);
        if (type == Define.BlockType.TOP)
            _topCount--;
        GameObject blockObj = Managers.Resource.Instantiate($"{type}", _blockParent);
        blockObj.GetComponent<Block>().SetBlock(coord);
    }
}
