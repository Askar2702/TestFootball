using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameMode GameState { get; private set; }
    #region UI
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _startGame;
    [SerializeField] private TextMeshProUGUI _textButton;
    [SerializeField] private Button _exitGame;
    [SerializeField] private Button _pauseGame;
    #endregion
    private void Awake()
    {
        if (!Instance) Instance = this;
        GameController.Instance.Finishing += FinishGame;
        _startGame.onClick.AddListener(() => StartGame());
        _exitGame.onClick.AddListener(() => ExitGame());
        _pauseGame.onClick.AddListener(() => PauseGame());
    }
    private void Start()
    {
        GameState = GameMode.Start;
    }
   
    private void FinishGame()
    {
        GameState = GameMode.Finish;
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        GameState = GameMode.Play;
    }

    #region UIButtons
    private void StartGame()
    {
        GameState = GameMode.Play;
        _panel.SetActive(false);
        _pauseGame.gameObject.SetActive(true);
    }
    private void PauseGame()
    {
        GameState = GameMode.Pause;
        _panel.SetActive(true);
        _textButton.text = "Continue";
        _pauseGame.gameObject.SetActive(false);
    }
    private void ExitGame()
    {
        GameState = GameMode.Finish;
        Application.Quit();
    }
    #endregion
}
