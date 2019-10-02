
using UnityEngine;

public class FrameAssignmentZero_Alpha : MonoBehaviour {

	public ReloadButton mReloadButton;
	[SerializeField] private int timerDuration = 20; //where 20seconds is the default value
	private float xTransDelta,yTransDelta; //no Z translation delta for there is no movement on the z-axis for this exercise
	[SerializeField] private int deltaStepForTranslation = 5;
	[SerializeField] private int deltaStepForRotation = 18; 			
	private float yRotDelta; // there is no x and z vars for there will be no rotation in those for this exercise

	private Vector3 translationDeltaVector;		

	private float timeElaspedBuffer;		


	private AudioSource audioSouceControl;	
	// Use this for initialization
	public void Start () {
		if(timerDuration<=0){
			timerDuration = 20;
			timeElaspedBuffer = 0;
		} 
		audioSouceControl = GetComponent<AudioSource>();
		audioSouceControl.Play();

		//Bottom Edge View 
		var tardisPlacementPos = new Vector3(Screen.width/4,50,70);
		tardisPlacementPos = Camera.main.ScreenToWorldPoint(tardisPlacementPos);
		transform.position = tardisPlacementPos;
		transform.rotation.Normalize();
	
	}

	
	// Update is called once per frame
	void Update () {
			if(timerDuration>0){

				float timeSinceLastFrame = Time.deltaTime;
				timeElaspedBuffer+=timeSinceLastFrame;
		
				//Keeps track of the second gone by
				if(timeElaspedBuffer>=1.0f){
						//if one second has elasped then reflect it on the game clock and reset this buffer
						timerDuration--;
						timeElaspedBuffer = 0.0f;
				}

				if(timerDuration>=0){
					updateObjectsPositionAndRotation();
				}
				

		}
		else{
					audioSouceControl.Stop();
					mReloadButton.gameObject.SetActive(true);
				}
	}
	

	void calculateDelta(){
		//move the object to the bottom left corner of the screen
		xTransDelta = deltaStepForTranslation*timerDuration;
		yTransDelta = deltaStepForTranslation*timerDuration;
		yRotDelta = deltaStepForRotation*timerDuration;
		translationDeltaVector = new Vector3(xTransDelta,yTransDelta,0);
	}

	
	private void updateObjectsPositionAndRotation(){
	
			calculateDelta();
			Vector3 updatedPosInVector = transform.position+translationDeltaVector/1000;
        	Quaternion quaternionRotationAboutYAxis = Quaternion.AngleAxis(yRotDelta, Vector3.up);
			quaternionRotationAboutYAxis.Normalize();

			transform.position = updatedPosInVector;
			//Using Slerp to interpolate between the positions smoothly
			transform.rotation = Quaternion.Slerp(transform.rotation, quaternionRotationAboutYAxis,Time.fixedDeltaTime);
	
	}


}
