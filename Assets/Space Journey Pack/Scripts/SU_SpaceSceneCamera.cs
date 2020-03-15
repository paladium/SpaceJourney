

using UnityEngine;
using System.Collections;

public class SU_SpaceSceneCamera : MonoBehaviour {

	public Camera parentCamera;

	public bool inheritFOV = true;
	
	public float relativeSpeed = 0.0f;
	
	private Vector3 _originalPosition;
	private Transform _transformCache;
	private Transform _transformCacheParentCamera;

	
	void Start () {

		_transformCache = transform;
		
		if (parentCamera == null) {

			if (Camera.main != null) {
				// Set parent camera to main camera.
				parentCamera = Camera.main;							
			} else {				
				// No main camera found - throw a fit...
				Debug.LogWarning ("You have not specified a parent camera to the space background camera and there is no main camera in your scene. " +
								  "The space scene will not rotate properly unless you set the parentCamera in this script.");
			}
		}
		
		if (parentCamera != null) {
			_transformCacheParentCamera = parentCamera.transform;							
		}
		
		_originalPosition = _transformCache.position;
	}
	
	void Update () {		
		if (_transformCacheParentCamera != null) {		
			_transformCache.rotation = _transformCacheParentCamera.rotation;
			if (inheritFOV) GetComponent<Camera>().fov = parentCamera.fov;
			_transformCache.position = _originalPosition + (_transformCacheParentCamera.position * relativeSpeed);
		}
	}
}
