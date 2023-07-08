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
        StartCoroutine(StateTwoStart());
    }

    public IEnumerator StateTwoStart()
    {
        yield return new WaitForSeconds(2); 
        UIManager.Instance.angerBar.gameObject.SetActive(true);
    }
}
