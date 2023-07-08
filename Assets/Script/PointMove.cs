using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    public Vector3 endPos = new Vector3(10, 1, 10);
    public float speed = 1.0f;

    public bool beginMove = false;
    

    // Start is called before the first frame update

    public void BeginMove(Vector3 pos)
    {
        endPos = pos;
        beginMove = true;
    }

    public void EndMove()
    {
        beginMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (beginMove)
        {
            transform.LookAt(endPos);
            if (transform.position != endPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
            }
            else
            {
                Debug.Log("到达目的地");
                beginMove = false;
                ///这里可以执行一些功能。反正也只能执行一次
            }
        }
    }
}
