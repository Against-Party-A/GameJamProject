using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TMP_Text time;
    public Image clock;
    private float timer;
    private bool isChange;

    private void Update()
    {
        timer += Time.deltaTime;
        time.text = "00:" + (Settings.prepareTime - timer).ToString("00");
        if(timer > Settings.prepareTime && !isChange)
        {
            GameManager.Instance.ChangeGameState();
            clock.gameObject.SetActive(false);
            isChange = true;
        }
    }
}
