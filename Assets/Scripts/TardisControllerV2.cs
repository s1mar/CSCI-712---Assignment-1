
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TardisControllerV2 : MonoBehaviour {
	[SerializeField] private string url = "https://drive.google.com/open?id=1mTEwFADqozcmoH47MQ6eS6sMoDYsEHJKlYJJUuW9xPo";
	//It should always be sorted, basically each entry increasing in time
	List<AnimationAttr> list_AnimationAttrs = new List<AnimationAttr>();
	
	private int tDStep =1;

	void Start () {
		StartCoroutine(InitFromInputDoc());
	}
	

		private IEnumerator InitFromInputDoc(){

			//UnityWebRequest webRequest = new UnityWebRequest()
			initWithDummyData();

			AnimationAttr firstAnimationAttr = list_AnimationAttrs[0];
			if(firstAnimationAttr.time==0.0f)
			{
				transform.position = firstAnimationAttr.position;
				transform.rotation = firstAnimationAttr.quaternion;
				list_AnimationAttrs.RemoveAt(0);
				
			}

			StartCoroutine(processRoutines());
			yield break;
	}

	private IEnumerator processRoutines(){
		while(list_AnimationAttrs.Count>0){
				AnimationAttr currentAniAttr= list_AnimationAttrs[0];
				list_AnimationAttrs.RemoveAt(0);
				
				float sTime = Time.time;
				float currentStep = Time.time - sTime;
				while(currentStep <=tDStep){
					float perc_step = currentStep*currentStep*(3.0f - 2.0f*currentStep);
					transform.position = Vector3.Lerp(transform.position,currentAniAttr.position,perc_step); 
				}
	
		}
			yield return null;
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
