using Assets._Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlaceHolder : MonoBehaviour
{
    public GameObject shadowPrefab;

    private int shadowCount = 6;
    private GameObject piece;
    private GameObject[] shadowCells;
    private Vector3[] localPos;
    private  Rotation rot;

    void Start()
    {
        GenerateShadowShape();
        PlaceHolder.OnHolderClicked += OnPlaceHolderClickedShadow;
        PlaceHolder.OnHolderDragged += OnPlaceHolderDraggedShadow;
        PlaceHolder.OnHolderFull += OnPlaceHolderFullShadow;
        //PlaceHolder.OnHolderEmpty += OnPlaceHolderEmptyShadow;
    }

    private void OnPlaceHolderDraggedShadow(PlaceHolder obj)
    {
        piece.transform.position = obj.GetMouseWorldPos() + obj.mOffset;
        for (int i = 0; i < shadowCells.Length; i++)
        {
            if (shadowCells[i].activeInHierarchy == true)
                shadowCells[i].transform.parent.position = GameManager.Instance.gridController.GetCellPositionFromWorldPosition(transform.position + localPos[i]);
        }
    }

    private void OnPlaceHolderFullShadow(PlaceHolder obj)
    {
        for (int i = 0; i < shadowCount; i++)
        {
            shadowCells[i].SetActive(false);
        }
    }

    private void OnPlaceHolderClickedShadow(PlaceHolder obj)
    {
        transform.SetParent(obj.transform);
        transform.localPosition = Vector3.zero;

        PieceController pieceCtrl = obj.pieceController;
        rot = (Rotation)obj.rotIndex;
        for (int i = 0; i < pieceCtrl.cellSprites.Length; i++)
        {
            localPos[i] = pieceCtrl.data.Coordinations[i];
            var cord = GameManager.PosGen(rot, localPos[i]);
            shadowCells[i].transform.localPosition = cord;
            shadowCells[i].SetActive(true);
        }
    }

    public void GenerateShadowShape()
    {
        piece = new GameObject("Shadow Piece");
        piece.transform.SetParent(transform);
        piece.transform.localPosition = Vector3.zero;
        shadowCells = new GameObject[shadowCount];
        localPos = new Vector3[shadowCount];
        //var piece = new GameObject("Piece", typeof(PieceController)).GetComponent<PieceController>();
        for (int i = 0; i < shadowCount; i++)
        {
            GameObject cellObj = Instantiate(shadowPrefab);
            Transform shape = cellObj.transform.GetChild(0);
            shape.transform.localScale = new Vector2(GameManager.Instance.shapeCellSize.x, GameManager.Instance.shapeCellSize.y);
            cellObj.transform.SetParent(piece.transform);
            cellObj.transform.localPosition = Vector3.zero;
            cellObj.SetActive(false);
            shadowCells[i] = cellObj;
        }
        
        return;
    }


    /*public void CheckPos()
    {
        for (int i = 0; i < shadowCells.Count; i++)
        {
            if (shadowCells[i].activeInHierarchy == true)
                shadowCells[i].transform.parent.position = GameManager.Instance.gridController.GetCellPositionFromWorldPosition(transform.position + localPose[i]);
        }
    }*/
}
