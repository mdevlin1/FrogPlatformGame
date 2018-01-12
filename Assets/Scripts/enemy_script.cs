using UnityEngine;
using System.Collections;

public class enemy_script : MonoBehaviour {

	public Transform target;
	private Vector3 start;
	private bool targetReached; 

	// Use this for initialization
	void Start () {
		start = transform.position;
		targetReached = false;
	}
	
	// Update is called once per frame
	void Update () {
		float step = .5f * Time.deltaTime;

		if (Vector3.Distance(transform.position, target.position) < 0.1f) {
			targetReached = true;
		}
		
		if (Vector3.Distance(transform.position, start) < 0.1f) {
			targetReached = false;
		}

		if (targetReached) {
			transform.position = Vector3.MoveTowards(transform.position, start, step);
		}
		else {
			transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		}
			
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "asteroid") {
			Destroy(other);
			Destroy(gameObject);
		}
	}
}
