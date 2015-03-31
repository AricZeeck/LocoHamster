#pragma strict
var myTarget : Transform;
var cannonBall : GameObject;
var shootAngle: float = 30;
var vel : float;
var dir : Vector3;

function BallisticVel(target: Transform, angle: float): Vector3 {
	dir = target.position - transform.position; // get target direction
	var h = dir.y; // get height difference
	dir.y = 0; // retain only the horizontal direction
	var dist = dir.magnitude ; // get horizontal distance
	var a = angle * Mathf.Deg2Rad; // convert angle to radians
	dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle
	dist += h / Mathf.Tan(a); // correct for small height differences
	// calculate the velocity magnitude
	vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
	//Debug.Log(vel * dir.normalized);
	return vel * dir.normalized;
}

function Update(){
	if (Input.GetKeyDown("b")){ // press b to shoot
		var ball: GameObject = Instantiate(cannonBall, transform.position, Quaternion.identity);
		ball.rigidbody.velocity = BallisticVel(myTarget, shootAngle);
	}
	
	//Debug.Log((vel * dir.normalized).magnitude);
}

function Start()
{

}