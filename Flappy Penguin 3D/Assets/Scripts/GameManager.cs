using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public GameObject startPage;
    public GameObject gameOverPage;
    //public GameObject countdownPage;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public static int Score = 0;

    public static bool IsGameOvered { get; private set; } = true;

    enum States{
        StartPage,
        //CountDown,
        GamePlay,
        GameOverPage
    };

    private void Start()
    {
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
        Time.timeScale = 0;
    }

    void SetStatePage(States state)
    {
        switch (state)
        {
            case States.StartPage:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                //countdownPage.SetActive(false);
                break;
            //case States.CountDown:
            //    startPage.SetActive(false);
            //    gameOverPage.SetActive(false);
            //    countdownPage.SetActive(true);
            //    break;
            case States.GamePlay:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                //countdownPage.SetActive(false);
                break;
            case States.GameOverPage:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                //countdownPage.SetActive(false);
                break;
            default:
                Debug.LogError("Err: Unknown state called in \"SetStatePage\" method");
                break;
        }
    }

    //public void OnCountdownFinished()
    //{
    //    SetStatePage(States.GamePlay);
    //    Score = 0;
    //    IsGameOvered = false;
    //    Time.timeScale = 1;
    //    OnGameStarted();
    //}

    public void OnPlayerDied()
    {
        IsGameOvered = true;
        var highscore = PlayerPrefs.GetInt("Highscore");
        if (Score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", Score);
            highscoreText.text = "Highscore: " + Score;
        }

        Time.timeScale = 0;
        TapController.IsGameStarted = false;
        SetStatePage(States.GameOverPage);
    }

    public void OnPlayerScored()
    {
        Score++;
        scoreText.text = Score.ToString();
    }

    public void RestartGame()
    {
        scoreText.text = "0";
        Score = 0;
        OnGameOverConfirmed();
        StartGame();
    }

    public void StartGame()
    {
        //SetStatePage(States.CountDown);
        SetStatePage(States.GamePlay);
        Time.timeScale = 1;
        OnGameStarted();
    }

    void OnEnable()
    {
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerScored += OnPlayerScored;
        //CountdownScript.OnCountdownFinished += OnCountdownFinished;
    }
    void OnDisable()
    {
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerScored -= OnPlayerScored;
        //CountdownScript.OnCountdownFinished -= OnCountdownFinished;
    }
}
