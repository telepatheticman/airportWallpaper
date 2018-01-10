using UnityEngine;
using System.Collections;

public class landing : MonoBehaviour {
	public GameObject plane;
	public bool isInAir;
	public bool takeOff;
	public bool taxiingToo;
	public bool taxiingFrom;
	public bool landed;
	bool turningLeft = false;
	bool turningRight = false;
	bool turningBackRight = false;
	bool turningBackLeft = false;
	bool reverse = true;
	float current;
	float target;
	public float deceleration;
	public float acceleration;
	public float landingRotation;
	public float takeoffRotation;
	public float velocity;
	public float angle;
	public string term;

	void inAir(){
		//float zComp = velocity/((1/(Mathf.Cos((2*angle)*(Mathf.PI/180))))*(180/Mathf.PI));
		//float yComp = velocity/((1/(Mathf.Sin((2*angle)*(Mathf.PI/180))))*(180/Mathf.PI));

		plane.transform.eulerAngles = new Vector3 (360-angle, 0, 0);
		plane.transform.Translate (0, -Time.deltaTime * velocity * Mathf.Sin(angle * (Mathf.PI/180)) , Time.deltaTime * velocity * Mathf.Cos(angle * (Mathf.PI/180)), Space.World);
	}

	void onStrip(){
		
		
		bool rotated = false;
		if (plane.transform.position.y < .31) {
			plane.transform.Translate (0f, .05f, 0f, Space.World);
		}
		if (velocity > 10) {
			velocity -= deceleration;
		} else {
			landed = false;
			taxiingToo = true;
		}
		//float zComp = velocity/((1/(Mathf.Cos((angle)*(Mathf.PI/180))))*(180/Mathf.PI));
		//float yComp = velocity/((1/(Mathf.Sin((angle)*(Mathf.PI/180))))*(180/Mathf.PI));
		//plane.transform.Translate (0, -Time.deltaTime * yComp, Time.deltaTime * zComp);
		plane.transform.Translate (0, 0, Time.deltaTime * velocity, Space.World);
		//Debug.Log (plane.transform.eulerAngles.x);
		if (!(plane.transform.eulerAngles.x < .1) && !(plane.transform.eulerAngles.x > 359.9)) {
			plane.transform.Rotate (Time.deltaTime * landingRotation, 0, 0);
		} else {
			rotated = true;
		}

		if (rotated) {
			plane.transform.eulerAngles = new Vector3 (0, 0, 0);
		}
	}

	void takeingOff(){
		if (velocity < 70) {
			velocity += acceleration;
		}

		if (velocity >= 70) {
			if (!(plane.transform.eulerAngles.x > 339 && plane.transform.eulerAngles.x < 341)) { 
				plane.transform.Rotate (-Time.deltaTime * takeoffRotation, 0f, 0f);
			}
		}

		plane.transform.Translate (0, 0, Time.deltaTime * velocity);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Runway" && isInAir) {
			//Debug.Log("Found");
			isInAir = false;
			landed = true;
		}

		if (other.gameObject.tag == "turnLeftForward" && (taxiingToo || taxiingFrom)) {
			current = plane.transform.eulerAngles.y;
			target = current - 90.0f;
			if (target <= 0) {
				target += 360;
			}
			if (target >= 360) {
				target -= 360;
			}
			if (current == 0) {
				current = 360;
			}
			turningLeft = true;
			Debug.Log("Turn");
		}

		if (other.gameObject.tag == term && taxiingToo) {
			current = plane.transform.eulerAngles.y;
			target = current + 90.0f;
			if (target <= 0) {
				target += 360;
			}
			if (target >= 360) {
				target -= 360;
			}
			if (current == 0) {
				current = 360;
			}
			turningRight = true;
			Debug.Log("Turn");
		}
		if (other.gameObject.tag == "turnBackLeft" && taxiingFrom) {
			current = plane.transform.eulerAngles.y;
			target = current + 90.0f;
			if (target <= 0) {
				target += 360;
			}
			if (target >= 360) {
				target -= 360;
			}
			if (current == 0) {
				current = 360;
			}
			turningBackLeft = true;
			Debug.Log("Turn");
		}

		if (other.gameObject.tag == "park" && taxiingToo) {
			taxiingToo = false;
		}

		if (other.gameObject.tag == "revDir" && taxiingFrom) {
			reverse = false;
		}

		if (other.gameObject.tag == "takeOff" && taxiingFrom) {
			taxiingFrom = false;
			takeOff = true;
		}

	}

	void turnFor(bool left){
		float turnSpeed = 15.0f;
		bool isTurned = false;
		Debug.Log ("Target: " + target + " Current: " + current);
		//Debug.Log (!(current > target + 1 && current < target - 1));
		if (!(current < target + 1 && current > target - 1)) {
			if (left) {
				//Debug.Log("Got inside");
				float turn = Time.deltaTime * turnSpeed;
				plane.transform.Rotate (0, -turn, 0);
				current -= turn;
			} else {
				float turn = Time.deltaTime * turnSpeed;
				plane.transform.Rotate (0, turn, 0);
				current += turn;;
			}
		} else {
			isTurned = true;
		}

		if (isTurned) {
			plane.transform.eulerAngles = new Vector3 (0, target, 0);
			turningLeft = false;
			turningRight = false;
			current = target;
		}
	}

	void turnBack(bool left){
		float turnSpeed = 15.0f;
		bool isTurned = false;
		Debug.Log ("Target: " + target + " Current: " + current);
		if (target == 0) {
			target += 180;
		}
		//Debug.Log (!(current > target + 1 && current < target - 1));
		if (!(current < target + 1 && current > target - 1)) {
			if (left) {
				//Debug.Log("Got inside");
				float turn = Time.deltaTime * turnSpeed;
				plane.transform.Rotate (0, -turn, 0);
				current -= turn;
			} else {
				float turn = Time.deltaTime * turnSpeed;
				plane.transform.Rotate (0, turn, 0);
				current += turn;;
			}
		} else {
			isTurned = true;
		}

		if (isTurned) {
			plane.transform.eulerAngles = new Vector3 (0, target, 0);
			turningBackLeft = false;
			turningBackRight = false;
			current = target;
		}
	}

	void taxiToo(){
		plane.transform.Translate (0, 0, Time.deltaTime * velocity);
	}

	void taxiFrom(){
		if (reverse) {
			plane.transform.Translate (0, 0, -Time.deltaTime * velocity);
		} else {
			plane.transform.Translate (0, 0, Time.deltaTime * velocity);
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isInAir) {
			inAir ();
		} else if (landed) {
			//velocity -= velocity/((1/(Mathf.Cos((2*angle)*(Mathf.PI/180))))*(180/Mathf.PI));
			onStrip ();
		} else if (takeOff) {
			takeingOff ();
		} else if (taxiingToo) {
			taxiToo ();
		} else if (taxiingFrom) {
			taxiFrom ();
		}

		if (turningLeft) {
			turnFor (true);
		}
		if (turningRight) {
			turnFor (false);
		}
		if (turningBackLeft) {
			turnBack (true);
		}
	}
}
