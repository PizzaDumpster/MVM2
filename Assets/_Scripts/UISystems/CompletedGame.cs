using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GameCompleted : BaseMessage { }
public class CompletedGame : MonoBehaviour
{
    public ScoreBoard scoreBoard;

    [Header("")]
    public GameObject onQuit;
    public GameObject onMainMenu;

    [Header("")]
    public TextMeshProUGUI deathCount;
    public TextMeshProUGUI time;


    void Awake()
    {
        MessageBuffer<GameCompleted>.Subscribe(GameCompletion);
        this.gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        MessageBuffer<GameCompleted>.Unsubscribe(GameCompletion);
    }

    private void GameCompletion(GameCompleted obj)
    {
        MessageBuffer<PauseMessage>.Dispatch();

        deathCount.text = scoreBoard.GetDeathCount().ToString();
        time.text = scoreBoard.GetCurrentTime();

        this.gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(onMainMenu);
    }
}
