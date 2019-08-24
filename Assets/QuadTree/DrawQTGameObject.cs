using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DrawQTGameObject : MonoBehaviour
{
    public QuadTreeNode<QTObject> QTNode  { get; set; }

    private void OnDrawGizmosSelected()
    {
        DrawGizmosQT(QTNode.LT);
        DrawGizmosQT(QTNode.LB);
        DrawGizmosQT(QTNode.RT);
        DrawGizmosQT(QTNode.RB);
    }

    private void DrawGizmosQT(QuadTreeNode<QTObject> treeNode)
    {
        if (treeNode == null)
            return;
        Color color = Gizmos.color;
        Gizmos.color = Color.cyan;
        Vector2 center = treeNode.Rect.center;
        Gizmos.DrawWireCube(new Vector3(center.x, 0, center.y), new Vector3(treeNode.Rect.width, 1, treeNode.Rect.height));
        Gizmos.color = color;
    }
}
