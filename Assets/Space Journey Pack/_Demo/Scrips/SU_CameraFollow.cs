using UnityEngine;
using System.Collections;

public class SU_CameraFollow : MonoBehaviour {
	
	
	public enum UpdateMode { FIXED_UPDATE, UPDATE, LATE_UPDATE }
	public UpdateMode updateMode = UpdateMode.FIXED_UPDATE;
	
	public enum FollowMode { CHASE, SPECTATOR }	
	public FollowMode followMode = FollowMode.SPECTATOR;
	
	public Transform target;
		
	public float distance = 60.0f;	

	public float chaseHeight = 15.0f;
	

	public float followDamping = 0.3f;

	public float lookAtDamping = 4.0f;	

	public KeyCode freezeKey = KeyCode.None;
		
	private Transform _cacheTransform;
	
	void Start () {
		_cacheTransform = transform;
	}
	
	void FixedUpdate () {
		if (updateMode == UpdateMode.FIXED_UPDATE) DoCamera();
	}	
	void Update () {
		if (updateMode == UpdateMode.UPDATE) DoCamera();
	}
	void LateUpdate () {
		if (updateMode == UpdateMode.LATE_UPDATE) DoCamera();
	}
	
	void DoCamera () {
		if (target == null) return;
		
		Quaternion _lookAt;
		
		switch (followMode) {
		case FollowMode.SPECTATOR:
			_lookAt = Quaternion.LookRotation(target.position - _cacheTransform.position);
			_cacheTransform.rotation = Quaternion.Lerp(_cacheTransform.rotation, _lookAt, Time.deltaTime * lookAtDamping);
			if (!Input.GetKey(freezeKey)) {
				if (Vector3.Distance(_cacheTransform.position, target.position) > distance) {					
					_cacheTransform.position = Vector3.Lerp(_cacheTransform.position, target.position, Time.deltaTime * followDamping);
				}
			}
			break;			
		case FollowMode.CHASE:			
			if (!Input.GetKey(freezeKey)) {	
				_lookAt = target.rotation;
				_cacheTransform.rotation = Quaternion.Lerp(_cacheTransform.rotation, _lookAt, Time.deltaTime * lookAtDamping);						
				_cacheTransform.position = Vector3.Lerp(_cacheTransform.position, target.position - target.forward * distance + target.up * chaseHeight, Time.deltaTime * followDamping * 10) ;
			}
			break;
		}						
	}	
}
