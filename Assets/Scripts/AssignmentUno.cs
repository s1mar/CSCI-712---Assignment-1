using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssignmentUno : MonoBehaviour {

	[SerializeField] private string url = "https://drive.google.com/open?id=1mTEwFADqozcmoH47MQ6eS6sMoDYsEHJKlYJJUuW9xPo";
	
	private List<AnimationAttr> listOfInputPoints = new List<AnimationAttr>();
	private Dictionary<int,AnimationAttr> dict_objectPosWithRespectToTime = new Dictionary<int, AnimationAttr>(0);
	private int currentTimeInSecs = 0;

	private float timeElaspedBuffer = 0.0f;

	// Use this for initialization
	void Start () {
			//fetch data from the cloud or use the dummy data as a fall back
			StartCoroutine(getInputDoc());
			
	}
	
	// Update is called once per frame
	void Update () {
			/*
			updateTimer();
			executeAnimation(currentTimeInSecs);		
			*/



	}

	

	private void executeAnimation(int keyframe){
		
		if(dict_objectPosWithRespectToTime.ContainsKey(keyframe))
		{
			AnimationAttr attr = dict_objectPosWithRespectToTime[keyframe];
			if(attr!=null){
				Debug.Log(attr);
				transform.position = attr.position;
				transform.rotation = attr.quaternion;

		} else{
			Debug.Log("Corrupt attr at index : "+currentTimeInSecs);
		}
		
		}else{
			Debug.Log("Keyframe data not found at index : "+currentTimeInSecs);
		}


	}


/* 

	private void updateTimer(){
				float timeSinceLastFrame = Time.deltaTime;
				timeElaspedBuffer+=timeSinceLastFrame;
		
				//Keeps track of the second gone by
				if(timeElaspedBuffer>=1.0f){
						//if one second has elasped then reflect it on the game clock and reset this buffer
						currentTimeInSecs+=1;
						timeElaspedBuffer = 0.0f;
				}
	}
*/

#region  data_structures And Dummy data
	private IEnumerator getInputDoc(){

			//UnityWebRequest webRequest = new UnityWebRequest()
			initWithDummyData();
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
	
	listOfInputPoints = dummyData;
	
	//update the dictionary
	foreach(AnimationAttr attr in listOfInputPoints){
		dict_objectPosWithRespectToTime.Add((int)attr.time,attr);
	}
}


	public class AnimationAttr{

			public float time{get ; set;}
			public Vector3 position{get;set;}
			public Vector3 axis{get;set;}
			public float angle{get;set;}		

			public Quaternion quaternion{get;set;}

			public AnimationAttr(float time,Vector3 position,Vector3 axis,float angle){
						this.time = time;
						this.position = position;
						this.axis = axis;
						quaternion = new Quaternion(axis.x,axis.y,axis.z,angle);
			}

		    public AnimationAttr(float time, float pos_x,float pos_y,float pos_z, float xa,float ya, float za, float angle){
				this.time = time;
				this.position = new Vector3(pos_x,pos_y,pos_z);
				this.axis = new Vector3(xa,ya,za);
				this.angle = angle;
				this.quaternion = new Quaternion(xa,ya,za,angle);
			}

	}
	#endregion
}
