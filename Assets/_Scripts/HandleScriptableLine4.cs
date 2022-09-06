using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScriptableLine4 : MonoBehaviour
{
    public ScriptableLine4 scriptableLine4;
    public GameObject cellToSpawn;

    void Start()
    {
        SpawnLine4();
    }

    void SpawnLine4()
    {
        GameObject parent = new("piece");
        //parent.transform.parent = bottompanel;
        parent.transform.position = new Vector2(-2, -10);

        for (int x = 0; x < 4; x++)
        {
            float posx = scriptableLine4.cellPosition[x,0];
            float posy = scriptableLine4.cellPosition[x,1];

            GameObject blockObj = Instantiate(cellToSpawn);
            blockObj.transform.SetParent(parent.transform);
            blockObj.GetComponent<SpriteRenderer>().size = new Vector2(1, 1);
            blockObj.transform.localPosition = new Vector2(posx, posy);
            
        }

    }
}
