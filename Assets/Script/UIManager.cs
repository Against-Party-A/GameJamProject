using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image angerBar;
    public Image barContainer;
    public Sprite[] angerSources;

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
        if (isAnger && angerAmount <= angerMax)
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

            if(angerAmount == angerMax)
            {
                barContainer.sprite = angerSources[1];
            }
        }
        else if (isRelieve && angerAmount >= 0)
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

            if(angerAmount < angerMax * 3 / 4)
            {
                barContainer.sprite = angerSources[0];
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
