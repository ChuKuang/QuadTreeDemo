using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 自定义的数据类，可以根据需求自己更改
/// 这里是用来测试
/// </summary>

[Serializable]
public class QTGameObjectData
{
    public GameObject obj;
    public Bounds bounds;
}



/// <summary>
/// 测试用于管理场景物体的加载
/// </summary>
public class QTGameObject : QTObject
{
    Rect m_rect;
    public QTGameObject()
    {

    }

    public QTGameObject(Rect rect)
    {
        m_rect = rect;
    }

    GameObject m_Obj;


    /// <summary>
    /// 判定当前对象是否在指定的包围包围盒内
    /// 初始化四叉树节点的时候，可判定将其分配在父节点还是子节点
    /// 可根据自己的需求进行判定
    /// </summary>
    /// <param name="rect"></param>
    /// <returns></returns>
    public override bool IsFill(Rect rect)
    {
        int iCount = 0;
        Vector2 ptLT = new Vector2(m_rect.xMin, m_rect.yMin);
        Vector2 ptLB = new Vector2(m_rect.xMin, m_rect.yMax);
        Vector2 ptRT = new Vector2(m_rect.xMax, m_rect.yMin);
        Vector2 ptRB = new Vector2(m_rect.xMax, m_rect.xMax);
        if(rect.Contains(ptLT))
        {
            iCount++;
        }
        if (rect.Contains(ptLB))
        {
            iCount++;
        }
        if (rect.Contains(ptRT))
        {
            iCount++;
        }
        if (rect.Contains(ptRB))
        {
            iCount++;
        }

        bool bInRect = iCount > 2;
        return bInRect;
    }

   
    /// <summary>
    /// 进入时的操作
    /// </summary>
    /// <param name="qt"></param>
    public override void OnEnter(object qt)
    {
        if(m_Obj.activeInHierarchy == false)
        {
            m_Obj.SetActive(true);
            Debug.Log("生成" + m_Obj.name);
        }
        
    }

    /// <summary>
    /// 离开时的操作
    /// </summary>
    /// <param name="qt"></param>
    public override void OnExit(object qt)
    {
        if (m_Obj.activeInHierarchy == true)
        {
            m_Obj.SetActive(false);
            Debug.Log("隐藏" + m_Obj.name);
        }
    }
    
    /// <summary>
    /// 设置操作的游戏物体对象
    /// </summary>
    /// <param name="Obj"></param>
    public void SetData(GameObject Obj)
    {
        this.m_Obj = Obj;
    }


    /// <summary>
    /// 创建四叉树数据对象
    /// </summary>
    /// <param name="Obj"></param>
    /// <param name="bounds"></param>
    /// <returns></returns>
    public static QTGameObject CreatQTGameObject(GameObject Obj, Bounds bounds)
    {
        if (Obj == null)
            return null;  
        Rect rect = new Rect(bounds.center.x - bounds.size.x / 2, bounds.center.z - bounds.size.z / 2, bounds.size.x, bounds.size.z);
        QTGameObject qTGameObject = new QTGameObject(rect);
        qTGameObject.SetData(Obj);
        return qTGameObject;
    }
}
