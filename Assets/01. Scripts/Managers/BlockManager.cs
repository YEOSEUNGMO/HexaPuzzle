using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BlockManager
{
    Transform _blockParent = null;
    int _topCount = 10;
    const float MoveSpeed = 1.5f;
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
                if (coord.x % 2 != 0)
                    nearCoord.y += 1;
                break;
            case Define.Direction.DOWN_RIGHT:
                nearCoord.x += 1;
                if (coord.x % 2 == 0)
                    nearCoord.y -= 1;
                break;
            case Define.Direction.DOWN:
                nearCoord.y -= 1;
                break;
            case Define.Direction.DOWN_LEFT:
                nearCoord.x -= 1;
                if (coord.x % 2 == 0)
                    nearCoord.y -= 1;
                break;
            case Define.Direction.UP_LEFT:
                nearCoord.x -= 1;
                if (coord.x % 2 != 0)
                    nearCoord.y += 1;
                break;
        }
        return GetBlock(nearCoord);
    }
    public void Init()
    {
        GameObject blockParentObj = new GameObject { name = "BlockParent" };
        _blockParent = blockParentObj.transform;
        while (true)
        {
            CreateBlocks();
            if (Managers.Match.CheckMatchingAll().Count == 0)
                break;
            ClearBlocks();
        }
    }

    void CreateBlocks()
    {
        foreach (var tile in Managers.Tile.Tiles)
        {
            CreateRandom(tile.Coordinates);
        }
    }
    void ClearBlocks()
    {
        for (int i = Blocks.Count - 1; i >= 0; i--)
        {
            Block removeTarget = Blocks[i];
            Managers.Resource.Destroy(removeTarget.gameObject);
            Blocks.RemoveAt(i);
        }
    }

    void CreateRandom(Vector2Int coord)
    {
        int maxCount = _topCount > 0 ? Enum.GetValues(typeof(Define.BlockType)).Length : Enum.GetValues(typeof(Define.BlockType)).Length - 1;
        Define.BlockType type = (Define.BlockType)UnityEngine.Random.Range(0, maxCount);
        if (type == Define.BlockType.TOP)
            _topCount--;
        GameObject blockObj = Managers.Resource.Instantiate($"{type}", _blockParent);
        Block block = blockObj.GetComponent<Block>();
        block.SetBlock(coord);
        Blocks.Add(block);
    }

    public IEnumerator SwapBlocks(Block block1, Block block2)
    {
        Vector3 block1TargetPos = block2.transform.position;
        float block1Dist = Vector3.Distance(block1.transform.position, block1TargetPos);
        Vector3 block2TargetPos = block1.transform.position;
        float block2Dist = Vector3.Distance(block2.transform.position, block2TargetPos);
        while (block1Dist >= 0.1f || block2Dist >= 0.1f)
        {
            block1.transform.position = Vector3.Lerp(block1.transform.position, block1TargetPos, Time.deltaTime * MoveSpeed);
            block2.transform.position = Vector3.Lerp(block2.transform.position, block2TargetPos, Time.deltaTime * MoveSpeed);
            yield return null;
            block1Dist = Vector3.Distance(block1.transform.position, block1TargetPos);
            block2Dist = Vector3.Distance(block2.transform.position, block2TargetPos);
        }

        block1.transform.position = block1TargetPos;
        block2.transform.position = block2TargetPos;
        Vector2Int tempCoord = block1.Coordinates;
        block1.Coordinates = block2.Coordinates;
        block2.Coordinates = tempCoord;
    }

}
