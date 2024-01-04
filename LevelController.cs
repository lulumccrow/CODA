using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Vector2 blockOffset;
    public Vector2 firstRowStart;
    public Vector2 secondRowStart;
    public int rowMax = 4;
    public int blockMax = 8;
    public List<GameObject> currentBlocks;
    public List<BlockCombination> allSteps;
    public int currentStep;

    public Vector2 ComputeNextPosition()
    {
        var count = currentBlocks.Count;
        if(count < rowMax)
        {
            return firstRowStart + (blockOffset * currentBlocks.Count);
        } else
        {
            return secondRowStart + (blockOffset * (currentBlocks.Count - rowMax));
        }
    }

    public bool IsCorrectCommand()
    {
        // If there's the wrong number of blocks, return false.
        if(currentBlocks.Count != allSteps[currentStep].blockIds.Length)
        {
            return false;
        }

        // Loop and compare all block types, returning false if incorrect type.
        int index = 0;
        foreach(var block in currentBlocks)
        {
            var blockType = block.GetComponent<Block>().blockType;
            if (allSteps[currentStep].blockIds[index] != blockType)
            {
                return false;
            }

            index += 1;
        }
        return true;
    }

    public void ResetAllBlocks()
    {
        foreach (var block in currentBlocks)
        {
            block.GetComponent<Block>().isSelected = false;
        }
        currentBlocks.Clear();
    }
}