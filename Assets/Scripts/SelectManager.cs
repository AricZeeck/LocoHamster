using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectManager : MonoBehaviour {
		

	RaycastHit hit;
	Ray ray;

	private NodeGeneratorController NGC;
	private Node Node, tempNodeScript, _previousNode;
	private GameObject NGC_GO, original, player, hole, _previousSelectedNode;
	public List<GameObject> selectionCache = new List<GameObject> ();
	public GameObject currently_selected_node, execute_selected_node;

	private bool blnGrabbedOriginal = false;
	private bool nodeGenComplete = false;
	public bool isSelecting = false;
	private Stack<GameObject> _path = new Stack<GameObject> ();
	private Traj TrajScript;
	private LineScript LineScript;
	private GameObject Traj, LR;

	// Use this for initialization
	void Start () {
		ray = new Ray ();
		NGC_GO = GameObject.FindGameObjectWithTag ("NodeGenerator");
		NGC = NGC_GO.GetComponent<NodeGeneratorController> ();
		//original = NGC._original;
		player = GameObject.FindGameObjectWithTag ("Player");
		hole = GameObject.FindGameObjectWithTag ("Hole");
		_path = DijkstraAlgorithm.Dijkstra (GameObject.FindGameObjectsWithTag ("Node"), 
		                                                      player.GetComponent<CurrentNode> ().currentNode, 
		                                                      hole.GetComponent<CurrentNode> ().currentNode);

		Traj = GameObject.FindWithTag ("Traj");
		TrajScript = Traj.GetComponent<Traj> ();
	}

	void SelectNode(Node node, GameObject go)
	{
		currently_selected_node = go;
		TrajScript.tr_myTarget = currently_selected_node.transform;
		if (_previousSelectedNode != null) {
			_previousNode = _previousSelectedNode.GetComponent<Node>();
			_previousNode.isSelected = false;
		}
		Node = go.GetComponent<Node>();
		Node.isSelected = true;
		_previousSelectedNode = go;
	}

	IEnumerator FindNextNode()
	{
		//Stack<GameObject> _tempPath = new Stack<GameObject> ();
		//_tempPath = _path;
		if (_path == null) {
			UpdatePath ();
		}
		//Debug.Log ("temp path pop: " +  _tempPath.Pop ());
		SelectNode(Node, _path.Pop ());
		//selectionCache.Add (_path.Pop ());

		yield return 0;
	}

	void FixedUpdate()
	{

	}

	void UpdatePath()
	{
		_path = DijkstraAlgorithm.Dijkstra (GameObject.FindGameObjectsWithTag ("Node"), 
		                                    player.GetComponent<CurrentNode> ().currentNode, 
		                                    hole.GetComponent<CurrentNode> ().currentNode);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("NodeGenerator") != null)
				original = NGC._original;

		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			isSelecting = true;
			if(!blnGrabbedOriginal)
			{
				//Debug.Log ("Here");
				blnGrabbedOriginal = true;
				SelectNode(Node, original);
				//Debug.Log ("And here");
			}
			else {
				if (_path == null) {
					UpdatePath ();
				}
				StartCoroutine(FindNextNode());
			}
		}

		if(Input.GetKeyDown (KeyCode.KeypadEnter))
		{
			execute_selected_node = currently_selected_node;
			isSelecting = false;
			TrajScript.tr_myTarget = execute_selected_node.transform;
		}
	
	}

}
