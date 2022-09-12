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
    private Vector3 upOffset;
    private GridController grid;

    public int rotIndex = 0;

    void Start()
    {
        upOffset = new Vector3(0f, 2f, 0f);
        pieceController = GameManager.Instance.GenerateShape();
        grid = GameManager.Instance.gridController;
        pieceController.transform.SetParent(transform);
        pieceController.transform.localPosition = Vector3.zero;
        pieceController.Repaint(Rotation.zero);
    }

    void OnMouseDown()
    {
        pieceController.transform.position += upOffset;
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
        
        for (int i = 0; i < pieceController.cellSprites.Length; i++)
        {
            if (!GameManager.Instance.gridController.IsValid(pieceController.cellSprites[i].transform.position) || !GameManager.Instance.gridController.IsValidToFill(pieceController.cellSprites[i].transform.position))
            {
                pieceController.transform.localPosition = Vector3.zero;
                pieceController.CleanPoes();
                OnHolderFull(this);
                return;
            }
        }

        //now we can fill the grid
        for (int i = 0; i < pieceController.cellSprites.Length; i++)
        {
            GameManager.Instance.gridController.FillGrid(pieceController.cellSprites[i].transform.position, pieceController.cellSprites[i].transform);
        }

        GameManager.Instance.gridController.CheckBlockForScore();

        OnHolderEmpty(this);
    }


    public void OnRotateButtonClick()
    {
        var r = (Rotation)(++rotIndex % 4);
        pieceController.Repaint(r);
    }

    
}
