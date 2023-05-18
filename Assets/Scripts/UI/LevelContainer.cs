using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelContainer : MonoBehaviour
{
    public List<Button> levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        AssignLevelButtons();

        AssignLevelButtonFunctionality();

        DisableButtonAccordingToLevelReached();

    }

    private void DisableButtonAccordingToLevelReached()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            int level = i + 1;
            if(PlayerPrefs.GetInt(SavedValues.REACHED_LEVEL, 1) < level)
            {
                levelButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void AssignLevelButtonFunctionality()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            int level = i + 1;
            levelButtons[i].onClick.AddListener(delegate {
                if(level == 1)
                {
                    ScoreManager.hasPlayerStartedFromTheBeggining = true;
                }
                else
                {
                    ScoreManager.hasPlayerStartedFromTheBeggining = false;
                }
                StartCoroutine(FindObjectOfType<CrossFade>().MoveToScene(level));
            });
        }
    }

    void AssignLevelButtons()
    {
        levelButtons.AddRange(GetComponentsInChildren<Button>());
        foreach (Button button in levelButtons)
        {
            if (button.GetComponentInChildren<TMP_Text>().text.Equals("GO BACK"))
            {
                levelButtons.Remove(button);
                break;
            }
        }
    }
}
