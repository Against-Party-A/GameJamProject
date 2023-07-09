using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int gameState = 1;

    private float timer;

    private void Update()
    {

    }

    public void ChangeGameState()
    {
        gameState = 2;
        if (Player.Instance.transform.Find("IKUN666") == null)
        {
            StartCoroutine(StateTwoStart());
        }
        else
        {
            UIManager.Instance.PlayEnd(0);
        }
    }

    public IEnumerator StateTwoStart()
    {
        yield return new WaitForSeconds(2); 
        UIManager.Instance.angerBar.gameObject.SetActive(true);
    }
}
