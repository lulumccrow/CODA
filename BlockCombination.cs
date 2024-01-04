using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockCombination", menuName = "ScriptableObjects/BlockCombination", order = 1)]
public class BlockCombination : ScriptableObject
{
    public BlockType[] blockIds;
}

