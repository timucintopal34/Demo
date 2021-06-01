using System;
using UnityEngine;

public class PushPlayer : MonoBehaviour
{
    [SerializeField] 
    private float pushForce = 5.0f;
    [SerializeField]
    private bool pushBack = false;
    [SerializeField]
    private bool pushLeft = false;
    [SerializeField]
    private bool pushRight = false;
    [SerializeField]
    private bool continousForce = false;

    private bool collided = false;

    private Rigidbody targetRigidbody;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (collided && continousForce)
        {
            if(pushLeft)
            {
                targetRigidbody.velocity = new Vector3(targetRigidbody.velocity.x - (pushForce * Time.deltaTime) , targetRigidbody.velocity.y, targetRigidbody.velocity.z);
            }
                
            else if(pushLeft)
            {
                targetRigidbody.velocity = new Vector3(targetRigidbody.velocity.x + (pushForce * Time.deltaTime) , targetRigidbody.velocity.y, targetRigidbody.velocity.z);
            }
        }
            
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Donut Collided");

            if (!continousForce)
            {
                if(pushBack)
                    other.rigidbody.AddForce(Vector3.back * pushForce , ForceMode.Impulse);
                else if(pushLeft)
                    other.rigidbody.AddForce(Vector3.left * pushForce , ForceMode.Force);
                else if(pushLeft)
                    other.rigidbody.AddForce(Vector3.right * pushForce , ForceMode.Force);
            }
            else
            {
                targetRigidbody = other.rigidbody;
                collided = true;
            }
                
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision exit");
            collided = false;
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Opponent"))
        {

        }
        
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Opponent"))
        {
            collided = false;
        }
    }
}
