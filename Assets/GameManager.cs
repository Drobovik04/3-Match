using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap background;
    [SerializeField]
    private GameObject[] tiles;
    [SerializeField]
    private float[] probability;
    [SerializeField]
    private Vector2Int size;
    private Vector3 leftUpCorner;
    private float weight;
    private float[] probabilityForFruits;
    private Camera mainCamera;
    private RaycastHit2D prevObject;
    private RaycastHit2D hit;
    private GameObject[,] gridInfo;
    private int[,] gridInfoTypes;
    private void Start()
    {
        leftUpCorner = background.tileAnchor + new Vector3(-size.x, size.y, 0);
        weight = probability.Sum();
        probabilityForFruits = new float[probability.Length];
        mainCamera = Camera.main;
        gridInfo = new GameObject[size.x, size.y];
        CalculateProbability();
        FillField();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsMoveable();
        }
    }
    private void FillField()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                int k = ChooseTile(Random.Range(0, 1f));
                gridInfo[i,j] = Instantiate(tiles[k], new Vector3Int(i - size.x / 2, j - size.y / 2) - Vector3Int.RoundToInt(background.tileAnchor), Quaternion.identity);
                gridInfoTypes[i, j] = k;
            }
        }
    }
    private void CalculateProbability()
    {
        probabilityForFruits[0] = probability[0]/weight;
        for (int i = 1; i < probabilityForFruits.Length; i++)
        {
            probabilityForFruits[i] = (probability[i] / weight) + probabilityForFruits[i - 1];
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
    private bool IsMoveable()
    {
        if (hit=Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition),Vector2.zero))
        {
            if (prevObject.transform==null)
            {
                prevObject = hit;
                return false;
            }
            if (Math.Abs(hit.transform.position.x-prevObject.transform.position.x) + Math.Abs(hit.transform.position.y-prevObject.transform.position.y) == 1)
            {
                Move();
                prevObject = new RaycastHit2D();
                hit = new RaycastHit2D();
            }
            return true;
        }
        return false;
    }
    private void Move()
    {
        prevObject.transform.DOMove(hit.transform.position, 0.5f);
        hit.transform.DOMove(prevObject.transform.position, 0.5f);
    }
}
