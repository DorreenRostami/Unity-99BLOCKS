using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHolder : MonoBehaviour
{
    [SerializeField] PieceDataManager DataManager;
    public GameObject[] cells;

    GameObject shape;

    public GameObject GridObj;
    private Vector2 size;
    public Canvas canvas;

    void Start()
    {
        size = GridObj.GetComponent<GridController>().cellSize;
        GenerateShape();
    }

    private void GenerateShape()
    {
        GameObject parent = new GameObject("Piece");
        parent.transform.SetParent(transform);
        parent.transform.localPosition = Vector3.zero;

        var dd = DataManager.GetRandomData();
        GameObject cell = cells[UnityEngine.Random.Range(0, cells.Length)];
        foreach (Vector2 cord in dd.Coordinations)
        {
            GameObject cellObj = MonoBehaviour.Instantiate(cell);
            cellObj.GetComponent<SpriteRenderer>().size = new Vector2(size.x, size.y);
            cellObj.transform.SetParent(parent.transform);
            cellObj.transform.localPosition = new Vector2(cord[0], cord[1]);
        }
    }
}
