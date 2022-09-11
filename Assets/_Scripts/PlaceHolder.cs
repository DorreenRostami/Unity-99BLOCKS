using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHolder : MonoBehaviour
{
    [SerializeField] PieceDataManager DataManager;
    public GameObject[] cells;
    private GameObject piece;
    private Vector3 mOffset;
    private Vector3 pieceStartedPosition;

    GameObject shape;

    public GameObject GridObj;
    private Vector2 size;
    public Canvas canvas;

    void Start()
    {
        size = GridObj.GetComponent<GridController>().cellSize;
        GenerateShape();
        pieceStartedPosition = this.transform.position;
    }

    void OnMouseDown()
    {
        mOffset = piece.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        piece.transform.position = GetMouseWorldPos() + mOffset;
    }

    void OnMouseUp()
    {
        piece.transform.position = pieceStartedPosition;    
    }
    private void GenerateShape()
    {
        piece = new GameObject("Piece");
        piece.transform.SetParent(transform);
        piece.transform.localPosition = Vector3.zero;

        var dd = DataManager.GetRandomData();
        GameObject cell = cells[UnityEngine.Random.Range(0, cells.Length)];
        foreach (Vector2 cord in dd.Coordinations)
        {
            GameObject cellObj = MonoBehaviour.Instantiate(cell);
            cellObj.GetComponent<SpriteRenderer>().size = new Vector2(size.x, size.y);
            cellObj.transform.SetParent(piece.transform);
            cellObj.transform.localPosition = new Vector2(cord[0], cord[1]);
        }
    }
}
