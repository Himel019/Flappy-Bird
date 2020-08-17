using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text endScore;
    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Button resumeOrRestartGameButton;
    [SerializeField]
    private Button instructionsButton;
    [SerializeField]
    private Button shareButton;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject[] birds;

    [SerializeField]
    private Sprite[] medals;

    [SerializeField]
    private Image medalImage;

    private string shareMessage;

    void Awake() {
        MakeInstance();
        Time.timeScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void  MakeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    public void PauseGame() {
        if(Player.instance != null) {
            if(Player.instance.IsAlive()) {
                pausePanel.SetActive(true);
                gameOverText.gameObject.SetActive(false);
                shareButton.gameObject.SetActive(false);
                endScore.text = Player.instance.GetScore().ToString();
                bestScore.text = GameController.instance.GetHighScore().ToString();
                Time.timeScale = 0f;
                
                float score = Player.instance.GetScore();

                if(score <= 30) {
                    medalImage.sprite = medals[0];
                } else if(score > 30 &&  score < 60) {
                    medalImage.sprite = medals[1];
                } else {
                    medalImage.sprite = medals[2];
                }

                resumeOrRestartGameButton.onClick.RemoveAllListeners();
                resumeOrRestartGameButton.onClick.AddListener(() => ResumeGame());
            }
        }
    }

    public void GoToMenuButton() {
        SceneFader.instance.FadeIn("1_MainMenu");

        if(Random.Range(1, 10) < 5) {
            UnityAdsScript.instance.ShowInterstitialAd();
        } else {
            UnityAdsScript.instance.ShowRewardedVideo();
        }
    }

    public void ResumeGame() {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame() {
        Scene scene = SceneManager.GetActiveScene();
        SceneFader.instance.FadeIn(scene.name);
    }

    public void PlayGame() {
        scoreText.gameObject.SetActive(true);
        birds[GameController.instance.GetSelectedBird()].SetActive(true);
        instructionsButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetScore(int score) {
        scoreText.text = score.ToString();
    }

    public void PlayerDiedShowScore(int score) {
        pausePanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        shareButton.gameObject.SetActive(true);

        endScore.text = score.ToString();

        if(score > GameController.instance.GetHighScore()) {
            GameController.instance.SetHighScore(score);
        }

        bestScore.text = GameController.instance.GetHighScore().ToString();

        if(score <= 30) {
            medalImage.sprite =  medals[0];
        } else if(score > 30 &&  score < 60) {
            medalImage.sprite = medals[1];

            if(GameController.instance.IsGreenBirdUnlocked() == 0) {
                GameController.instance.UnlockGreenBird();
            }
        } else {
            medalImage.sprite = medals[2];

            if(GameController.instance.IsRedBirdUnlocked() == 0) {
                GameController.instance.UnlockRedBird();
            }
        }

        resumeOrRestartGameButton.onClick.RemoveAllListeners();
        resumeOrRestartGameButton.onClick.AddListener(() => RestartGame());
    }

    public void ShareScore() {
        shareMessage = "Whippersnapper scored " + scoreText.text + " points in Flappy Bird";

        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();

        string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
        File.WriteAllBytes( filePath, ss.EncodeToPNG() );

        // To avoid memory leaks
        Destroy( ss );

        new NativeShare().AddFile( filePath )
            .SetSubject( "Flappy Bird" ).SetText( shareMessage )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();
    }
}
