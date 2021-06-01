using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float gameStartDelay;
    [SerializeField] private GameObject gameStartTimerUI;
    TextMeshProUGUI uiText;
    RankingManager _rankingManager;
    
    // Game start UI timer counter
    void Start()
    {
        Sequence mySequence = DOTween.Sequence();

        uiText = gameStartTimerUI.GetComponent<TextMeshProUGUI>();
        _rankingManager = FindObjectOfType<RankingManager>();
        
        mySequence
            .Append(gameStartTimerUI.transform.DOScale(Vector3.one * 1.5f, 1.0f))
            .Append(gameStartTimerUI.transform.DOScale(Vector3.zero, 1.0f).OnComplete((() =>
            {
                uiText.text = "3";
            })))
            .Append(gameStartTimerUI.transform.DOScale(Vector3.one * 3.0f, 0.1f))
            .Append(gameStartTimerUI.transform.DOScale(Vector3.zero, 0.9f).OnComplete((() =>
            {
                uiText.text = "2";
            })))
            .Append(gameStartTimerUI.transform.DOScale(Vector3.one * 3.0f, 0.1f))
            .Append(gameStartTimerUI.transform.DOScale(Vector3.zero, 0.9f).OnComplete((() =>
            {
                uiText.text = "1";
            })))
            .Append(gameStartTimerUI.transform.DOScale(Vector3.one * 3.0f, 0.1f))
            .Append(gameStartTimerUI.transform.DOScale(Vector3.zero, 0.9f).OnComplete((() =>
            {
                uiText.text = "GO!";
                GameManager.Instance.GameStart();
            })))
            .Append(gameStartTimerUI.transform.DOScale(Vector3.one * 3.0f, 0.1f))
            .Append(gameStartTimerUI.transform.DOScale(Vector3.zero, 3f))
            .SetEase(ease: Ease.Linear);
    }

    //Called for success
    public void GameWinText()
    {
        uiText.text = "Success!";
        gameStartTimerUI.transform.DOScale(Vector3.one * 1.5f, 1f).SetEase(Ease.OutBack);
    }

    //Called for fail text
    public void GameFailText()
    {
        uiText.text = "Winner is " + _rankingManager.GetWinner();
        gameStartTimerUI.transform.DOScale(Vector3.one * 1.5f, 1f).SetEase(Ease.OutBack);
    }

}
