﻿using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool useTorque = true;
    public float maxAngularVelocity = 25;
    public float jumpPower = 2;
    public const float groundRayLength = 1f;
    private Rigidbody thisRigidbody;

    private Transform mainCamera;
    private Vector3 mainCameraForward;
    private bool jump;
    private Vector3 move;
    private Transform defaultParant;

    public GameObject messageText;
    //public Text coinsText;
    private int coinTotal;
    private int coinCount;
    public bool lastLevel;

	//public float newZCoordinate, newXCoordinate;
	public bool inAir = false;

	//instances
	private CheckpointManager chkMngr;


    private void Start()
    {
        Screen.lockCursor = true;
        Screen.showCursor = false;

        mainCamera = Camera.main.transform;
        thisRigidbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().maxAngularVelocity = maxAngularVelocity;

        coinTotal = GameObject.FindGameObjectsWithTag("Coin").Length;
        //coinsText.text = "Coins: " + coinCount.ToString() + "/" + coinTotal.ToString();
        defaultParant = transform.parent;
		chkMngr = GameObject.FindGameObjectWithTag("checkpointManager")
			.GetComponent<CheckpointManager>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");

        if (mainCamera != null)
        {
            mainCameraForward = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
            move = (v * mainCameraForward + h * mainCamera.right).normalized;
        }
        else
        {
            move = (v * Vector3.forward + h * Vector3.right).normalized;
        }

        if (transform.position.y < -5)
        {
			ResetPosToLastCheckpoint();
			inAir = false;
        }

		if(inAir)
		{
			//Debug.Log ("IN AIR");

			if(Input.GetKeyDown(KeyCode.W))
			{
				//float newZCoordinate = .02f;
				//transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x, transform.position.y, newZCoordinate),Time.fixedDeltaTime * 20);
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				//float newZCoordinate = -.02f;
				//transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x, transform.position.y, newZCoordinate),Time.fixedDeltaTime * 20);
			}
				
			if(Input.GetKey(KeyCode.A))
			{
				this.rigidbody.AddForce(new Vector3(0,0,.5f));
			}
				
			if(Input.GetKey(KeyCode.D))
			{
				this.rigidbody.AddForce(new Vector3(0,0,-.5f));
			}
				

		}

        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
           Application.LoadLevel("Menu");
        }*/
    }

    private void FixedUpdate()
    {

        if (useTorque)
        {
            thisRigidbody.AddTorque(new Vector3(move.z, 0, -move.x) * moveSpeed);
        }
        else
        {
            thisRigidbody.AddForce(move * moveSpeed);
        }

        if (Physics.Raycast(transform.position, -Vector3.up, groundRayLength) && jump)
        {
           	thisRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

		//thruster logic
		/*if (Input.GetKeyDown (KeyCode.T)) {
			rigidbody.AddForce(transform.forward * 2000f);
		}*/
    }

    void OnTriggerEnter(Collider _hit)
    {
        if (_hit.tag == "Coin")
        {
            Destroy(_hit.gameObject);
            coinCount++;
            //coinsText.text = "Coins: " + coinCount.ToString() + "/" + coinTotal.ToString();
        }
        else if (_hit.tag == "Next")
        {
            if (coinCount == coinTotal)
            {
                if (lastLevel)
                {
                    Application.LoadLevel("Menu");
                }
                else
                {
                    Application.LoadLevel(Application.loadedLevel + 1);
                }
            }
            else
            {
                StartCoroutine(ShowMessage("You need to find all the coins"));
            }
        }
        else if (_hit.tag == "Platform")
        {
			Debug.Log("Allo m8");
            transform.parent = _hit.transform;
        }
    }
    void OnTriggerExit(Collider _hit)
    {
        if (_hit.tag == "Platform")
        {
            transform.parent = defaultParant;
        }
    }

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Platform")
		{
			inAir = false;
		}
	}

    IEnumerator ShowMessage(string _msg)
    {
        //messageText.SetActive(true);
        //messageText.transform.FindChild("Text").GetComponent<Text>().text = _msg;

        yield return new WaitForSeconds(2);
       // messageText.SetActive(false);
    }

	void ResetPosToLastCheckpoint()
	{
		//Grab last checkpoint location
		//Change position to last checkpoint position
		if(chkMngr.cache_check_points.Count == 0)
		{
			Application.LoadLevel(0);
			return;
		} else {
			Vector3 grabbedCheckpointPosition = chkMngr.cache_check_points[chkMngr.cache_check_points.Count - 1];
			Debug.Log (grabbedCheckpointPosition);
			transform.position = grabbedCheckpointPosition;
		}

	}
}