

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BlockType
{
    Up,
    Left,
    Right,
    Use,
    Move,
    Computer,
    Button,
    YellowBlock,
    RedBlock,
    BlueBlock,
    Build
}

public class Block : MonoBehaviour
{
    private LevelController levelController;

    public BlockType blockType;

    public float smoothTime = 0.5f;
    public float speed = 10;

    private Vector3 velocity;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public bool isSelected = false;
    private int blockPosition = 0;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        if (levelController == null)
        {
            Debug.LogError("Level controller not found!");
        }

        startPosition = transform.position;
    }

    public void OnMouseDown()
    {
        if (!isSelected)
        {
            // Limit the blocks to the maximum to prevent overflow.
            if(levelController.currentBlocks.Count >= levelController.blockMax)
            {
                return;
            }

            endPosition = levelController.ComputeNextPosition();
            blockPosition = levelController.currentBlocks.Count;
            levelController.currentBlocks.Add(gameObject);
            isSelected = true;
        }
        else
        {
            for(var i = 0; i < levelController.currentBlocks.Count; i++)
            {
                if(i > blockPosition)
                {
                    levelController.currentBlocks[i].GetComponent<Block>().isSelected = false;
                }
            }
            levelController.currentBlocks
                .RemoveRange(blockPosition, levelController.currentBlocks.Count - blockPosition);
            isSelected = false;
        }
    }

    public void Update()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            isSelected ? endPosition : startPosition,
            ref velocity,
            smoothTime,
            speed
        );
    }
}
