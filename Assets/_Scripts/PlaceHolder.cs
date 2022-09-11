using Assets._Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHolder : MonoBehaviour
{
    public static System.Action<PlaceHolder> OnHolderClicked = delegate { };
    [SerializeField] PieceDataManager DataManager;
    
    public GameObject GridObj;
   
    private PieceController pieceController;
    
    private Vector3 mOffset;

    int rotIndex;
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
    }


    public void OnRotateButtonClick()
    {
        var r = (Rotation)(rotIndex++ % 4);
        pieceController.Repaint(r);
    }

    
}
