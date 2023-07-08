using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image angerBar;

    [SerializeField]private float angerAmount;
    private int angerMax = 100;
    [SerializeField]private float angerTimer;
    private bool isAnger;
    private bool isRelieve;

    private void Start()
    {
        angerBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(GameManager.Instance.gameState == 2)
            Timetick();
    }

    private void Timetick()
    {
        if (isAnger)
        {
            if (angerTimer > Settings.angerThreshold)
            {
                angerAmount++;
                angerBar.fillAmount = angerAmount / angerMax;
                angerTimer = 0;
            }
            else
            {
                angerTimer += Time.deltaTime;
            }
        }
        else if (isRelieve)
        {
            if (angerTimer > Settings.relieveThreshold)
            {
                angerAmount--;
                angerBar.fillAmount = angerAmount / angerMax;
                angerTimer = 0;
            }
            else
            {
                angerTimer += Time.deltaTime;
            }
        }
    }

    public void StartAngerTiming()
    {
        isAnger = true;
        isRelieve = false;
    }

    public void StartRelieveTiming()
    {
        isAnger = false;
        isRelieve = true;
    }
}
