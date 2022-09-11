using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets._Scripts;

public class HiddenPlaceHolder : MonoBehaviour
{

    private PieceController pieceController;

    void Start()
    {
        pieceController = GameManager.Instance.GenerateShape();
        pieceController.transform.SetParent(transform);
        pieceController.transform.localPosition = Vector3.zero;
        pieceController.Repaint(Rotation.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
