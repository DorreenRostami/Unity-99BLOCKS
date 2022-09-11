using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets._Scripts;
using System;

public class HiddenPlaceHolder : MonoBehaviour
{
    private PieceController pieceController;

    void Start()
    {
        GenerateHiddenShape();
        PlaceHolder.OnHolderClicked += OnPlaceHolderClicked;
        PlaceHolder.OnHolderFull += OnPlaceHolderFull;
        PlaceHolder.OnHolderEmpty += OnPlaceHolderEmpty;
    }

    private void OnPlaceHolderEmpty(PlaceHolder obj)
    {
        pieceController.transform.parent = obj.transform;

        pieceController.transform.localScale = new Vector2(1, 1);
        for (int i = 0; i < pieceController.cellSprites.Length; i++)
        {
            var col = pieceController.cellSprites[i].color;
            col.a = 1;
            pieceController.cellSprites[i].color = col;
        }

        GenerateHiddenShape();
    }

    private void OnPlaceHolderFull(PlaceHolder obj)
    {
        gameObject.SetActive(false);
    }

    private void OnPlaceHolderClicked(PlaceHolder obj)
    {
        transform.SetParent(obj.transform);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }

    public void GenerateHiddenShape()
    {
        pieceController = GameManager.Instance.GenerateShape();
        pieceController.transform.SetParent(transform);
        pieceController.transform.localPosition = Vector3.zero;
        
        pieceController.transform.localScale = new Vector2(0.5f, 0.5f);
        for (int i = 0; i < pieceController.cellSprites.Length; i++)
        {
            var col = pieceController.cellSprites[i].color;
            col.a = 0.4f;
            pieceController.cellSprites[i].color = col;
        }

        pieceController.Repaint(Rotation.zero);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
