using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public int costAnger = 20;

    private bool isStart;
    private bool canExecute;
    private bool isComplete;
    private int requestedTime = 10;
    private bool isExecuting;
    [SerializeField] private float timer = 0;

    private void Update()
    {
        if(canExecute && Input.GetKeyDown(KeyCode.J))
        {
            isExecuting = true;
            UIManager.Instance.StartRelieveTiming();
        }
        if(isExecuting)
        {
            timer += Time.deltaTime;
        }
        if(isExecuting && Input.GetKeyUp(KeyCode.J))
        {
            isExecuting = false;
        }
        if(timer > requestedTime)
        {
            isComplete = true;
            UIManager.Instance.MinusAnger(costAnger);
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0 && isStart)
        {
            canExecute = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0 && !isComplete && isStart)
        {
            isExecuting = false;
            UIManager.Instance.StartAngerTiming();
        }
    }

    public void Init()
    {
        isStart = true;
        this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
