using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    private BabyControl _babyControl;
    public Vector2 endPos = new Vector3(10, 10);
    public float speed = 1.0f;

    public bool beginMove = false;

    private void Awake()
    {
        _babyControl = GetComponent<BabyControl>();
    }

    public void BeginMove(Vector2 pos)
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
            var playerPos = transform.position;
            transform.LookAt(new Vector3(endPos.x, playerPos.y, endPos.y));
            if (new Vector2(playerPos.x , playerPos.z) != endPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(endPos.x, playerPos.y, endPos.y), Time.deltaTime * speed);
            }
            else
            {
                Debug.Log("到达目的地");
                _babyControl.EndMove();
                beginMove = false;
            }
        }
    }
}
