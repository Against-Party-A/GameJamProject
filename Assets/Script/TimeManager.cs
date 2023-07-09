using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TMP_Text time;
    public Image clock;
    public Image countDown;
    public float actTime;   //小孩找的时间

    [SerializeField]private float timer;
    private bool isChange;

    private void Update()
    {
        timer += Time.deltaTime;
        if (!isChange )
        {
            time.text = ((int)((Settings.prepareTime - timer) / 60)).ToString("00") + ":" + ((int)(Settings.prepareTime - timer) % 60).ToString("00");
            countDown.fillAmount = (Settings.prepareTime - timer) / Settings.prepareTime;
            if (timer >= Settings.prepareTime)
            {
                GameManager.Instance.ChangeGameState();
                isChange = true;
                timer = 0;
            }
        }
        else if(isChange)
        {
            time.text = ((int)((actTime - timer) / 60)).ToString("00") + ":" + ((int)(actTime - timer) % 60).ToString("00");
            countDown.fillAmount = (actTime - timer) / actTime;
            if((actTime - timer) <= 0)
            {
                UIManager.Instance.PlayEnd(1);
            }
        }
    }
}
