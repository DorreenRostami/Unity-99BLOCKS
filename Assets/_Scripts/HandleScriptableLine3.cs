using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScriptableLine3 : MonoBehaviour
{
    public ScriptableLine3 scriptableLine3;
    public GameObject cellToSpawn;

    void Start()
    {
        SpawnEntities();
    }

    void SpawnEntities()
    {
        GameObject parent = new("piece");
        //parent.transform.parent = bottompanel;
        parent.transform.position = new Vector2(0, -10);

        for (int x = 0; x < 3; x++)
        {
            float posx = scriptableLine3.cellPosition[x,0];
            float posy = scriptableLine3.cellPosition[x,1];

            GameObject blockObj = Instantiate(cellToSpawn);
            blockObj.transform.SetParent(parent.transform);
            blockObj.GetComponent<SpriteRenderer>().size = new Vector2(1, 1);
            blockObj.transform.localPosition = new Vector2(posx, posy);
            
        }

    }
}
