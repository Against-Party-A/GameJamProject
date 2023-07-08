using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float timer;
    private bool isChange;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > Settings.prepareTime && !isChange)
        {
            GameManager.Instance.ChangeGameState();
            isChange = true;
        }
    }
}
