using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager
{
    public void Init()
    {

    }

    public void CheckAll()
    {
        List<Block> matchedBlocks = new List<Block>();
        foreach(var block in Managers.Block.Blocks)
        {
            CheckMatching(block);
        }
    }
    public List<Block> CheckMatching(Block block)
    {
        if(block.Type == Define.BlockType.TOP)
            return null;

        return null;
    }
}