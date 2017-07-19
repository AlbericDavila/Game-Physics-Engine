using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour 
{
	private PhysicsEngine[] physicsEngineArray;
	private const float bigG = 6.673e-11f;	// [m^3 s^-2 kg-1]

	// Use this for initialization
	void Start ()
	{
		physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine> ();

	}

	void FixedUpdate()
	{
		calculateGravity ();
	}
	

	/// <summary>
	/// Calculating the force of gravity that causes objects to attract each other
	/// 
	/// F = G * m1*m2/(r^2)
	/// 
	/// r = distance between both objects
	/// m1 = mass of first object
	/// m2 = mass of second object
	/// G = gravitational constant (bigG)
	/// </summary>
	private void calculateGravity()
	{
		// Get interaction of an physics object with all physics other objects
		foreach (PhysicsEngine physicsEngineA in physicsEngineArray) 
		{
			foreach (PhysicsEngine physicsEngineB in physicsEngineArray)
			{
				if (physicsEngineA != physicsEngineB && physicsEngineA != this) 
				{
					Vector3 offset = physicsEngineA.transform.position - physicsEngineB.transform.position;
					float rSquared = Mathf.Pow (offset.magnitude, 2f);
					float gravityMagnitud = bigG * physicsEngineA.mass * physicsEngineB.mass / rSquared;
					Vector3 gravityFeltVector = gravityMagnitud * offset.normalized;

					physicsEngineA.addForce (-gravityFeltVector);
				}
			}
		}
	}
}
