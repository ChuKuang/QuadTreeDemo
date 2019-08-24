using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneObjectsCheck : MonoBehaviour
{
    public Transform player;
    public List<QTGameObjectData> sceneObjectList = new List<QTGameObjectData>();
    QuadTreeLoad treeLoad;

    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            QTGameObjectData data = new QTGameObjectData();
            data.obj = child.gameObject;
            data.obj.SetActive(false);
            data.bounds = new Bounds(child.position, child.localScale);
            sceneObjectList.Add(data);
        }

        treeLoad = new QuadTreeLoad(sceneObjectList, new Rect(new Vector2(-50,-50), Vector2.one * 100f));
        treeLoad.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(player.position);
       treeLoad.m_QuadTree.Curser(new Vector2(player.position.x, player.position.z ));
    }

}
