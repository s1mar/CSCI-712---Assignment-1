using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
This is the foundation class that'll determine the framework moving forward 
*/
public class FrameAssignmentZero_Beta : MonoBehaviour {

	[SerializeField] private int timerDuration = 20; //where 20seconds is the default value
	private float xTransDelta,yTransDelta; //no Z translation delta for there is no movement on the z-axis for this exercise
	[SerializeField] private int deltaStepForTranslation = 5;
	[SerializeField] private int deltaStepForRotation = 18; 		
	private float yRotDelta; // there is no x and z vars for there will be no rotation in those for this exercise

	private Vector3 translationDeltaVector;		

	private float timeElaspedBuffer;


	// Initialization
	private void Start() {
		var tardisPlacementPos = new Vector3(100,100,70);
		tardisPlacementPos = Camera.main.ScreenToWorldPoint(tardisPlacementPos);
		transform.position = tardisPlacementPos;	
	}	

	void determineDeltaTransform(float delta){

		//move the object to the bottom left corner of the screen

		xTransDelta = deltaStepForTranslation*delta;
		yTransDelta = deltaStepForTranslation*delta;
		yRotDelta = deltaStepForRotation*timerDuration;

		translationDeltaVector = new Vector3(xTransDelta,yTransDelta,0);
	}
	
	private void FixedUpdate() {
			if(timerDuration>0){

				float timeSinceLastFrame = Time.deltaTime;
				timeElaspedBuffer+=timeSinceLastFrame;
		
			//Keeps track of a second gone by
				if(timeElaspedBuffer>=1.0f){
						//if one second has elasped then reflect it on the game clock and reset this buffer
						timerDuration--;
						timeElaspedBuffer = 0.0f;
				}

			//if their is time left then do the animation
				if(timerDuration>0){
						determineDeltaTransform(timeSinceLastFrame);
						updateObjectsPositionAndRotation();
				}

		}
	}


	private void updateObjectsPositionAndRotation(){
	
			Vector3 updatedPosInVector = transform.position+translationDeltaVector;
        	Quaternion quaternionRotationAboutYAxis = Quaternion.AngleAxis(yRotDelta, Vector3.up);
			transform.SetPositionAndRotation(updatedPosInVector,quaternionRotationAboutYAxis);
	}
}
