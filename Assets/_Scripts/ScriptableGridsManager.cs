using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class ScriptableGridsManager : ScriptableObject
{
    public List<ScriptableObject> scriptableGrids;
    public GameObject bottomPanel;
    [SerializeField] Vector2 bottomPanelSize;


    void Start()
    {
        InitializeBottomPanel();
    }

    public void InitializeBottomPanel()
    {
        GameObject bottomPanelObj = Instantiate(bottomPanel);
        bottomPanelObj.GetComponent<SpriteRenderer>().size = new Vector2(bottomPanelSize.x, bottomPanelSize.y);
        bottomPanelObj.transform.position = new Vector2(0, 6);

    }

}
