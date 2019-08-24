using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 基类，四叉树操作对象
/// </summary>
public abstract class QTObject : IQTCursor,IQTFill
{
   /// <summary>
   /// 数据操作者
   /// </summary>
    public  static QuadTreeLoad QTLoader { set; protected get; }


    /// <summary>
    /// 判定是否进入
    /// </summary>
    /// <param name="rect"></param>
    /// <returns></returns>
    public abstract bool IsFill(Rect rect);

    /// <summary>
    /// 执行进入
    /// </summary>
    /// <param name="qt"></param>
    public abstract void OnEnter(object qt);

    /// <summary>
    /// 执行离开
    /// </summary>
    /// <param name="qt"></param>
    public abstract void OnExit(object qt);


}

