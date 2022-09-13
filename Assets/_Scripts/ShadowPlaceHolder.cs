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

    //check shadows to see if shape can fill the grid when it's done being dragged
    private void OnHolderLetGoCheckShadows(PlaceHolder obj)
    {
        for (int i = 0; i < obj.pieceController.cellSprites.Length; i++)
        {
            if (!GameManager.Instance.gridController.IsInsideGrid(shadowCells[i].transform.position) || !GameManager.Instance.gridController.IsValidToFill(shadowCells[i].transform.position))
            {
                obj.PlaceHolderIsFull();
                return;
            }
        }
        obj.PlaceHolderIsEmpty(shadowCells);
    }

    //make shadows snap into the cells inside the grid while it's being dragged
    private void OnPlaceHolderDraggedShadow(PlaceHolder obj)
    {
        int len = obj.pieceController.cellSprites.Length;
        for (int i = 0; i < len; i++)
        {
            if (GameManager.Instance.gridController.IsInsideGrid(shadowCells[i].transform.position))
            {
                shadowCells[i].transform.position = GameManager.Instance.gridController.GetCellPositionFromWorldPosition(piece.transform.position + localPos[i]);
                if (!shadowCells[i].activeInHierarchy)
                {
                    shadowCells[i].SetActive(true);
                }
            }
            else
            {
                shadowCells[i].SetActive(false);
            }
        }
    }

    //deativate shadows when the shape isnt being dragged around anymore
    private void ResetShadow(PlaceHolder obj)
    {
        piece.transform.SetParent(transform);
        for (int i = 0; i < shadowCount; i++)
        {
            shadowCells[i].SetActive(false);
        }
    }

    //get shadows ready for the shape that was just clicked
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
        }
    }

    //generate cells which will be used as a shadow inside the grid for the shape being dragged
    //this only happens once at the start
    public void GenerateShadowShape()
    {
        piece = new GameObject("Shadow Piece");
        piece.transform.SetParent(transform);
        piece.transform.localPosition = Vector3.zero;
        shadowCells = new GameObject[shadowCount];
        localPos = new Vector3[shadowCount];
        
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
