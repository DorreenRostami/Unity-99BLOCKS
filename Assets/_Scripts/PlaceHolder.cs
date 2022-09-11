using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHolder : MonoBehaviour
{
    [SerializeField] PieceDataManager DataManager;
<<<<<<< HEAD
    public GameObject[] cellsPrefab;
=======
    public GameObject[] cells;
    private GameObject piece;
    private Vector3 mOffset;
    private Vector3 pieceStartedPosition;

    GameObject shape;
>>>>>>> 236a5f2661a6ac6b14582553ab4c1752833cdca2

    public GameObject GridObj;
    private Vector2 size;

    private List<Transform> cells;


    private Rotation rot;
    PieceData data;

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
<<<<<<< HEAD
        cells = new List<Transform>();
=======
        piece = new GameObject("Piece");
        piece.transform.SetParent(transform);
        piece.transform.localPosition = Vector3.zero;
>>>>>>> 236a5f2661a6ac6b14582553ab4c1752833cdca2

        GameObject piece = new GameObject("Piece");
        piece.transform.SetParent(transform);
        piece.transform.localPosition = Vector3.zero;

        data = DataManager.GetRandomData();
        GameObject cell = cellsPrefab[UnityEngine.Random.Range(0, cellsPrefab.Length)];
        foreach (Vector2 cord in data.Coordinations)
        {
            GameObject cellObj = MonoBehaviour.Instantiate(cell);
            cellObj.GetComponent<SpriteRenderer>().size = new Vector2(size.x, size.y);
            cellObj.transform.SetParent(piece.transform);
<<<<<<< HEAD
            cells.Add(cellObj.transform);
            //cellObj.transform.localPosition = new Vector2(cord[0], cord[1]);
        }
        rot = Rotation.zero;
        Repaint();
    }

    private void Repaint()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].localPosition = PosGen(data.Coordinations[i]);
=======
            cellObj.transform.localPosition = new Vector2(cord[0], cord[1]);
>>>>>>> 236a5f2661a6ac6b14582553ab4c1752833cdca2
        }
    }

    private Vector3 PosGen(Vector3 vec)
    {
        switch (rot)
        {
            case Rotation.p90:
                return new Vector3(vec.y, -vec.x, vec.z);
            case Rotation.p180:
                return new Vector3(-vec.x, -vec.y, vec.z);
            case Rotation.p270:
                return new Vector3(-vec.y, vec.x, vec.z);
            case Rotation.zero:
            default:
                return vec;
        }
    }

    public void OnRotateButtonClick()
    {
        switch (rot)
        {
            case Rotation.zero:
                rot = Rotation.p90;
                break;
            case Rotation.p90:
                rot = Rotation.p180;
                break;
            case Rotation.p180:
                rot = Rotation.p270;
                break;
            case Rotation.p270:
                rot = Rotation.zero;
                break;
            default:
                break;
        }
        Repaint();
    }

    enum Rotation {
        zero, p90, p180, p270
    }
}
