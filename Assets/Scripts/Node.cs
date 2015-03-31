using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour 
{

	public List<GameObject> neighbors;
	public GameObject goal;
	private GameObject player, NGC_GO, original, hole;
	//Instances
	private CurrentNode _cN;
	private NodeGeneratorController NGC;
	//private CompanionMachine _companionMachine;

	private Transform me;
	
	//Bools
	private bool _nodeListIsFull = false;
	public bool cantGoUp, cantGoDown, cantGoRight, cantGoLeft, isSelected = false;



	void Start()
	{
		neighbors = new List<GameObject>();
		NGC_GO = GameObject.FindGameObjectWithTag ("NodeGenerator");
		NGC = NGC_GO.GetComponent<NodeGeneratorController> ();
		original = NGC._original;
		me = gameObject.transform.FindChild ("Selected");
		me.gameObject.SetActive(false);
		hole = GameObject.FindGameObjectWithTag ("Hole");
		player = GameObject.FindGameObjectWithTag ("Player");

	}

	void Update()
	{
		if (this.isSelected) {
						me.gameObject.SetActive (true);
				} else {
						me.gameObject.SetActive (false);
		}

		//goal = hole;
	}

	IEnumerator DrawNeighbors()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(5, 1 ,5));
		foreach(GameObject neighor in neighbors)
		{
			Gizmos.DrawLine (transform.position, neighor.transform.position);
			Gizmos.DrawWireSphere(neighor.transform.position, 0.25f);
		}
		
		/*if(goal)
		{
			Gizmos.color = Color.green;
			GameObject current = gameObject;
			
			Stack<GameObject> path = DijkstraAlgorithm.Dijkstra(GameObject.FindGameObjectsWithTag("Node"), player.GetComponent<CurrentNode>().currentNode, goal.GetComponent<CurrentNode>().currentNode);
			
			foreach(GameObject obj in path)
			{
				//Gizmos.DrawWireSphere(obj.transform.position, 1.0f);
				
				Gizmos.DrawLine(current.transform.position, obj.transform.position);
				current = obj;
			}
			yield return 0;
		}*/
		yield return 0;
	}

	void OnDrawGizmos() //editor function, if gizmos is turned on
	{
		StartCoroutine(DrawNeighbors());
	}

	void FixedUpdate()
	{
		player = GameObject.FindWithTag ("Player");

	}
	
}
