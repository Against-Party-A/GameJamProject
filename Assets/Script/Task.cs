using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public int costAnger = 20;

    public Canvas dialogue;
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
            Player.Instance.GetComponent<Animator>().SetBool("Dance", true);
        }
        if(isExecuting)
        {
            timer += Time.deltaTime;
        }
        if(isExecuting && Input.GetKeyUp(KeyCode.J))
        {
            isExecuting = false;
            UIManager.Instance.StartAngerTiming();
            Player.Instance.GetComponent<Animator>().SetBool("Dance", false);

        }
        if(timer > requestedTime)
        {
            isComplete = true;
            UIManager.Instance.MinusAnger(costAnger);
            //this.transform.localScale = new Vector3(1f, 1f, 1f);
            dialogue.gameObject.SetActive(false);
            UIManager.Instance.StartAngerTiming();
            this.GetComponent<Task>().enabled = false;
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
            canExecute = false;
            UIManager.Instance.StartAngerTiming();
        }
    }

    public void Init()
    {
        isStart = true;
        //this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        dialogue.gameObject.SetActive(true);
        UIManager.Instance.StartAngerTiming();
    }
}
