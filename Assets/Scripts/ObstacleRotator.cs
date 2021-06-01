using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotator : MonoBehaviour
{
    [SerializeField] private bool xAxisRotation = false;
    [SerializeField] private bool yAxisRotation = false;
    [SerializeField] private bool zAxisRotation = false;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject rotatingObject;

    private bool gameStarted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnLevelStart += () =>
        {
            gameStarted = true;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (xAxisRotation)
            {
                rotatingObject.transform.Rotate( rotateSpeed,0,0,Space.Self);
            }
            else if (yAxisRotation)
            {
                rotatingObject.transform.Rotate( 0,rotateSpeed,0,Space.Self);
            }
            else if (zAxisRotation)
            {
                rotatingObject.transform.Rotate( 0,0,rotateSpeed,Space.Self);
            }
        }
    }
}
