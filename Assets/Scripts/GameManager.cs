using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    public bool hasPlayerDied = false;
    bool death = false;

    [SerializeField]
    private GameObject scoreCanvas;

    [SerializeField]
    private GameObject pauseMenuCanvas;

    [SerializeField]
    private int endMenuBuildIndex = 4;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(this.gameObject);

        SceneManager.sceneLoaded += InitializeVariables;

        Debug.Log("Current Level: " + PlayerPrefs.GetInt(SavedValues.LEVEL));


    }


    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(gameObject);
    }

    private void InitializeVariables(Scene scene, LoadSceneMode arg1)
    {

        //Είμαστε στο MainMenu ή είμαστε στο Win Screen
        if (scene.buildIndex == 0 || scene.buildIndex == endMenuBuildIndex)
        {
            if (scene.buildIndex == endMenuBuildIndex)
            {
                SavePlayerLevel(1);

                var endGameMenu = FindObjectOfType<EndGameMenu>();

                endGameMenu.finalScoreText.gameObject.SetActive(ScoreManager.hasPlayerStartedFromTheBeggining);
                endGameMenu.finalScoreText.gameObject.SetActive(ScoreManager.hasPlayerStartedFromTheBeggining);

                ScoreManager.time = 0;

                if (ScoreManager.hasPlayerStartedFromTheBeggining)
                {
                    endGameMenu.finalScoreText.text = "Score: " + ScoreManager.score;
                    endGameMenu.finalTimeText.text = "Time: " + ScoreManager.time;
                }

                ScoreManager.levelScore = 0;
                SavePlayerScore(scene.buildIndex);

            }

            DestroyObjects<PauseMenu, ScoreManager>();
        }
        else
        {
            SaveMaximumLevel(scene.buildIndex);

            SavePlayerLevel(scene.buildIndex);

            SavePlayerScore(scene.buildIndex);

            InstantiateObjects<PauseMenu, ScoreManager>();

            if (FindObjectOfType<EventSystem>() == null)
                new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));

            player = FindObjectOfType<PlayerController>().gameObject;
            hasPlayerDied = false;
            death = false;

        }

    }

    void SaveMaximumLevel(int level)
    {
        SavedValues.MAXIMUM_LEVEL = level;

        if (SavedValues.MAXIMUM_LEVEL > PlayerPrefs.GetInt(SavedValues.REACHED_LEVEL, 1))
            PlayerPrefs.SetInt(SavedValues.REACHED_LEVEL, SavedValues.MAXIMUM_LEVEL);


    }

    void DestroyObjects<T, M>() where T : MonoBehaviour where M : MonoBehaviour
    {
        if (FindObjectOfType<T>() != null)
            Destroy(FindObjectOfType<T>().gameObject);


        if (FindObjectOfType<M>() != null)
            Destroy(FindObjectOfType<M>().gameObject);
    }

   
    void InstantiateObjects<T, M>() where T : PauseMenu where M : ScoreManager
    {
        if (FindObjectOfType<T>() == null)
            Instantiate(pauseMenuCanvas);


        if (FindObjectOfType<M>() == null)
            Instantiate(scoreCanvas);
    }

    void SavePlayerScore(int level)
    {
        if(level == endMenuBuildIndex)
        {
            ScoreManager.levelScore = 0;
            ScoreManager.score = 0;

            PlayerPrefs.SetInt(SavedValues.SCORE, ScoreManager.levelScore);
        }
        else
        {
            if(ScoreManager.levelScore != 0)
                PlayerPrefs.SetInt(SavedValues.SCORE, ScoreManager.levelScore);
        }

    }

    void SavePlayerLevel(int level)
    {
        SavedValues.CURRENT_LEVEL = level;

        PlayerPrefs.SetInt(SavedValues.LEVEL, SavedValues.CURRENT_LEVEL);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (player.transform.position.y < -5f)
            {
                player.SetActive(false);
                hasPlayerDied = true;
            }

            if (hasPlayerDied)
            {
                if (!death)
                    StartCoroutine(RespawnPlayer());
            }
        }
    }

    IEnumerator RespawnPlayer()
    {
        death = true;

        yield return new WaitForSecondsRealtime(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
