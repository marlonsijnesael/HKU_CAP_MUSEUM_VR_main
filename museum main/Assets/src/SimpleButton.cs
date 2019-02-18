using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

public class SimpleButton : MonoBehaviour
{                   // simple class for clickable interaction constructs

	[Header("Setup")]
	public bool isBlocked = false;              // blocks the button and makes it ignore clicks during this time

	[Header("Main Button")]
	public UnityEvent onDownA;                          // event to fire on button down -> A key
	public UnityEvent onUpA;                            // event to fire on button up   -> A key

	[Header("Secondary Button")]
	public UnityEvent onDownB;                          // event to fire on button down -> B key	
	public UnityEvent onUpB;                            // event to fire on button up   -> B key

	[Header("Selection")]
	public UnityEvent onHovering;
	public UnityEvent onOut;

	public void DownA() { Invoke(true, false, false, false); }
	public void UpA() { Invoke(false, true, false, false); }
	public void DownB() { Invoke(false, false, true, false); }
	public void UpB() { Invoke(false, false, false, true); }

	public void Unblock() { isBlocked = false; }        // unblock interaction
	public void Block() { isBlocked = true; }       // block interaction
	public void Invoke(                             // invoke a test for event firing with a set of given buttons
		bool isDownA,                                   // A down
		bool isUpA,                                     // A up
		bool isDownB,                                   // B down
		bool isUpB                                      // B up
	)
	{
		if (isBlocked)                                  // button blocked for interaction?
			return;                                     // exit

		if (isDownA) onDownA.Invoke();                  // fire down A
		if (isUpA) onUpA.Invoke();                  // fire up A
		if (isDownB) onDownB.Invoke();                  // fire down B
		if (isUpB) onUpB.Invoke();                  // fire up B
	}

	public void IsSelected() { onHovering.Invoke(); }
	public void IsNoLongerSelected() { onOut.Invoke(); }
}

#if UNITY_EDITOR
[CanEditMultipleObjects]                                // allow for multiple object selection in hierarchy
[CustomEditor(typeof(SimpleButton))]                          // custom editor for this class
public class ButtonEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();                         // draw default inspector

		SimpleButton b = target as SimpleButton;                    // get target as instance
		if (b == null)                                  // no valid ref?
			return;                                     // exit

		GUILayout.Space(10);                            // add a bit of space
		GUILayout.Label("DEBUG");                       // draw a label

		if (GUILayout.Button("onDown()")) b.DownA();    // fire explicitly down A
		else if (GUILayout.Button("onUp()")) b.UpA();   // " up A
		else if (GUILayout.Button("onDownB()")) b.DownB();  // " down B
		else if (GUILayout.Button("onUpB()")) b.UpB();  // " up B
	}
}
#endif // UNITY_EDITOR