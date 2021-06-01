using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Vector3 firstPosition;
    Quaternion firstRotation;
    NavMeshAgent _navMeshAgent;
    Vector3 targetPosition;
    Animator _animator;
    Rigidbody _rigidbody;

    public bool pushLeft;
    public bool pushRight;
    public float forceAmount;

    bool called = false;
    bool gameWin = false;
    // Start is called before the first frame update
    void Start()
    {
        firstPosition = transform.position;
        firstRotation = transform.rotation;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = transform.Find("ModelHolder").GetChild(0).GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        GameManager.Instance.OnLevelStart += ()=>{
            targetPosition = GameObject.Find("GameEndPosition").transform.position;
            _navMeshAgent.SetDestination(targetPosition);
            _animator.SetTrigger("Run");
        };

        GameManager.Instance.OnLevelEnd += ()=>{
            if(!gameWin)
                GameEnd();
        };
    }

    void OnCollisionEnter(Collision other) 
    {
        if(!other.gameObject.CompareTag("Opponent") && other.gameObject.CompareTag("Obstacle"))
        {
//            Debug.Log(name + " collided with " + other.gameObject.name);
            transform.position = firstPosition;
            transform.rotation = firstRotation;
        }
    }

    void Update() 
    {
        if(pushLeft)
        {
            _rigidbody.AddForce(Vector3.left * forceAmount , ForceMode.Force);
        }
        if(pushRight)
        {
            _rigidbody.AddForce(Vector3.right * forceAmount, ForceMode.Force);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "GameEndTrigger" && !called)
        {
            Debug.Log("Enemy " + name);
            _animator.SetTrigger("Dance");
            called = true;
            gameWin = true;
            GameManager.Instance.GameFail();
        }
    }

    void GameEnd()
    {
        _navMeshAgent.isStopped = true;
        if(!gameWin)
        {
            _animator.SetTrigger("Cry");
        }
        else
            _animator.SetTrigger("Dance");
    }

}
