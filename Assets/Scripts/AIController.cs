using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	public float sensorForward = 10.0f;
	public float sensorLength = 5.0f;
	public float speed = 5.00f;
	float directionValue = 1.0f;
	float turnValue = 0.0f;
	public float turnSpeed = 50.0f;

	Collider myCollider;
	// Use this for initialization
	void Start () {
		myCollider = transform.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		int flag = 0;

		//Si xoquem contra un objecte frontal (sensor davanter)
		if (Physics.Raycast (transform.position, Quaternion.AngleAxis(-35, transform.up) * transform.forward, out hit, (sensorForward + transform.localScale.z)) || Physics.Raycast (transform.position, Quaternion.AngleAxis(35, transform.up) * transform.forward, out hit, (sensorForward + transform.localScale.z))) {
			if (hit.collider.tag != "Obstacle" || hit.collider == myCollider) {
				return;
			}

			//Si no detecta el sensor esquerra
			if (!Physics.Raycast (transform.position, -transform.right, out hit, (sensorLength + transform.localScale.x))) {
				Debug.Log ("ESQUERA");
				turnValue -= 1; //Esquerra
				flag++;
			}

			//Si no detecta el sensor dret
			if (!Physics.Raycast (transform.position, transform.right, out hit, (sensorLength + transform.localScale.x))) {
				Debug.Log ("DRETA");
				turnValue += 1; //Dreta
				flag++;
			}
		}

		if (flag == 0) {
			turnValue = 0;
		}
		transform.Rotate (Vector3.up * (turnSpeed * turnValue) * Time.deltaTime);    
		transform.position += transform.forward * (speed*directionValue) * Time.deltaTime;
	}

	void OnDrawGizmos(){
		//Sensors fronta
		Gizmos.DrawRay(transform.position, transform.InverseTransformDirection(new Vector3(-5,0,10)));
		Gizmos.DrawRay(transform.position, transform.InverseTransformDirection(new Vector3(5,0,10)));
		//Sensor posterior
		Gizmos.DrawRay(transform.position, -transform.forward * (sensorLength + transform.localScale.z));
		//Sensor dret
		Gizmos.DrawRay(transform.position, transform.right * (sensorLength + transform.localScale.x));
		//Sensor esquerra
		Gizmos.DrawRay(transform.position, -transform.right * (sensorLength + transform.localScale.x));

	}
}

//		//Si xoquem contra algo pel sensor dret
//		if (Physics.Raycast (transform.position, transform.right, out hit, (sensorLength + transform.localScale.x))) {
//			if (hit.collider.tag != "Obstacle" || hit.collider == myCollider) {
//				return;
//			}
//			turnValue -= 1; //Esquerra 
//			flag++;
//		}
//
//		//Si xoquem contra algo pel sensor esquerra
//		if (Physics.Raycast (transform.position, -transform.right, out hit, (sensorLength + transform.localScale.x))) {
//			if (hit.collider.tag != "Obstacle" || hit.collider == myCollider) {
//				return;
//			}
//
//			turnValue += 1; //Dreta
//			flag++;
//		}
//
//		//Si xoquem contra algo pel sensor davanter
//		if (Physics.Raycast (transform.position, transform.forward, out hit, (sensorLength + transform.localScale.z))) {
//			if (hit.collider.tag != "Obstacle" || hit.collider == myCollider) {
//				return;
//			}
//
//			//Si anem cap endavant directionValue = 1
//			if (directionValue == 1.0f) {
//				directionValue = -1;
//			}
//			flag++;
//		}
//
//		//Si xoquem contra algo pel sensor posterior
//		if (Physics.Raycast (transform.position, -transform.forward, out hit, (sensorLength + transform.localScale.z))) {
//			if (hit.collider.tag != "Obstacle" || hit.collider == myCollider) {
//				return;
//			}
//			if (directionValue == -1.0f) {
//				directionValue = 1;
//			}
//			flag++;
//		}