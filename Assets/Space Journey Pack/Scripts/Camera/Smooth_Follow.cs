using UnityEngine;
using System.Collections;

public class Smooth_Follow : MonoBehaviour {
	public Transform target;
	public float distance = 10.0f;
	public float height = 5.0f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

	// Update is called once per frame
	void LateUpdate () {
		if(target==null)
			return;
		var wantedRotationAngle = target.rotation.eulerAngles.y;
		var wantedHeight = target.transform.position.y + height;
			
		var currentRotationAngle = transform.rotation.eulerAngles.y;
		var currentHeight = transform.position.y;
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
		
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);
		transform.LookAt (target);
	}
}
