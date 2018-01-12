using UnityEngine;
using System.Collections;

public class Taxi : MonoBehaviour {

	GameObject plane;

	float velocity;
	float rotation;

	bool turningLeft = false;
	bool turningRight = false;
	bool turningBackRight = false;
	bool turningBackLeft = false;
	bool reverse = true;
	float current;
	float target;

	public Taxi(float vel, float rot, ref GameObject obj){
		velocity = vel;
		rotation = rotation;
		plane = obj;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
