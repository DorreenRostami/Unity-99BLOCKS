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
    public static Action<PlaceHolder> OnHolderLetGo = delegate { };

    [SerializeField] PieceDataManager DataManager;
     
    public GameObject GridObj;
   
    public PieceController pieceController;
    
    private Vector3 mOffset;
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

        for (int i = 0; i < pieceController.cellSprites.Length; i++)
        {
            var col = pieceController.cellSprites[i].color;
            col.a = 0.5f;
            pieceController.cellSprites[i].color = col;
            pieceController.cellSprites[i].transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
    }

    public Vector3 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        pieceController.transform.position = GetMouseWorldPos() + mOffset;
        OnHolderDragged(this);
    }

    void OnMouseUp()
    {
        for (int i = 0; i < pieceController.cellSprites.Length; i++)
        {
            var col = pieceController.cellSprites[i].color;
            col.a = 1;
            pieceController.cellSprites[i].color = col;
            pieceController.cellSprites[i].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        OnHolderLetGo(this);
    }

    public void PlaceHolderIsFull()
    {
        //bring shape back to the bottom
        pieceController.transform.localPosition = Vector3.zero;
        pieceController.CleanPoes();

        //deactivate hidden shape and shadow
        OnHolderFull(this);
    }

    public void PlaceHolderIsEmpty(GameObject[] shadowCells)
    {
        //fill grid where the shadows are
        for (int i = 0; i < shadowCells.Length; i++)
        {
            if (shadowCells[i].activeInHierarchy)
            {
                GameManager.Instance.gridController.FillGrid(
                    shadowCells[i].transform.GetChild(0).position, pieceController.cellSprites[i].transform);

            }
        }

        //reset totation for the new shape
        rotIndex = 0;

        //check score
        GameManager.Instance.gridController.CheckBlockForScore();

        //bring hidden shape to front and make a new one + deactivate shadow
        OnHolderEmpty(this);
    }


    public void OnRotateButtonClick()
    {
        var r = (Rotation)(++rotIndex % 4);
        pieceController.Repaint(r);
    }

    
}
