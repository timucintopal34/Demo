using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardMoveSpeed = 300;
    public float sideMoveSpeed = 10;

	GameObject character;
    Animator _animator;

	private Rigidbody characterBody;
	private float ScreenWidth;

    
    bool gameStart = false;

	void Start () 
    {

		ScreenWidth = Screen.width;
        character = gameObject;
        _animator = transform.Find("ModelHandler").GetChild(0).GetComponent<Animator>();

		characterBody = character.GetComponent<Rigidbody>();

        //Lines are invoked by Instance which tells game is started
        GameManager.Instance.OnLevelStart += ()=>{
            MoveAnimation();
            gameStart = true;
        };

        GameManager.Instance.OnLevelEnd += ()=>{
            gameStart = false;
        };
	}
	
	// Update is called once per frame
	void Update () 
    {
		int i = 0;

        if(gameStart)
        {
            if(characterBody.velocity.z < 5.0f )
            {
                //Player is pushed forward while its velocity is under a value
                characterBody.AddForce(Vector3.forward * forwardMoveSpeed ,ForceMode.Acceleration);
            }
                
            //loop over every touch found - for touch input
            while (i < Input.touchCount) {
                if (Input.GetTouch (i).position.x > ScreenWidth / 2) {
                    //move right
                    RunCharacter (1.0f);
                }
                if (Input.GetTouch (i).position.x < ScreenWidth / 2) {
                    //move left
                    RunCharacter (-1.0f);
                }
                ++i;
            }
            //Game Fail regarding height - if it fell then y is -
            if(transform.position.y <= -3f)
            {
                Debug.Log("Game Fail");
                gameStart = false;
                FallAnimation();
                //GameManager.Instance.GameFail();
            }
        }
	}

    //For Keyboard input left - right
	void FixedUpdate()
    {
		#if UNITY_EDITOR
		RunCharacter(Input.GetAxis("Horizontal"));
		#endif
	}

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "GameEndTrigger")
        {
            Debug.Log("GameWin");
            GameWin();
        }
    }

	private void RunCharacter(float horizontalInput){
		//Moves player left or right due to input
		characterBody.AddForce(new Vector3(horizontalInput * sideMoveSpeed * Time.deltaTime , 0));

	}
    
    void GameWin()
    {
        gameStart = false;
        GameManager.Instance.GameWin();
        DanceAnimation();
    }

    void MoveAnimation()
    {
        _animator.SetTrigger("Run");
    }

    void FallAnimation()
    {
        _animator.SetTrigger("Fall");
        StartCoroutine(CameraRelease(1.0f));
    }

    void DanceAnimation()
    {
        _animator.SetTrigger("Dance");
    }

    public void CryAnimation()
    {
        _animator.SetTrigger("Cry");
    }

    IEnumerator CameraRelease(float time)
    {
        yield return new WaitForSeconds(time);
        Camera.main.transform.SetParent(null);
        yield return new WaitForSeconds(time);
        transform.position = Vector3.zero;
    }
    
}
