using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    private Transform m_player;
    void Start()
    {
        m_player = this.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            m_player.Translate(Vector3.forward * Time.deltaTime * 10f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_player.Translate(Vector3.left * Time.deltaTime * 10f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_player.Translate(Vector3.back * Time.deltaTime * 10f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_player.Translate(Vector3.right * Time.deltaTime * 10f);
        }
    }
}
