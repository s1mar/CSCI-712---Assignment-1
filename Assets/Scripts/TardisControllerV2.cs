using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TardisControllerV2 : MonoBehaviour {

[SerializeField] private string url = "https://drive.google.com/open?id=1mTEwFADqozcmoH47MQ6eS6sMoDYsEHJKlYJJUuW9xPo";
	//It should always be sorted, basically each entry increasing in time
	List<AnimationAttr> list_AnimationAttrs = new List<AnimationAttr>(); 
	private bool successfullyFetchedInput;

	[SerializeField] private GameObject tardisShip;
	private Engine_AssignmentUno animationEngine;


	private void Start() {
			StartCoroutine(InitFromInputDoc());
	}

	private void Awake() {
		tardisShip.SetActive(false);
	}
	
	private IEnumerator InitFromInputDoc(){
			//UnityWebRequest webRequest = new UnityWebRequest()
			initWithDummyData();
			successfullyFetchedInput = true;
			//Now that we have the data, let's start the engine
			animationEngine = gameObject.AddComponent<Engine_AssignmentUno>();
			animationEngine.Initialize(list_AnimationAttrs,transform,tardisShip); //initialized the engine	
			animationEngine.startProcessing();
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


