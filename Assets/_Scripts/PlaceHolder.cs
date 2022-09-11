using Assets._Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHolder : MonoBehaviour
{
    public static System.Action<PlaceHolder> OnHolderClicked = delegate { };
    public static System.Action<PlaceHolder> OnHolderFull = delegate { };
    public static System.Action<PlaceHolder> OnHolderEmpty = delegate { };
    [SerializeField] PieceDataManager DataManager;
    
    public GameObject GridObj;
   
    private PieceController pieceController;
    
    private Vector3 mOffset;

    int rotIndex = 0;

    void Start()
    {
        pieceController = GameManager.Instance.GenerateShape();
        pieceController.transform.SetParent(transform);
        pieceController.transform.localPosition = Vector3.zero;
        pieceController.Repaint(Rotation.zero);
    }

    void OnMouseDown()
    {
        mOffset = pieceController.transform.position - GetMouseWorldPos();
        OnHolderClicked(this);
    }

    private Vector3 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        pieceController.transform.position = GetMouseWorldPos() + mOffset;
    }

    void OnMouseUp()
    {
        //if piece position is not in grid
        //getcellposfromworld
        pieceController.transform.localPosition = Vector3.zero;
        OnHolderFull(this);
        //OnHolderEmpty(this);
        for (int i = 0; i < pieceController.cells.Length; i++)
        {
            if (!GameManager.Instance.gridController.IsValid(pieceController.cells[i].position))
            {
                pieceController.transform.localPosition = Vector3.zero;
                return;
            }
        }

    }


    public void OnRotateButtonClick()
    {
        var r = (Rotation)(++rotIndex % 4);
        pieceController.Repaint(r);
    }

    
}
