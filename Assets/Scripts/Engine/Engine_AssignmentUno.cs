
using System.Collections.Generic;
using UnityEngine;

public class Engine_AssignmentUno : MonoBehaviour {

	private Queue<AnimationAttr> animationStatesQueue;
	private int keyframeDelta = 1; //keyframe delta for now is one second; it'll execute each element of the queue after every one second

	private Transform subject; // this will be subjected to the animations in the queue

	private float secondTimer = 0;
	private int timeKeeper = 0;

	private bool startTheClock;

	private bool startNextAnimation;

	private Lerper lerperManager;
	private Slerper slerperManager;

	private GameObject gameObjectToActivate;

	public void Initialize(List<AnimationAttr> animations,Transform subject,GameObject toActivate){
		animationStatesQueue = new Queue<AnimationAttr>(animations);
		this.subject = subject;
		this.gameObjectToActivate = toActivate;
		
		lerperManager = gameObject.AddComponent<Lerper>();
		lerperManager.Initialize(subject);
		slerperManager = gameObject.AddComponent<Slerper>();
		slerperManager.Initialize(subject);
		
	}

	public void startProcessing(){

		//Check if queue is in a valid state and has elements
		if(animationStatesQueue==null || animationStatesQueue.Count==0){
			Debug.Log("Animation queue is empty at the start of the processing");
			return;
		}	


		//Check if the element first in queue is at zero, if it is , then we'll simply warp our transform there
		AnimationAttr topMostElement = animationStatesQueue.Peek();
		if(topMostElement.time==0){
				subject.position = topMostElement.position;
				animationStatesQueue.Dequeue(); //removes the topmost element
		}

		gameObjectToActivate.SetActive(true); //enable the subject if it's disabled, so as to run the animations on it
		startNextAnimation= true;
		startTheClock = true;
		
	}

	private void Update() {
		if(startTheClock){
			Debug.Log("Sequence Initiated");
			if(startNextAnimation){

						startNextAnimation = false;
						//check if we have something in the queue and then pass onto the lerp and slerp mechanisms
						if(animationStatesQueue.Count==0){
								startTheClock = false;
								return;
						}	
						AnimationAttr animation = animationStatesQueue.Dequeue();
						if(animation!=null){
									//You lerp and slerp
									lerperManager.Enqueue(animation);
									slerperManager.Enqueue(animation);
						}

			}

			secondTimer+=Time.deltaTime;
			if(secondTimer>=1){
				//A second has elasped, record it in the log and get ready for the next animation
				timeKeeper+=1;
				secondTimer =0;
				Debug.Log("A second has elasped, now we\'re at second: "+timeKeeper);
				startNextAnimation = true;
			}
		
		}
	}

class Lerper : MonoBehaviour{

 Queue<AnimationAttr> elements = new Queue<AnimationAttr>(0);
 AnimationAttr animationUnderProcess;


//Attrs for the animation under progress
Vector3 initialPos;
Vector3 targetPos;
float animationStartTime;
float distanceBetweenTheTwoPoints;





float movementSpeed = 1.0f;
Transform subject;

bool animationPending;

bool initalized;

float animationStepTime;
public void Initialize(Transform subject){
	this.subject = subject;
	this.initalized = true; 

}
 public void Enqueue(AnimationAttr animationAttr){
		 elements.Enqueue(animationAttr);
}

private void Update() {

	if(!initalized){
			return;
	}

	if(animationUnderProcess!=null){
		if(elements.Count>0){
			//Finish the animation under process and execute the next animation
			animationPending = true;
		}

			//Keep on progressing the current animation
			translate();
		

	}else{
			//there is no animation to process
			if(elements.Count>0){
						animationUnderProcess = elements.Dequeue();
						animationStartTime = Time.time;

						initialPos = targetPos==Vector3.zero?subject.position:targetPos;
						
						targetPos = animationUnderProcess.position;
						
						//distanceBetweenTheTwoPoints = Vector3.Distance(initialPos,targetPos);
						
						//movementSpeed = distanceBetweenTheTwoPoints/1; // The movement speed to cover the distance in one second
						//Debug.Log("Movement Speed Calculated: "+movementSpeed);
						animationStepTime = 0.0f;
						translate();
			}

	}
}

void translate(){
	
        //float distanceCovered = (Time.time - animationStartTime) * movementSpeed;

        //float percJourney = distanceCovered / distanceBetweenTheTwoPoints;
		animationStepTime +=Time.deltaTime;
		if(animationStepTime>1){
			animationStepTime = 1;
		}
		
		float percJourney = calculateSmoothStep(animationStepTime);

	
		if(animationPending)
        {
			//percJourney =1;
		}
		
		subject.position = Vector3.Lerp(initialPos, targetPos, percJourney);

		if(subject.position == targetPos){
			//end this animation
			animationUnderProcess = null;
		}
}

}


class Slerper : MonoBehaviour{
	Queue<AnimationAttr> elements = new Queue<AnimationAttr>(0);
	AnimationAttr animationUnderProcess;	

	Transform subject;

	bool animationPending;
	
	//Move specific attrs
	Quaternion initalRotation;
	Quaternion targetRotation = Quaternion.identity;
	float animationStepTime;
	bool initalized;
	public void Initialize(Transform subject){
		this.subject = subject;
		this.initalized = true;
	}
	public void Enqueue(AnimationAttr animationAttr){
		 elements.Enqueue(animationAttr);
	}

	private void Update() {
		if(!initalized){
			return;
		}
		if(animationUnderProcess!=null){
			if(elements.Count>0){
				//Finish the animation under process and execute the next animation
				animationPending = true;
			}
			
				//Keep on progressing the current animation
				rotate();
			

		}else{
				//there is no animation to process
				if(elements.Count>0){
							animationUnderProcess = elements.Dequeue();
							initalRotation = targetRotation==Quaternion.identity?subject.rotation:targetRotation;
							targetRotation = (animationUnderProcess.quaternion);
							animationStepTime = 0; //resetting the animation step time
							rotate();
				}

		}
	}

	void rotate(){
		
			animationStepTime+=Time.deltaTime;
			if(animationStepTime>=1 || animationPending){
				animationStepTime = 1;
			}
			
			subject.rotation = Quaternion.Slerp(initalRotation, targetRotation, calculateSmoothStep(animationStepTime));


			if(subject.rotation == targetRotation){
				//end this animation
				animationUnderProcess = null;
			}
		
	}

}

private static float calculateSmoothStep(float currentStep){
			return currentStep*currentStep*(3.0f - 2.0f*currentStep);
			
}

/* 
private static Quaternion Quaternion_ConvertToUnityHandedSystem(Quaternion input) {
    return new Quaternion(
         input.y,   // -(  right = -left  )
        -input.z,   // -(     up =  up     )
        -input.x,   // -(forward =  forward)
         input.w
    );
}
*/
}
