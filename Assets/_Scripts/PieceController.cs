using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class PieceController:MonoBehaviour
    {
        public PieceData data;
        public SpriteRenderer[] cellSprites;
        public Rotation rot;

        Vector3[] localPose;

        public void Setup(PieceData data, Transform[] cells)
        {
            this.data = data;
            this.cellSprites = new SpriteRenderer[cells.Length];
            localPose = new Vector3[cells.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                this.cellSprites[i] = cells[i].GetChild(0).GetComponent<SpriteRenderer>();
            }
        }
        public void Repaint(Rotation rot)
        {
            this.rot = rot;
            for (int i = 0; i < cellSprites.Length; i++)
            {
                localPose[i] = GameManager.PosGen(rot, data.Coordinations[i]);
                cellSprites[i].transform.parent.localPosition = localPose[i];
            }
        }

        public void CleanPoes()
        {
            for (int i = 0; i < cellSprites.Length; i++)
            {
                cellSprites[i].transform.parent.localPosition = localPose[i];
            }
        }
    }
}
