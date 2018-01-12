using UnityEngine;
using System.Collections;

public class Airplane : MonoBehaviour {
	//Readability vars
	public GameObject plane;

	public float landingSpeed;
	public float takeOffSpeed;
	public float taxiSpeed;

	public float Acceleration;
	public float Deceleration;

	public float LaTRotation;
	public float TaxRotation;

	public bool landing;
	public bool takeOff;
	public bool taxiingToo;
	public bool taxiingFrom;
	public bool landed;

	public string term;

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
		LaT = new LandingAndTakeoff (landingSpeed, takeOffSpeed, taxiSpeed, LaTRotation, Acceleration, Deceleration, ref plane);
		taxi = new Taxi(taxiSpeed, TaxRotation, ref plane);
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
