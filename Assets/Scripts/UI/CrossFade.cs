using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossFade : MonoBehaviour
{
    public Animator crossFadeAnimator;
    public float animationTime = 1f;

    public const string FadeIn = "FadeIn";
 
    // Start is called before the first frame update
    void Start()
    {
        crossFadeAnimator = GetComponent<Animator>();
    }

    public IEnumerator MoveToNextScene()
    {
        crossFadeAnimator.SetTrigger(FadeIn);

        yield return new WaitForSecondsRealtime(animationTime);

        ScoreManager.levelScore = ScoreManager.score;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public IEnumerator MoveToScene(int index)
    {
        crossFadeAnimator.SetTrigger(FadeIn);

        yield return new WaitForSeconds(animationTime);

        ScoreManager.levelScore = ScoreManager.score;

        SceneManager.LoadScene(index);
    }

}
