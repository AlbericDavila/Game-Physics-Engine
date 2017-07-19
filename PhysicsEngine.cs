using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour 
{
	public Vector3 velocityVector;	// [m s^-1]
	public Vector3 netForceVector;	// N [kg m s^-2]
	public float mass;				// [kg]

	private List<Vector3> forceVectorList = new List<Vector3>();

	// Use this for initialization
	void Start () 
	{
		setUpThrustTrails ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		renderTrails ();
		updateVelocity ();
	}

	public void addForce(Vector3 forceVector)
	{
		forceVectorList.Add (forceVector);
	}
		
	void updateVelocity()
	{
		netForceVector = Vector3.zero;
		foreach (Vector3 forceVector in forceVectorList)
			netForceVector += forceVector;
		
		// Clear the forces
		forceVectorList = new List<Vector3>();

		// Calculate acceleraton
		Vector3 accelerationVector = netForceVector / mass;

		// Update velocity
		velocityVector += accelerationVector * Time.deltaTime;

		// Update position
		transform.position += velocityVector * Time.deltaTime;
	}



	/// <summary>
	/// Code for generating thrust trails.
	/// </summary>
	public bool showTrails = true;
	private LineRenderer lineRenderer;
	private int numberOfForces;

	void setUpThrustTrails () 
	{
		forceVectorList = GetComponent<PhysicsEngine>().forceVectorList;

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		lineRenderer.SetColors(Color.yellow, Color.yellow);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.useWorldSpace = false;
	}

	// Update is called once per frame
	void renderTrails () {
		if (showTrails) {
			lineRenderer.enabled = true;
			numberOfForces = forceVectorList.Count;
			lineRenderer.SetVertexCount(numberOfForces * 2);
			int i = 0;
			foreach (Vector3 forceVector in forceVectorList) {
				lineRenderer.SetPosition(i, Vector3.zero);
				lineRenderer.SetPosition(i+1, -forceVector);
				i = i + 2;
			}
		} else {
			lineRenderer.enabled = false;
		}
	}
}

