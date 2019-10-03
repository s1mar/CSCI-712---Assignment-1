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

	
	void Start() {			
			StartCoroutine(InitFromInputDoc());
	}

	
	bool processFinished = false;
	private void animateAccordingToFrameData(){

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
					transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,calculateSmoothStep(travelPerc));
					
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
			if(!processFinished)
				animateAccordingToFrameData();
	}


	
	private IEnumerator InitFromInputDoc(){
			getKeyFramesFromTextAsset();
			yield break;
	}

	

private void getKeyFramesFromTextAsset(){


	TextAsset keyframesText = Resources.Load("keyframes") as TextAsset;
	string[] keyFramesByLines = keyframesText.text.Split("\r\n".ToCharArray(),System.StringSplitOptions.RemoveEmptyEntries);
	foreach (string key in keyFramesByLines)
	{
			List<float> elementdetails =new List<float>(); //t,x,y,z,xa,ya,za,angle;
			string[] sub_entries = key.Split(new char[]{',',' '},System.StringSplitOptions.RemoveEmptyEntries);
			foreach (string item in sub_entries)
			{
				elementdetails.Add(float.Parse(item.Trim()));
			}

			AnimationAttr animationAttr = new AnimationAttr(elementdetails[0],elementdetails[1],elementdetails[2],elementdetails[3],elementdetails[4],elementdetails[5],elementdetails[6],elementdetails[7]);
			Debug.Log(animationAttr);
			list_AnimationAttrs.Add(animationAttr);
	}


}
	
}

