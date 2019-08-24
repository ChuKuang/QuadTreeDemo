using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQTCursor {
	void OnEnter(object qt);
	void OnExit(object qt);
}

public interface IQTFill
{
    bool IsFill(Rect rect);
}


/// <summary>
/// 节点
/// </summary>
/// <typeparam name="T"></typeparam>
public class QuadTreeNode<T> where T:IQTFill,IQTCursor
{
    public QuadTreeNode(Rect rect)
    {
        Rect = rect;
    }

    IQTCursor ICurser;
    public Rect Rect { get; protected set; }
    public QuadTreeNode<T> LT = null;
    public QuadTreeNode<T> LB = null;
    public QuadTreeNode<T> RT = null;
    public QuadTreeNode<T> RB = null;

    List<T> listObjects = new List<T>();

    public void Enter()
    {
        for(int i = 0; i < listObjects.Count; i++)
        {
            listObjects[i].OnEnter(this);
        }
    }

    public void Exit()
    {
        for (int i = 0; i < listObjects.Count; i++)
        {
            listObjects[i].OnExit(this);
        }
    }

    bool Fill(T item, Rect rect, ref QuadTreeNode<T> qtNode)
    {
        if(item.IsFill(rect))
        {
            if (qtNode == null)
                qtNode = new QuadTreeNode<T>(rect);
            qtNode.Fill(item);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 判定数据是否包含在当前节点的四个子节点
    /// 不在的话就存储在当前节点
    /// </summary>
    /// <param name="item"></param>
    public void Fill(T item)
    {
        Rect rtLT = new Rect(Rect.x, Rect.y, Rect.width / 2, Rect.height / 2);
        Rect rtLB = new Rect(Rect.x, Rect.y + Rect.height / 2, Rect.width / 2, Rect.height / 2);
        Rect rtRT = new Rect(Rect.x + Rect.width / 2, Rect.y, Rect.width / 2, Rect.height / 2);
        Rect rtRB = new Rect(Rect.x + Rect.width / 2, Rect.y + Rect.height / 2, Rect.width / 2, Rect.height / 2);

        if(Fill(item, rtLT,ref LT))
        {

        }
        else if (Fill(item, rtLB, ref LB))
        {

        }
        else if (Fill(item, rtRT, ref RT))
        {

        }
        else if (Fill(item, rtRB, ref RB))
        {

        }
        else
        {
            listObjects.Add(item);
        }
    }

    public QuadTreeNode<T> FindNode(Vector2 pos)
    {
        if(LT != null && LT.Rect.Contains(pos))
        {
            return LT;
        }
        else if(RT != null && RT.Rect.Contains(pos))
        {
            return RT;
        }
        else if (LB != null && LB.Rect.Contains(pos))
        {
            return LB;
        }
        else if (RB != null && RB.Rect.Contains(pos))
        {
            return RB;
        }
        else if (Rect.Contains(pos))
        {
            return this;
        }
        return null;
    }
}


/// <summary>
/// 四叉树
/// </summary>
/// <typeparam name="T"></typeparam>
public class QuadTree<T> where T:IQTFill, IQTCursor
{
    public QuadTreeNode<T> Root;
    Stack<QuadTreeNode<T>> StackQTNode = new Stack<QuadTreeNode<T>>();

    public void Init(Rect rect, List<T> objects)
    {
        if(Root != null || StackQTNode.Count != 0)
        {
            Debug.LogError("QuadTree inited");
            Clear();
        }
        Root = new QuadTreeNode<T>(rect);
        for (int i = 0; i < objects.Count; i++)
        {
            Root.Fill(objects[i]);
        }
    }

    /// <summary>
    /// 用于每帧执行
    /// </summary>
    /// <param name="curPos"></param>
    public void Curser(Vector2 curPos)
    {
        if(StackQTNode.Count == 0)
        {
            Enter(Root);
            Curser(Root, curPos);
        }
        else
        {
            var curNode = StackQTNode.Peek();
            Curser(curNode, curPos);
        }
    }


    void Curser(QuadTreeNode<T> curNode, Vector2 curPos)
    {
        if (curNode == null)
            return;
        var n = curNode.FindNode(curPos);
        if(n == null)
        {
            if (curNode == Root)
                return;
            Exit(curNode);
            Curser(StackQTNode.Peek(), curPos);
        }
        else if(n == curNode)
        {

        }
        else
        {
            Enter(n);
            Curser(n, curPos);
        }
    }


    void Enter(QuadTreeNode<T> curNode)
    {
        if (curNode == null)
            return;
        StackQTNode.Push(curNode);
        curNode.Enter();
        Debug.LogFormat("Enter ：{0}, {1}, {2}, {3}", curNode.Rect.x, curNode.Rect.y, curNode.Rect.width, curNode.Rect.height);
    }

    void Exit(QuadTreeNode<T> curNode)
    {
        if (curNode == null)
            return;
        curNode.Exit();
        StackQTNode.Pop();
        Debug.LogFormat("Enter ：{0}, {1}, {2}, {3}", curNode.Rect.x, curNode.Rect.y, curNode.Rect.width, curNode.Rect.height);
    }


    public void Clear()
    {
        Root = null;
        while(StackQTNode.Count != 0)
        {
            var item = StackQTNode.Pop();
            item.Exit();
        }
    }
}
