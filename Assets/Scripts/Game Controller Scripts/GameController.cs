using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private const string HIGH_SCORE = "High_Score";
    private const string SELECTED_BIRD = "Selected_Bird";
    private const string GREEN_BIRD = "Green_Bird";
    private const string RED_BIRD = "Red_Bird";

    private void Awake() {
        //PlayerPrefs.DeleteAll();

        MakeInstance();
        IsTheGameStartingForTheFirstTime();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void MakeInstance() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void IsTheGameStartingForTheFirstTime() {
        if(!PlayerPrefs.HasKey("IsTheGameStartingForTheFirstTime")) {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt(SELECTED_BIRD, 0);
            PlayerPrefs.SetInt(GREEN_BIRD, 0);
            PlayerPrefs.SetInt(RED_BIRD, 0);
            PlayerPrefs.SetInt("IsTheGameStartingForTheFirstTime", 0);
        }
    }

    public void SetHighScore(int score) {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int GetHighScore() {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }

    public void SetSelectedBird(int selectedBird) {
        PlayerPrefs.SetInt(SELECTED_BIRD, selectedBird);
    }

    public int GetSelectedBird() {
        return PlayerPrefs.GetInt(SELECTED_BIRD);
    }

    public void UnlockGreenBird() {
        PlayerPrefs.SetInt(GREEN_BIRD, 1);
    }

    public int IsGreenBirdUnlocked() {
        return PlayerPrefs.GetInt(GREEN_BIRD);
    }

    public void UnlockRedBird() {
        PlayerPrefs.SetInt(RED_BIRD, 1);
    }

    public int IsRedBirdUnlocked() {
        return PlayerPrefs.GetInt(RED_BIRD);
    }
}
