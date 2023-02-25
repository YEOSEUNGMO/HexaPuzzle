using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MatchManager
{
    public void Init()
    {

    }

    public List<Block> CheckMatchingAll()
    {
        List<Block> matchedBlocks = new List<Block>();
        foreach (var block in Managers.Block.Blocks)
        {
            var checkedBlocks = CheckMatching(block);
            if (checkedBlocks != null)
                matchedBlocks = matchedBlocks.Union(checkedBlocks).ToList();
        }

        Debug.Log($"Matched Blocks : {matchedBlocks.Count}");
        return matchedBlocks;
    }
    public List<Block> CheckMatching(Block block)
    {
        List<Block> result = new List<Block>();
        if (block.Type == Define.BlockType.TOP)
            return null;

        var straightBlocks = CheckStraightAll(block);
        if (straightBlocks.Count > 0)
        {
            result = result.Union(straightBlocks).ToList();
        }
        return result;
    }

    public List<Block> CheckStraightAll(Block block)
    {
        List<Block> result = new List<Block>();
        //상,좌상,좌하 방향만 검사
        int dirCount = Enum.GetValues(typeof(Define.Direction)).Length - 3;

        for (int i = 0; i < dirCount; i++)
        {
            Define.Direction dir = (Define.Direction)i;
            // Debug.Log($"Start Checking : {block.name} to {dir}");
            var straightBlocks = CheckStaight(block, dir);
            if (straightBlocks.Count > 0)
            {
                result = result.Union(straightBlocks).ToList();
            }
        }

        return result;
    }

    public List<Block> CheckStaight(Block block, Define.Direction dir)
    {
        List<Block> result = new List<Block>();
        var nextBlock = block;
        result.Add(block);
        Vector2Int currentCoord = block.Coordinates;
        while (true)
        {
            currentCoord = nextBlock.Coordinates;
            nextBlock = Managers.Block.GetNearBlock(nextBlock.Coordinates, dir);

            if (nextBlock == null)
                break;
            if (nextBlock.Type != block.Type)
                break;
            Vector2Int nextCoord = nextBlock.Coordinates;
            result.Add(nextBlock);
        }
        if (result.Count < 3)
            result.Clear();
        return result;
    }
}