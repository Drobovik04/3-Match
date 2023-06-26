using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundPart : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int width, height;
    private Vector3 centerOfField;
    [SerializeField]
    private Tilemap backField;
    [SerializeField]
    private Tile first, second;
    void Start()
    {
        centerOfField = backField.tileAnchor;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    backField.SetTile(new Vector3Int(i - width / 2, j - height / 2, 0) - Vector3Int.RoundToInt(centerOfField), first);
                }
                else
                {
                    backField.SetTile(new Vector3Int(i - width / 2, j - height / 2, 0) - Vector3Int.RoundToInt(centerOfField), second);
                }
            }
        }
    }

    void Update()
    {

    }
    public Vector2Int GetSize()
    {
        return new Vector2Int(width, height);
    }
}
