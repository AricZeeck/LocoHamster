using UnityEngine;
using System.Collections;

public class HitControl : MonoBehaviour {

	public Vector3 startPos;
	public GameObject target;
	private CamControl control;

	private float xOffset = 0.0f;
	private float yOffset = 0.0f;
	private float zOffset = 0.62f;
	private Vector3 temp;

	private CamControl camControl;
	// Use this for initialization
	void Start () {
		//Debug.Log ("hi");

		startPos = transform.position;
		target = GameObject.FindGameObjectWithTag ("Player");
		//control = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CamControl> ();
		camControl = GameObject.FindGameObjectWithTag ("CamController").GetComponent<CamControl> ();
		camControl.switchCamera = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.tag == "ballCollider") {
			startPos.x = target.transform.position.x + xOffset;
			startPos.y = target.transform.position.y + yOffset;
			startPos.z = target.transform.position.z + zOffset;
			transform.position = startPos;
				
		}


	}

	void OnCollisionEnter (Collision col)
	{

		if (col.gameObject.tag == "Player") {
			camControl.switchCamera = true;
				if (col.gameObject.tag == "ballCollider") {
								//temp.x += 10.0f;
								//temp.y = transform.position.y;
								//temp.y = transform.position.y;
								//transform.position = temp;
								//control.target = GameObject.FindGameObjectWithTag("golfBall").transform;
				}
		}

	}
}