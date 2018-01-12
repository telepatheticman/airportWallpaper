using UnityEngine;
using System.Collections;

public class LandingAndTakeoff : MonoBehaviour {

	GameObject plane;

	float landVel;
	float takeOffVel;
	float taxVel;

	float LaTR;

	float Acc;
	float Dec;

	public LandingAndTakeoff(float lV, float tkV, float txV, float R, float A, float D, ref GameObject obj){
		landVel = lV;
		takeOffVel = tkV;
		taxVel = txV;
		LaTR = R;
		Acc = A;
		Dec = D;
		plane = obj;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Land(){
		plane.transform.eulerAngles = new Vector3 (360-LaTR, 0, 0);
		plane.transform.Translate (0, 
			-Time.deltaTime * landVel * Mathf.Sin(LaTR * (Mathf.PI/180)), 
			Time.deltaTime * landVel * Mathf.Cos(LaTR * (Mathf.PI/180)), 
			Space.World);
	}
}
