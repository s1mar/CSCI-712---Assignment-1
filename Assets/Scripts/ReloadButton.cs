
using UnityEngine;

public class ReloadButton : MonoBehaviour {

	FrameAssignmentZero_Alpha frameAssignmentZero_Alpha;
	// Use this for initialization
	void Start () {
		frameAssignmentZero_Alpha = FindObjectOfType<FrameAssignmentZero_Alpha>();
	}
	

	public void onClick(){

		frameAssignmentZero_Alpha.Start();
		gameObject.SetActive(false);
	}

	// Update is called once per frame

}
