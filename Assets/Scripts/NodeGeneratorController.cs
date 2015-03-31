using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeGeneratorController : MonoBehaviour {


	public GameObject _original, clone;
	public int numOfDoneNodes;
	public bool endSearch = false;
	// Use this for initialization
	void Start () {
		numOfDoneNodes = 0;
		Instantiate(clone, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
		_original = GameObject.FindGameObjectWithTag("Node");

	
	}
	
	// Update is called once per frame
	void Update () 
	{


	
	}

	void FixedUpdate()
	{

		if(endSearch)
		{
			//companion_M.nodeGenDone = true;
			//endSearch = true;
			//Debug.Log("Done");
		}
		/*GameObject[] numOfNodes = GameObject.FindGameObjectsWithTag("Node");
		
		if(numOfDoneNodes >= numOfNodes.Length)
		{
			if(!endSearch)
			{
				//companion_M.nodeGenDone = true;
				//endSearch = true;
				Debug.Log("Done");
			}
		}*/
	}


}
