using UnityEngine;
using System.Collections;

public class Traj : MonoBehaviour {
	public Transform tr_myTarget;
	public GameObject go_golfBall;
	private float _fl_shootAngle = 30, _fl_vel;
	private Vector3 _v3_dir;

	private SelectManager _sm;
	private GameObject _go_sm, player;

	// Use this for initialization
	void Start () {
		_go_sm = GameObject.FindWithTag ("SelectManager");
		player = GameObject.FindWithTag ("Player");
		 _sm = _go_sm.GetComponent<SelectManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.B)) {
			GameObject golf_ball = Instantiate(go_golfBall, player.transform.position, Quaternion.identity) as GameObject;
			tr_myTarget = _sm.currently_selected_node.transform;
			golf_ball.rigidbody.velocity = BallisticVel(tr_myTarget, _fl_shootAngle);
		}
	}

	Vector3 BallisticVel(Transform target, float angle)
	{
		_v3_dir = target.position - transform.position;
		float h = _v3_dir.y;
		_v3_dir.y = 0;
		float dist = _v3_dir.magnitude;
		float a = angle * Mathf.Deg2Rad;
		_v3_dir.y = dist * Mathf.Tan (a);
		dist += h / Mathf.Tan (a);
		_fl_vel = Mathf.Sqrt (dist * Physics.gravity.magnitude / Mathf.Sin (2 * a));
		Debug.Log (_fl_vel * _v3_dir.normalized.magnitude);
		return _fl_vel * _v3_dir.normalized;
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Player") {
			tr_myTarget = _sm.currently_selected_node.transform;
			gameObject.rigidbody.velocity = BallisticVel(tr_myTarget, _fl_shootAngle);
		}


	}
}
