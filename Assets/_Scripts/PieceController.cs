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
        public Transform[] cells;
        public Rotation rot;


        public void Setup(PieceData data, Transform[] cells)
        {
            this.data = data;
            this.cells = cells;
        }
        public void Repaint(Rotation rot)
        {
            this.rot = rot;
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].localPosition = GameManager.PosGen(rot, data.Coordinations[i]);
            }
        }
    }
}
