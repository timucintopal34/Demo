using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    
    public UnityAction OnLevelStart,OnLevelEnd;
    public Transform paintGameObject;
    public Transform percentageUI;
    
    private static object _lock = new object();
    private static GameManager _instance;
    private PlayerMovement _playerMovement;

    UIManager _uiManager;

    RankingManager _rankingManager;

    public static GameManager Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null) _instance = (GameManager) FindObjectOfType(typeof(GameManager));

                return _instance;
            }
        }
    }
    void Start()
    {
        _rankingManager = FindObjectOfType<RankingManager>();
        paintGameObject.gameObject.SetActive(false);
        _uiManager = FindObjectOfType<UIManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void GameStart()
    {
        OnLevelStart?.Invoke();
    }

    public void GameWin()
    {
        paintGameObject.gameObject.SetActive(true);
        percentageUI.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        _uiManager.GameWinText();
        OnLevelEnd?.Invoke();
       // _rankingManager.
    }

    public void GameFail()
    {
        _uiManager.GameFailText();
        OnLevelEnd?.Invoke();
    }

}
