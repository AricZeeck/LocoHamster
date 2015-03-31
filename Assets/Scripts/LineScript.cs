using UnityEngine;
using System.Collections;

public class LineScript : MonoBehaviour {

	private LineRenderer lineRenderer;
	private float counter, dist;

	public Transform origin, destination;

	public float lineDrawSpeed;
	private GameObject _go_sm;
	private SelectManager _sm;
	// Use this for initialization
	void Start () {
		_go_sm = GameObject.FindWithTag ("SelectManager");
		_sm = _go_sm.GetComponent<SelectManager> ();
		
		//dist = Vector3.Distance (origin.position, destination.position);
	}
	
	// Update is called once per frame
	void Update () {




		if (counter < dist) {
			counter += .1f / lineDrawSpeed;

			float x = Mathf.Lerp (0, dist, counter);

			Vector3 pointA = origin.position;
			Vector3 pointB = destination.position;

			Vector3 pontAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

			lineRenderer.SetPosition(1, pontAlongLine);
		}

		if (_sm.isSelecting) {
			lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.SetPosition (0, origin.position);
			lineRenderer.SetWidth (.45f, .45f);
			destination = _sm.currently_selected_node.transform;
			dist = Vector3.Distance (origin.position, destination.position);
		}
	
	}
}
