using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {
	public CheckpointManager chkManager;
	// Use this for initialization
	void Start () {
		chkManager = GameObject.FindGameObjectWithTag("checkpointManager")
			.GetComponent<CheckpointManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			chkManager.cache_check_points.Add(this.transform.position);
			Destroy (gameObject);
		}
	}
}
