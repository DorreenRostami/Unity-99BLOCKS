using Assets._Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHolder : MonoBehaviour
{
    public static Action<PlaceHolder> OnHolderClicked = delegate { };
    public static Action<PlaceHolder> OnHolderFull = delegate { };
    public static Action<PlaceHolder> OnHolderEmpty = delegate { };
    public static Action<PlaceHolder> OnHolderDragged = delegate { };

    [SerializeField] PieceDataManager DataManager;
    
    public GameObject GridObj;
   
    public PieceController pieceController;
    
    public Vector3 mOffset;
    private GridController grid;

    public int rotIndex = 0;

    void Start()
    {
        pieceController = GameManager.Instance.GenerateShape();
        grid = GameManager.Instance.gridController;
        pieceController.transform.SetParent(transform);
        pieceController.transform.localPosition = Vector3.zero;
        pieceController.Repaint(Rotation.zero);
    }

    void OnMouseDown()
    {
        mOffset = pieceController.transform.position - GetMouseWorldPos();
        OnHolderClicked(this);
    }

    public Vector3 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        pieceController.transform.position = GetMouseWorldPos() + mOffset;
        //pieceController.CheckPos();
        OnHolderDragged(this);

    }

    void OnMouseUp()
    {
        //if piece position is not in grid
        //getcellposfromworld
        
        for (int i = 0; i < pieceController.cellSprites.Length; i++)
        {
            if (!GameManager.Instance.gridController.IsValid(pieceController.cellSprites[i].transform.position))
            {
                pieceController.transform.localPosition = Vector3.zero;
                pieceController.CleanPoes();
                OnHolderFull(this);
                return;
            }
        }

        OnHolderEmpty(this);
    }


    public void OnRotateButtonClick()
    {
        var r = (Rotation)(++rotIndex % 4);
        pieceController.Repaint(r);
    }

    
}
