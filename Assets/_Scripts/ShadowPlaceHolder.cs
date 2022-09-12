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
        PlaceHolder.OnHolderFull += ResetShadow;
        PlaceHolder.OnHolderEmpty += ResetShadow;
        PlaceHolder.OnHolderLetGo += OnHolderLetGoCheckShadows;
    }

    private void OnHolderLetGoCheckShadows(PlaceHolder obj)
    {
        for (int i = 0; i < shadowCells.Length; i++)
        {
            if (shadowCells[i].activeInHierarchy)
            {
                if (!GameManager.Instance.gridController.IsValid(shadowCells[i].transform.position) || !GameManager.Instance.gridController.IsValidToFill(shadowCells[i].transform.position))
                {
                    obj.PlaceHolderIsFull();
                    return;
                }
            }
        }
        obj.PlaceHolderIsEmpty(shadowCells);
    }

    private void OnPlaceHolderDraggedShadow(PlaceHolder obj)
    {
        for (int i = 0; i < shadowCells.Length; i++)
        {
            shadowCells[i].transform.position = GameManager.Instance.gridController.GetCellPositionFromWorldPosition(piece.transform.position + localPos[i]);
        }
    }

    private void ResetShadow(PlaceHolder obj)
    {
        piece.transform.SetParent(transform);
        for (int i = 0; i < shadowCount; i++)
        {
            shadowCells[i].SetActive(false);
        }
    }

    private void OnPlaceHolderClickedShadow(PlaceHolder obj)
    {
        piece.transform.SetParent(obj.pieceController.transform);
        piece.transform.localPosition = Vector3.zero;

        PieceController pieceCtrl = obj.pieceController;
        rot = (Rotation)obj.rotIndex;
        for (int i = 0; i < pieceCtrl.cellSprites.Length; i++)
        {
            localPos[i] = GameManager.PosGen(rot, pieceCtrl.data.Coordinations[i]);
            shadowCells[i].transform.localPosition = localPos[i];
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

}
