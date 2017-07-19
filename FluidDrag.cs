using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidDrag : MonoBehaviour 
{
	[Range (1, 2f)]
	public float velocityExponent;		// [none]
	public float dragConstant;	
	// ?
	private PhysicsEngine physicsEngine;

	// Use this for initialization
	void Start () 
	{
		physicsEngine = GetComponent<PhysicsEngine>();
	}

	void FixedUpdate () 
	{
		Vector3 velocityVector = physicsEngine.velocityVector;
		float speed = velocityVector.magnitude;
		float dragSize = calculateDrag (speed);
		Vector3 dragVector = dragSize * -velocityVector.normalized;

		physicsEngine.addForce (dragVector);
	}
	
	float calculateDrag(float speed)
	{
		return dragConstant * Mathf.Pow(speed, velocityExponent);

	}
}

