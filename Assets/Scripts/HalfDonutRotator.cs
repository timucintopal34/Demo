using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutRotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private bool gameStarted = false;
    private GameObject halfDonut;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        halfDonut = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        GameManager.Instance.OnLevelStart += () =>
        {
            if (halfDonut != null)
            {
                gameStarted = true;
            }
        };
    }

    void Update()
    {
        if (gameStarted)
            halfDonut.transform.Rotate( rotateSpeed,0,0,Space.Self);
    }
}
