using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeCloneController : MonoBehaviour {

	//Data Structures
	public Stack<GameObject> temp_NodeList;
	public Stack<GameObject> perm_NodeList;
	//private List<GameObject> arrayOfNodes;
	private GameObject[] arrayOfNodes;

	//Instances
	private NodeGeneratorController NGC;
	private GameObject NGC_GO, _previous, _current;
	private Node node, thisNode, otherNode;

	//Raycasts
	private RaycastHit hit;
	
	//Bools
	public bool begin = true, upDone = false, downDone = false, rightDone = false;


	//Floats
	public float offset;

	//Ints
	public int nodeCount;

	// Use this for initialization
	void Start () {
		temp_NodeList = new Stack<GameObject>();
		NGC_GO = GameObject.FindGameObjectWithTag("NodeGenerator");
		NGC = NGC_GO.GetComponent<NodeGeneratorController>();
		temp_NodeList.Push(NGC._original);
		_current = temp_NodeList.Pop ();
		temp_NodeList.Push (_current);
		nodeCount++;
		//thisNode = gameObject.GetComponent<Node> ();


		//StartCoroutine(RayCastLeft());

		StartCoroutine(RayCastForward());
		//StartCoroutine(RayCastDown());
		//StartCoroutine(RayCastRight());
		//StartCoroutine(CheckAllNodes());
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{


	}

	public bool checkIfPosEmpty(Vector3 targetPos)
	{
		//float offset = 1.0f;
		GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
		
		foreach(GameObject node in nodes)
		{
			if(node.transform.position == targetPos)
				return false;
			
		}
		return true;
	}

	public bool checkIfPosEmpty(Vector3 targetPos, Vector3 dir)
	{
		//float offset = 1.0f;
		GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
		Ray ray = new Ray(_current.transform.position, dir);

		foreach(GameObject node in nodes)
		{
			if(node.transform.position == targetPos)
				return false;
			
		}

		if(Physics.Raycast (ray, out hit, offset))
		{
			return false;	
		}
		return true;
	}
	
	public bool checkIfPosEmpty(Vector3 targetPos, GameObject current)
	{
		GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
		thisNode = current.GetComponent<Node>();

		foreach(GameObject node in nodes)
		{
			otherNode = node.GetComponent<Node>();
			if(node.transform.position == targetPos)
			{
				thisNode.neighbors.Add (node);
				otherNode.neighbors.Add (current);
				return false;
			}



		}
		return true;
	}

	public bool checkIfPosEmpty(Vector3 targetPos, GameObject current, Vector3 dir)
	{
		GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
		thisNode = current.GetComponent<Node>();

		foreach(GameObject node in nodes)
		{
			Ray ray = new Ray(_current.transform.position, dir);
			otherNode = node.GetComponent<Node>();
			if(node.transform.position == targetPos)
			{
				thisNode.neighbors.Add (node);
				otherNode.neighbors.Add (current);
				return false;
			}

			if(Physics.Raycast (ray, out hit, offset))
			{
				if(hit.collider.tag == "Wall")
					return false;
			}

		}
		return true;
	}

	IEnumerator RayCastForward()
	{

		//float distance = 200.0f;
		Ray ray = new Ray(_current.transform.position, Vector3.forward);
		//if (!thisNode.cantGoUp) {
			if(!Physics.Raycast (ray, out hit, offset))
			{
				Debug.DrawRay(_current.transform.position, Vector3.forward * offset, Color.green);
				Vector3 nextPos = new Vector3(_current.transform.position.x, _current.transform.position.y, _current.transform.position.z  + offset);
				Quaternion nextRot = _current.transform.rotation;
				
				if(checkIfPosEmpty(nextPos, Vector3.forward))
				{
					nodeCount++;
					Object node;
					node = Instantiate(_current, nextPos, nextRot);
					//parent = GameObject.Find("NodeGenerator");
					node.name = "Node " + nodeCount;
				}
				
				arrayOfNodes = GameObject.FindGameObjectsWithTag("Node");
				_previous = _current;
				
				foreach(GameObject obj in arrayOfNodes)
				{
					temp_NodeList.Push (obj);
					_current = temp_NodeList.Pop ();
					temp_NodeList.Push (_current);
				}
				
				yield return 0;
				
			} else {
				
				if(hit.collider.tag != "Wall")
				{
					Vector3 nextPos = new Vector3(_current.transform.position.x, _current.transform.position.y, _current.transform.position.z + offset);
					Quaternion nextRot = _current.transform.rotation;
					
					if(checkIfPosEmpty(nextPos, Vector3.forward))
					{
						nodeCount++;
						Object node;
						node = Instantiate(_current, nextPos, nextRot);
						//node.parent = GameObject.Find("NodeGenerator");
						node.name = "Node " + nodeCount;
					}
					
					arrayOfNodes = GameObject.FindGameObjectsWithTag("Node");
					_previous = _current;
					
					foreach(GameObject obj in arrayOfNodes)
					{
						temp_NodeList.Push (obj);
						_current = temp_NodeList.Pop ();
						temp_NodeList.Push (_current);
					}
				}
				_current = NGC._original;
				begin = false;
				upDone = true;
				yield return 0;
			}

		StartCoroutine(RayCastDown());
		//}
	}

	IEnumerator RayCastDown()
	{
		begin = false;

		Ray ray = new Ray(_current.transform.position, -Vector3.forward);
		//if(!thisNode.cantGoDown)
		//{
			if(!Physics.Raycast (ray, out hit, offset))
			{
				Debug.DrawRay(_current.transform.position, -Vector3.forward * offset, Color.yellow);
				Vector3 nextPos = new Vector3(_current.transform.position.x, _current.transform.position.y, _current.transform.position.z - offset);
				Quaternion nextRot = _current.transform.rotation;
				
				if(checkIfPosEmpty(nextPos, -Vector3.forward))
				{
					nodeCount++;
					Object node;
					node = Instantiate(_current, nextPos, nextRot);
					node.name = "Node " + nodeCount;
				}
				
				arrayOfNodes = GameObject.FindGameObjectsWithTag("Node");
				_previous = _current;
				
				foreach(GameObject obj in arrayOfNodes)
				{
					temp_NodeList.Push (obj);
					_current = temp_NodeList.Pop ();
					temp_NodeList.Push (_current);
				}
				
				yield return 0;
				
				
			} else {
				downDone = true;
				yield return 0;
			}

		StartCoroutine(RayCastRight());
		//}
	}

	IEnumerator RayCastRight()
	{

		Ray ray = new Ray(_current.transform.position, Vector3.right);
		//if(!thisNode.cantGoRight)
		//{
			if(!Physics.Raycast (ray, out hit, offset))
			{
				Debug.DrawRay(_current.transform.position, Vector3.right * offset, Color.red);
				Vector3 nextPos = new Vector3(_current.transform.position.x + offset, _current.transform.position.y, _current.transform.position.z);
				Quaternion nextRot = _current.transform.rotation;
				
				if(checkIfPosEmpty(nextPos, Vector3.right))
				{
					nodeCount++;
					Object node;
					node = Instantiate(_current, nextPos, nextRot);
					node.name = "Node " + nodeCount;
				}
				
				arrayOfNodes = GameObject.FindGameObjectsWithTag("Node");
				_previous = _current;
				
				foreach(GameObject obj in arrayOfNodes)
				{
					temp_NodeList.Push (obj);
					_current = temp_NodeList.Pop ();
					temp_NodeList.Push (_current);	
				}
				yield return 0;
				
			} else {
				rightDone = true;
				yield return 0;
			}

		StartCoroutine(CheckAllNodes());
		//}
	}

	IEnumerator RayCastLeft()
	{

		Ray ray = new Ray(_current.transform.position, -Vector3.right);
		//if (!thisNode.cantGoLeft) {
			if(!Physics.Raycast (ray, out hit, offset))
			{
				Debug.DrawRay(_current.transform.position, -Vector3.right * offset, Color.blue);
				Vector3 nextPos = new Vector3(_current.transform.position.x - offset, _current.transform.position.y, _current.transform.position.z);
				Quaternion nextRot = _current.transform.rotation;
				
				if(checkIfPosEmpty(nextPos, -Vector3.right))
				{
					nodeCount++;
					Object node;
					node = Instantiate(_current, nextPos, nextRot);
					//node.parent = GameObject.Find("NodeGenerator");
					node.name = "Node " + nodeCount;
				}
				
				arrayOfNodes = GameObject.FindGameObjectsWithTag("Node");
				_previous = _current;
				
				foreach(GameObject obj in arrayOfNodes)
				{
					temp_NodeList.Push (obj);
					_current = temp_NodeList.Pop ();
					temp_NodeList.Push (_current);	
				}
				yield return 0;
				
			} else {
				
				yield return 0;
			}
		StartCoroutine(CheckAllNodes());
		//}
	}


	IEnumerator CheckAllNodes()
	{
		arrayOfNodes = GameObject.FindGameObjectsWithTag("Node");
		node = gameObject.GetComponent<Node>();
		_current = gameObject;



		if(!node.cantGoUp || !node.cantGoDown || !node.cantGoRight)
		{
			//if(!node.cantGoUp)
			//{
			Vector3 nextPos = new Vector3(_current.transform.position.x, _current.transform.position.y, _current.transform.position.z  + offset);
			if(checkIfPosEmpty(nextPos, gameObject, Vector3.forward))
			{
				StartCoroutine(RayCastForward());
			}
			else {
				node.cantGoUp = true;
				yield return 0;
			}
			//}
			
			//if(!node.cantGoDown)
			//{
			Vector3 nextPos2 = new Vector3(_current.transform.position.x, _current.transform.position.y, _current.transform.position.z  - offset);
			if(checkIfPosEmpty(nextPos2, gameObject, -Vector3.forward))
			{
				StartCoroutine (RayCastDown());
			}
			else {
				node.cantGoDown = true;
				yield return 0;
			}
			//}
			
			
			
			if(!node.cantGoRight)
			{
				Vector3 nextPos3 = new Vector3(_current.transform.position.x + offset, _current.transform.position.y, _current.transform.position.z);
				if(checkIfPosEmpty(nextPos3, gameObject, Vector3.right))
				{
					StartCoroutine(RayCastRight ());
				}
				else {
					node.cantGoRight = true;
					yield return 0;
				}
			}

		}
		else {
			
			yield return 0;
		}

		if (node.cantGoUp && node.cantGoDown && node.cantGoRight) {
			NGC.numOfDoneNodes++;
		}


	}
	
}
