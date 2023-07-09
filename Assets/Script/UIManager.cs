using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image angerBar;
    public Image barContainer;
    public Image soundImage;
    public VideoPlayer videoplayer;
    public Sprite[] angerSources;
    public Sprite[] soundSources;
    public VideoClip[] videoSources;
    public GameObject taughtPanel;
    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject endPanel;

    public float angerAmount;
    public int angerMax = 100;
    [SerializeField]private float angerTimer;
    private bool isAnger;
    private bool isRelieve;
    private bool isSoundOn = true;
    private int currentSound = 0;
    private bool isInTaughtPanel;

    public List<AudioSource> _audioSources;

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 0;
    }

    private void Start()
    {
        angerBar.gameObject.SetActive(false);
        soundImage.sprite = soundSources[0];
    }

    private void Update()
    {
        if(GameManager.Instance.gameState == 2)
            Timetick();
        if (isInTaughtPanel && Input.GetMouseButtonDown(0))
        {
            taughtPanel.SetActive(false);
            isInTaughtPanel = false;
            Time.timeScale = 1;
        }
    }

    public void StartGame()
    {
        menuPanel.SetActive(false);
        taughtPanel.SetActive(true);
        gamePanel.SetActive(true);
        isInTaughtPanel = true;
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeSound()
    {
        currentSound = 1 - currentSound;
        isSoundOn = !isSoundOn;

        foreach (var source in _audioSources)
        {
            source.volume = isSoundOn ? 100 : 0;
        }
        
        soundImage.sprite = soundSources[currentSound];
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
                Player.Instance.MoveToParents();
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

    public void ChangeBarContainer()
    {
        barContainer.sprite = angerSources[0];
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

    public void MinusAnger(int amount)
    {
        angerAmount -= amount;
        if (angerAmount < 0)
            angerAmount = 0;
    }

    public AudioSource bgm;
    public AudioSource victoryAudio;
    public AudioSource defeatAudio;

    public BabyControl _BabyControl;
    public void PlayEnd(int index)
    {
        if(endPanel.activeSelf) 
            return;
        
        endPanel.SetActive(true);
        videoplayer.clip = videoSources[index];
        videoplayer.Play();
        _BabyControl._playerState = PlayerState.EndGame;

        videoplayer.isLooping = index == 1;
        bgm.Stop();
        if (index == 1)
        {
            victoryAudio.Play();
        }
        else
        {
            defeatAudio.Play();
        }

    }
}
