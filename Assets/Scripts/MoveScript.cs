using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	public const string up = "w";
	public const string down = "d";
	public const string left = "a";
	public const string right = "d";
	private float rotateSpeed = 3.0f;
	private float speed = 30.0f;

	private GameObject pos;
	// Use this for initialization
	void Awake () {


	}
	
	// Update is called once per frame
	void Update () {

		CharacterController controller = GetComponent<CharacterController> ();
		transform.Rotate (0, Input.GetAxis ("Horizontal") * rotateSpeed, 0);
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		float curSpeed = speed * Input.GetAxis ("Vertical");
		controller.SimpleMove (forward * curSpeed);
	}
}
