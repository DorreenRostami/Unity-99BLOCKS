using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovePiece : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler,
        IPointerUpHandler

{

    //move stuff
    private RectTransform rectTrans;
    private Vector3 shapeSelectedScale;
    private Vector3 shapeStartedPosition;
    private Vector3 shapeStartedScale;
    //private Canvas canvas;
    public Vector2 offset = new Vector2(0f, 100f);

    void Awake()
    {
        shapeStartedPosition = this.GetComponent<RectTransform>().localPosition;
        shapeStartedScale = this.GetComponent<RectTransform>().localScale;
        rectTrans = GetComponent<RectTransform>();
        //canvas = GetComponentInParent<Canvas>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = shapeSelectedScale;
        Debug.Log("on begin drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = shapeStartedScale;
        this.GetComponent<RectTransform>().localPosition = shapeStartedPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTrans.anchoredPosition += eventData.delta;
        Debug.Log("on drag");
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
