using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks;

    private bool taskBegin;

    private void Start()
    {
        tasks = Shuffle(tasks);
    }

    private void Update()
    {
        if(GameManager.Instance.gameState == 2 && !taskBegin)
        {
            taskBegin = true;
            tasks[0].Init();
            StartCoroutine(GenerateTask());
        }
    }

    private IEnumerator GenerateTask()
    {
        for(int i = 1; i < tasks.Count; i++)
        {
            yield return new WaitForSeconds(20);

            tasks[i].Init();
        }
    }

    /// <summary>
    /// 随机打乱事件
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    public List<Task> Shuffle(List<Task> original)
    {
        System.Random randomNum = new System.Random();
        int index = 0;
        Task temp;
        for (int i = 0; i < original.Count; i++)
        {
            index = randomNum.Next(0, original.Count - 1);
            if (index != i)
            {
                temp = original[i];
                original[i] = original[index];
                original[index] = temp;
            }
        }

        return original;
    }
}
