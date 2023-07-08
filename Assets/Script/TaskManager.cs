using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<GameObject> tasks;

    private void Start()
    {
        tasks = Shuffle(tasks);
    }

    private void Update()
    {
        if(GameManager.Instance.gameState == 2)
        {
            Instantiate(tasks[0], this.transform);
            StartCoroutine(GenerateTask());
        }
    }

    private IEnumerator GenerateTask()
    {
        for(int i = 1; i < tasks.Count; i++)
        {
            yield return new WaitForSeconds(20);

            Instantiate(tasks[i], this.transform);
        }
    }

    /// <summary>
    /// 随机打乱事件
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    public List<GameObject> Shuffle(List<GameObject> original)
    {
        System.Random randomNum = new System.Random();
        int index = 0;
        GameObject temp;
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
