using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Linq;

public class RankingManager : MonoBehaviour
{
    float firstPosition = -25f;
    float spacePosition = -25f;
    public List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();
    public List<GameObject> positions = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform text in transform)
        {
            textList.Add(text.GetComponent<TextMeshProUGUI>());
        }

        positions = positions.OrderBy(z => transform.position.z).ToList();
        
        StartCoroutine(ListRankings());
        StartCoroutine(ScaleRankings());
        StartCoroutine(SortRankings());

    }

    IEnumerator ListRankings()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < textList.Count; i++)
        {
            textList[i].transform.DOLocalMoveY((spacePosition * i) + firstPosition , 1.0f);
        }
    }

    IEnumerator ScaleRankings()
    {
        yield return new WaitForSeconds(1.2f);
        for (int i = 0; i < textList.Count; i++)
        {
            textList[i].transform.DOScale(Vector3.one, 0.8f);
        }
    }

    IEnumerator SortRankings()
    {
        yield return new WaitForSeconds(1.0f);
        positions = positions.OrderByDescending(temp => temp.transform.position.z).ToList();

        for (int i = 0; i < textList.Count; i++)
        {
            textList[i].text = positions[i].name;
        }
        StartCoroutine(SortRankings());
    }

    public string GetWinner()
    {
        return positions[0].name;
    }

    // public void PlayerGameEnd()
    // {

    //     for (int i = 1; i < positions.Count; i++)
    //     {
    //         if(positions[i].name == "Player")
    //         {
    //             positions[i].GetComponent<EnemyMovement>().CryAnimation();
    //         }
            
    //     }
    // }

    
}
