using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap background, foreground;
    [SerializeField]
    private Tile[] tiles;
    [SerializeField]
    private float[] probability;
    private Vector3 leftUpCorner;
    private Vector2Int size;
    private float weight;
    private float[] probabilityForFruits;
    private void Start()
    {
        size = background.GetComponent<BackgroundPart>().GetSize();
        leftUpCorner = background.tileAnchor + new Vector3(-size.x, size.y, 0);
        weight = probability.Sum();
        probabilityForFruits = new float[probability.Length];
        CalculateProbability();
        FillField();
    }

    private void Update()
    {
        
    }
    private void FillField()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                foreground.SetTile(new Vector3Int(i - size.x/2, j - size.y/2) - Vector3Int.RoundToInt(foreground.tileAnchor), tiles[ChooseTile(Random.Range(0, 1))]);
            }
        }
    }
    private void CalculateProbability()
    {
        for (int i = 0; i < probabilityForFruits.Length; i++)
        {
            probabilityForFruits[i] = probability[i] / weight;
        }
    }
    private int ChooseTile(float prob)
    {
        if (prob <= probabilityForFruits[0])
        {
            return 0;
        }
        if (prob >= probabilityForFruits[0] && prob <= probabilityForFruits[1])
        {
            return 1;
        }
        if (prob >= probabilityForFruits[1] && prob <= probabilityForFruits[2])
        {
            return 2;
        }
        if (prob >= probabilityForFruits[2] && prob <= probabilityForFruits[3])
        {
            return 3;
        }
        return 0;
    }
}
