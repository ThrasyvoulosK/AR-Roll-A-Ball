using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour, ICrossFadeHolder
{
    public static int score = 0;
    public static int levelScore = 0;
    public static float time = 0;

    public static bool hasPlayerStartedFromTheBeggining = true;

    public TMP_Text scoreText;
    public TMP_Text timerText;

    public Collectible[] collectibles;
    private CrossFade crossFade;


    private void Awake()
    {
        CollectibleSpawner.onCollectiblesSpawned += FindCollectibles;

        SceneManager.sceneLoaded += InitializeVariables;

        Collectible.onPickedUp += SetScore;

        scoreText.gameObject.SetActive(hasPlayerStartedFromTheBeggining);
        timerText.gameObject.SetActive(hasPlayerStartedFromTheBeggining);

    }

    // Start is called before the first frame update
    void Start()
    {
        EnableCrossFade();

        FindCollectibles();

        DisplayScore(PlayerPrefs.GetInt(SavedValues.SCORE, 0));

        SaveScore();

        DontDestroyOnLoad(this.gameObject);
    }

    void FindCollectibles()
    {
        collectibles = FindObjectsOfType<Collectible>();

    }

    void Update()
    {
        time += Time.deltaTime;
        timerText.text = FormatTime();
    }

    string FormatTime()
    {
        //130 s -> 2 min 10 second
        //130 - 2 * 60 = 10 seconds
        int minutes = Mathf.FloorToInt(time/60f);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);
        return string.Format("{0:0}:{1:00}", minutes, seconds);

    }

    void SaveScore()
    {
        score = PlayerPrefs.GetInt(SavedValues.SCORE, 0);

    }

    void DisplayScore(int points)
    {
        scoreText.text = "Score: " + points;
    }

    private void SetScore(int points)
    {
        StartCoroutine(SetScoreCoroutine(points));
    }

    private IEnumerator SetScoreCoroutine(int points)
    {
        score += points;
        DisplayScore(score);

        //Περιμενε ενα Frame
        yield return null;

        if (HasPlayerCollectedAllCollectibles())
        {
            scoreText.text = "You win!";
            StartCoroutine(crossFade.MoveToNextScene());
        }
    }

    private void InitializeVariables(Scene arg0, LoadSceneMode arg1)
    {
        EnableCrossFade();

        FindCollectibles();

        DisplayScore(PlayerPrefs.GetInt(SavedValues.SCORE, 0));

        SaveScore();
    }

    bool HasPlayerCollectedAllCollectibles()
    {
        foreach (var collectible in collectibles)
        {
            if (collectible.gameObject.activeInHierarchy)
                return false;
        }

        return true;
    }

    private void OnDisable()
    {
        CollectibleSpawner.onCollectiblesSpawned -= FindCollectibles;

        SceneManager.sceneLoaded -= InitializeVariables;

        Collectible.onPickedUp -= SetScore;

    }

    public void EnableCrossFade()
    {
        crossFade = GetComponentInChildren<CrossFade>(true);
        crossFade.gameObject.SetActive(true);
    }
}
