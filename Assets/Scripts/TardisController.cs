using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TardisController : MonoBehaviour {

[SerializeField] private string url = "https://drive.google.com/open?id=1mTEwFADqozcmoH47MQ6eS6sMoDYsEHJKlYJJUuW9xPo";
	//It should always be sorted, basically each entry increasing in time
	List<AnimationAttr> list_AnimationAttrs = new List<AnimationAttr>(); 
	
	private Vector3 startPos;
	private Vector3 endPos;

	private Quaternion targetRotation;
	private float lerpFactor = 1.0f;

	private float currentLerpTime = 0.0f; //means the lerp hasn't started yet	

	private bool isProcessingSet = false;

	private bool successfullyFetchedInput = false;
	void Start() {			
			StartCoroutine(InitFromInputDoc());

	}

	
	bool processFinished = false;
	private void animateLerpPosV2(){

			if(!isProcessingSet && list_AnimationAttrs.Count==0){
				//it's not lerping and the count is zero, then return
				processFinished = true;	
				return;
			}

			if(!isProcessingSet){
					AnimationAttr topOfStack = list_AnimationAttrs[0];
					list_AnimationAttrs.RemoveAt(0);
					startPos = transform.position;
					endPos = topOfStack.position;
					targetRotation = topOfStack.quaternion;
					currentLerpTime = 0;
					isProcessingSet = true;
				}

				if(endPos!=transform.position){

					currentLerpTime+= Time.deltaTime;
					if(currentLerpTime>lerpFactor){
						currentLerpTime = lerpFactor;
					}	

					float travelPerc = (currentLerpTime/lerpFactor);	
					
					transform.position = Vector3.Lerp(startPos,endPos,calculateSmoothStep(travelPerc));
					targetRotation.Normalize();
					transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,travelPerc);
					
					Debug.Log(Time.time);
				}else
				{
					isProcessingSet = false;
				}

	}


	private float calculateSmoothStep(float currentStep){
			return currentStep*currentStep*(3.0f - 2.0f*currentStep);

	}
	


	void Update(){
			if(successfullyFetchedInput && !processFinished)
				animateLerpPosV2();
	}


	
	private IEnumerator InitFromInputDoc(){

			//UnityWebRequest webRequest = new UnityWebRequest()
			initWithDummyData();
			successfullyFetchedInput = true;


			yield break;

	}

	

private void initWithDummyData(){
	List<AnimationAttr> dummyData = new List<AnimationAttr>();
	dummyData.Add(new AnimationAttr(0.0f,0.0f,0.0f,0.0f,1.0f,1.0f,-1.0f,0.0f));
	dummyData.Add(new AnimationAttr(1.0f,  4.0f, 0.0f, 0.0f, 1.0f, 1.0f, -1.0f, 30.0f));
	dummyData.Add(new AnimationAttr(2.0f,8.0f, 0.0f, 0.0f, 1.0f, 1.0f, -1.0f, 90.0f));
	dummyData.Add(new AnimationAttr(3.0f,12.0f, 12.0f, 12.0f, 1.0f, 1.0f, -1.0f, 180.0f));
	dummyData.Add(new AnimationAttr(4.0f,  12.0f, 18.0f, 18.0f, 1.0f, 1.0f, -1.0f, 270.0f));
	dummyData.Add(new AnimationAttr(5.0f,  18.0f, 18.0f, 18.0f, 0.0f, 1.0f, 0.0f, 90.0f));
	dummyData.Add(new AnimationAttr(6.0f,  18.0f, 18.0f, 18.0f, 0.0f, 0.0f, 1.0f, 90.0f));
	dummyData.Add(new AnimationAttr(7.0f,  25.0f, 12.0f, 12.0f, 1.0f, 0.0f, 0.0f, 0.0f));
	dummyData.Add(new AnimationAttr(8.0f,  25.0f, 0.0f, 18.0f, 1.0f, 0.0f, 0.0f, 0.0f));
	dummyData.Add(new AnimationAttr(9.0f, 25.0f, 1.0f, 18.0f, 1.0f, 0.0f, 0.0f, 0.0f));
	
	list_AnimationAttrs = dummyData;
	
}
	
}


