using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour, ICrossFadeHolder
{
    private CrossFade crossFade;

    public Button retryButton;
    public Button quitGameButton;
    public TMP_Text finalScoreText;
    public TMP_Text finalTimeText;


    // Start is called before the first frame update
    void Start()
    {
        EnableCrossFade();

        retryButton.onClick.AddListener(delegate {

            ScoreManager.hasPlayerStartedFromTheBeggining = true;
            StartCoroutine(crossFade.MoveToScene(1));
        
        
        });

        quitGameButton.onClick.AddListener(delegate {

            Application.Quit();
        
        });
    }

    public void EnableCrossFade()
    {
        crossFade = GetComponentInChildren<CrossFade>(true);
        crossFade.gameObject.SetActive(true);
    }
}
