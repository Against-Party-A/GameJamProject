using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public int costAnger = 20;

    private bool isComplete;
    private int requestedTime = 10;
    private bool isExecuting;
    [SerializeField] private float timer = 0;

    private void Update()
    {
        if(isExecuting)
        {
            timer += Time.deltaTime;
        }
        if(timer > requestedTime)
        {
            isComplete = true;
            UIManager.Instance.MinusAnger(costAnger);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0)
        {
            isExecuting = true;
            UIManager.Instance.StartRelieveTiming();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0 && !isComplete)
        {
            isExecuting = false;
            UIManager.Instance.StartAngerTiming();
        }
    }
}
