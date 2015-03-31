using UnityEngine;
using System.Collections;

public class PlayAnimationOnTriggerEnter : MonoBehaviour {
	
	public Animation[] objectsToAnimate;
	public string[] validTags;
	
	void OnTriggerEnter(Collider col)
	{
		foreach(string tag in validTags)
		{
			if(tag == col.tag)
			{
				foreach(Animation anim in objectsToAnimate)
				{
					anim.Play();
				}
			}
		}
	}
}
