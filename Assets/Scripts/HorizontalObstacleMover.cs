using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HorizontalObstacleMover : MonoBehaviour
{
    [SerializeField]
    private float moveDuration = 2.0f;
    
    [SerializeField]
    private float collisionForceAmount = 5.0f;

    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        var firstX = transform.position.x;
        GameManager.Instance.OnLevelStart += () =>
        {
            Sequence mySequence = DOTween.Sequence();

            mySequence
                .Append(transform.DOMoveX( firstX * -1, moveDuration))
                .Append(transform.DOMoveX(firstX, moveDuration))
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        };
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //_playerMovement.StunMovement();
            Debug.Log("Collided from " + other.GetContact(0).point);
            if (other.GetContact(0).point.x > transform.position.x)
            {
                other.rigidbody.AddForce(Vector3.left * collisionForceAmount, ForceMode.Impulse);
            }
            else
            {
                other.rigidbody.AddForce(Vector3.right * collisionForceAmount, ForceMode.Impulse);
            }

            if (other.GetContact(0).point.z < transform.position.z)
            {
                other.rigidbody.AddForce(Vector3.back * collisionForceAmount, ForceMode.Impulse);
            }
            else
            {
                other.rigidbody.AddForce(Vector3.forward * collisionForceAmount, ForceMode.Impulse);
            }
            //other.rigidbody.AddForce();
        }
        
    }
}
