using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    bool hasGamePaused = false;
    private CanvasGroup canvasGroup;

    public Button continueGameButton;
    public Button quitGameButton;
    public Button goToMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        continueGameButton.onClick.AddListener(delegate {

            ContinueGame();

            hasGamePaused = false;


        });

        quitGameButton.onClick.AddListener(delegate {

            Application.Quit();

        });

        goToMainMenu.onClick.AddListener(delegate {

            ContinueGame();

            ScoreManager.score = 0;

            SceneManager.LoadScene(0);

            Destroy(gameObject);
        
        });


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Αν το εχουμε σταματήσει
            if (!hasGamePaused)
            {
                PauseGame();
            }
            //Αν δεν το εχουμε σταματήσει
            else
            {

                ContinueGame();
            }

            hasGamePaused = !hasGamePaused;
        }

    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        //Με το 1 εμφανιζεται και με το 0 εξαφανίζεται
        canvasGroup.alpha = 1f;
    }

    void ContinueGame()
    {
        Time.timeScale = 1f;
        canvasGroup.alpha = 0f;
    }
}
