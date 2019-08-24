using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class QuadTreeLoad
{
    List<QTObject> m_QTGameObjectList = new List<QTObject>();

    Rect m_rect;

    public QuadTreeLoad(List<QTGameObjectData> datas, Rect rect)
    {
        CreadQuadTreeObject(datas, m_QTGameObjectList);
        m_rect = rect;
        QTGameObject.QTLoader = this;
    }


    /// <summary>
    /// 初始化数据，转换为四叉树操作对象
    /// </summary>
    /// <param name="datas"></param>
    /// <param name="qTGameObjects"></param>
    void CreadQuadTreeObject(List<QTGameObjectData> datas, List<QTObject> qTGameObjects)
    {
        if (qTGameObjects != null)
            qTGameObjects.Clear();
        datas.ForEach(item =>
        {
            QTGameObject qTGameObject = QTGameObject.CreatQTGameObject(item.obj, item.bounds);
            if (qTGameObject != null)
                qTGameObjects.Add(qTGameObject);
        });
       
    }

    /// <summary>
    /// 初始化，构建二叉树
    /// </summary>
    public void Initialize()
    {
        CreatQuadTree();
    }


    public QuadTree<QTObject> m_QuadTree = null;
    void CreatQuadTree()
    {
        m_QuadTree = new QuadTree<QTObject>();
        m_QuadTree.Init(m_rect, m_QTGameObjectList);


#if UNITY_EDITOR
        //绘制四叉树
        var root = m_QuadTree.Root;
        DrawQuadTree(root, null);
#endif
    }


#if UNITY_EDITOR
    Dictionary<QuadTreeNode<QTObject>, Transform> m_dicObjectNode = new Dictionary<QuadTreeNode<QTObject>, Transform>();
    void DrawQuadTree(QuadTreeNode<QTObject> treeNode, Transform parent)
    {
        if (treeNode == null)
            return;
        if (parent == null)
            parent = (new GameObject("QTParent")).transform;

        GameObject node = new GameObject(treeNode.Rect.ToString(), typeof(DrawQTGameObject));
        node.transform.position = new Vector3(treeNode.Rect.center.x, 0, treeNode.Rect.center.y);
        node.transform.SetParent(parent);
        var qtgo = node.GetComponent<DrawQTGameObject>();
        qtgo.QTNode = treeNode;
        GameObject go = new GameObject("Object");
        go.transform.SetParent(node.transform);
        m_dicObjectNode.Add(treeNode, go.transform);

        DrawQuadTree(treeNode.LT, node.transform);
        DrawQuadTree(treeNode.LB, node.transform);
        DrawQuadTree(treeNode.RT, node.transform);
        DrawQuadTree(treeNode.RB, node.transform);
    }
#endif
}
