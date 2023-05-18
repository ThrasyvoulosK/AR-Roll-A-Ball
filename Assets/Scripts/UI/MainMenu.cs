using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, ICrossFadeHolder
{
    public Button playGameButton;
    public Button quitGameButton;
    public Button selectLevelButton;
    public Button goBackButton;

    private CrossFade crossFade;

    [SerializeField]
    private GameObject buttonContainer;

    [SerializeField]
    private GameObject levelContainer;

    public void EnableCrossFade()
    {
        crossFade = GetComponentInChildren<CrossFade>(true);
        crossFade.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        EnableCrossFade();   

        playGameButton.onClick.AddListener(delegate {
            ScoreManager.hasPlayerStartedFromTheBeggining = true;
            StartCoroutine(crossFade.MoveToNextScene());
        });

        quitGameButton.onClick.AddListener(delegate {


            Application.Quit();
        
        });

        selectLevelButton.onClick.AddListener(delegate {

            levelContainer.SetActive(true);

            buttonContainer.SetActive(false);
        
        });
        
        goBackButton.onClick.AddListener(delegate {

            buttonContainer.SetActive(true);

            levelContainer.SetActive(false);
        
        });
    }
}
