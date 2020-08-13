using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;

    [SerializeField]
    private GameObject fadeCanvas;
    [SerializeField]
    private Animator fadeAnim;

    void Awake() {
        MakeSingleton();
    }

    private void MakeSingleton() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void FadeIn(string levelName) {
        StartCoroutine(FadeInAnimation(levelName));
    }

    public void FadeOut() {
        StartCoroutine(FadeOutAnimation());
    }

    private IEnumerator FadeInAnimation(string levelName) {
        fadeCanvas.SetActive(true);
        fadeAnim.Play("FadeIn");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.7f));
        SceneManager.LoadScene(levelName);
        FadeOut();
    }

    private IEnumerator FadeOutAnimation() {
        fadeAnim.Play("FadeOut");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f));
        fadeCanvas.SetActive(false);
    }
}
