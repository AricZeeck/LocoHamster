using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour
{
    public float power;
    public Vector3 dir;
	private Ball ball;
	

    void OnTriggerEnter(Collider _hit)
    {
        if (_hit.tag == "Player")
        {
            _hit.rigidbody.velocity = Vector3.zero;
            _hit.rigidbody.AddForce(dir * power * 100);
			ball = GameObject.FindGameObjectWithTag("Player")
				.GetComponent<Ball>();
			ball.inAir = true;
        }
    }
}
