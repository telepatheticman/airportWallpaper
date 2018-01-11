using UnityEngine;
using System.Collections;

public class Airplane : MonoBehaviour {
	//Readability vars
	public float landingSpeed;
	public float takeOffSpeed;
	public float taxiSpeed;

	public float Acceleration;
	public float Deceleration;

	public float LaTRotation;
	public float TaxRotation;

	//internal vars
	private float lanS;
	private float takS;
	private float taxS;

	private float Acc;
	private float Dec;

	private float LaTR;
	private float TaxR;

	LandingAndTakeoff LaT;
	Taxi taxi;

	// Use this for initialization
	void Start () {
		LaT = new LandingAndTakeoff ();
		taxi = new Taxi();
		lanS = landingSpeed;
		takS = takeOffSpeed;
		taxS = taxiSpeed;
		Acc = Acceleration;
		Dec = Deceleration;
		LaTR = LaTRotation;
		TaxR = TaxRotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
