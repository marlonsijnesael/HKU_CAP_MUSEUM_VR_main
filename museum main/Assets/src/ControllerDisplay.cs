using UnityEngine;
using UnityEngine.Events;

public class ControllerDisplay : MonoBehaviour
{                                               // basic class for oculus go controller interaction

	[Header("Setup")]
	public Camera cameraIs;                                 // camera to use for raycasting in editor
	public Transform controllerIs;                              // transform of the tracked controller
	public Transform rayIs;                                     // transform of some visualization ray
	public Transform cursorIs;                                  // transform of some " quad
	public MeshRenderer cursorDisplay;                              // for display of the cursor itself

	[Header("Click Parameters")]
	public LayerMask clickable;                                 // custom layermask for the raycasting

	[Header("Callbacks")]
	public UnityEvent onTriggerDown;                                // to fire on A button down
	public UnityEvent onTriggerUp;                                  // to fire on A button up
	public UnityEvent onTrackpadDown;                               // to fire on B button down
	public UnityEvent onTrackpadUp;                             // to fire on B button up

	[Header("Internal")]
	public Vector3 worldLooksAt;
	public Vector3 worldPointsAt;
	public SimpleButton lastHovering = null;

	void Update()
	{

#if (UNITY_EDITOR || UNITY_STANDALONE)                                                   // if in editor mode:
		bool buttonADown = Input.GetMouseButtonDown(0);                         // A down -> left mouse down
		bool buttonAUp = Input.GetMouseButtonUp(0);                         // A up   -> left mouse up
		bool buttonBDown = Input.GetMouseButtonUp(1);                           // B down -> right mouse down
		bool buttonBUp = Input.GetMouseButtonUp(1);                         // B up   -> right mouse up
#else                                                                                    // on device:
/*		bool buttonADown      = OVRInput.GetDown           (OVRInput.RawButton.A  );	// A down -> index finger button down
		bool buttonAUp        = OVRInput.GetUp             (OVRInput.RawButton.A  );	// A up   -> index finger button up
		bool buttonBDown      = OVRInput.GetDown           (OVRInput.Button   .One);	// B down -> trackpad button down
		bool buttonBUp        = OVRInput.GetUp             (OVRInput.Button   .One);	// B up   -> trackpad button up
		*/
#endif // UNITY_EDITOR

		if (buttonADown) onTriggerDown.Invoke();                                        // fire A down event
		if (buttonAUp) onTriggerUp.Invoke();                                        // fire A up event
		if (buttonBDown) onTrackpadDown.Invoke();                                       // fire B down event
		if (buttonBUp) onTrackpadUp.Invoke();                                       // fire B up event

		RaycastHit hitLook;                                                             // gaze hit structure
		Ray rayLook =                                                           // construct ray in gaze direction
			new Ray(cameraIs.transform.position, cameraIs.transform.forward);
		if (Physics.Raycast(rayLook, out hitLook, 100.0f))                              // now try to hit something with it
			worldLooksAt = hitLook.point;                                           // save our last world look position

/*#if (UNITY_EDITOR || UNITY_STANDALONE)                                                    // if in editor mode:
		Ray rayPointer = cameraIs.ScreenPointToRay(Input.mousePosition);            // use given camera to raycast into scene
#else*/                                                                                    // on device:
		Ray        rayPointer = new Ray(controllerIs.position, controllerIs.forward);	// use given tracked transform to raycast into scene
//#endif // UNITY_EDITOR

		RaycastHit hitPoint;                                                            // pointer hit structure
		bool hitPointAt =                                                           // POINT: did we hit something with the right mask?
			Physics.Raycast(rayPointer, out hitPoint, 100.0f, clickable);

		rayIs.position = rayPointer.origin;                                     // set the viz ray to the given ray origin
		rayIs.rotation = Quaternion.LookRotation(rayPointer.direction);         // set the viz ray to the given ray orientation
		rayIs.localScale =                                                          // scale the viz ray to the closest hit in the result
			new Vector3(1, 1, hitPointAt ? hitPoint.distance : 100.0f);

		SimpleButton b = null;                                                    // the purpose is, to find a button on something
		if (hitPointAt) {                                                               // did we hit something?
			worldPointsAt = hitPoint.point;                                         // save our last world pointer hit position
			cursorIs.position = hitPoint.point;                                         // position the cursor at the hit point
			cursorIs.rotation = Quaternion.LookRotation(rayPointer.direction);              // turn it into the general ray direction
			b =                                                         // try to get a button from the substructure
				hitPoint.transform.gameObject.GetComponentInChildren<SimpleButton>();
			if (b != null) {                                                            // if ref is valid
				if (lastHovering != b) {                                                // last hit button is not this button?
					if (lastHovering != null)                                           // last ref is valid?
						lastHovering.IsNoLongerSelected();                              // mark the ref as "no longer selected"
					lastHovering = b;                                                   // replace it by the new found button
				}

				b.Invoke(                                                               // invoke the callback with the right button status
					buttonADown,
					buttonAUp,
					buttonBDown,
					buttonBUp
				);
				b.IsSelected();                                                         // mark this button as "selected by hovering"
			}
		}
		
		if (!hitPointAt ||                                                              // so we didn't hit anything this frame?
			b == null) {                                                                // OR we hit something, but it contained no button
			if (lastHovering != null)                                                   // is there a button from the previous frame?
				lastHovering.IsNoLongerSelected();                                      // mark it as no longer "hovered over"
			lastHovering = null;                                                        // reset the last saved button ref for next frame
		}
		cursorDisplay.enabled = hitPointAt;                                             // only show the cursor, if we hit something
	}
}