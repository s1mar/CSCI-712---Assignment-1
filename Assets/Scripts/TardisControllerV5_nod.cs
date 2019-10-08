using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TardisControllerV5_nod : MonoBehaviour {

	
	[SerializeField] GameObject ReloadBtn;
	[SerializeField] float StopMusicAfterSeconds = 1.0f;

private void Awake() {
		ReloadBtn.SetActive(false);
	
	}
	// Use this for initialization
	void Start () {
	
		List<AnimationAttr> listAnimations = AnimationAttr.getAnimationListsFromAssets();
		
		LerpManager lerpManager = null; SlerpManager slerpManager = null;
		if((lerpManager = gameObject.GetComponent<LerpManager>())==null)
			lerpManager = gameObject.AddComponent<LerpManager>();
		

		if((slerpManager= gameObject.GetComponent<SlerpManager>())==null)
			slerpManager = gameObject.AddComponent<SlerpManager>();
		
		
		lerpManager.Intitalize(listAnimations);
		slerpManager.Intitalize(listAnimations);

		slerpManager.StartAnimating();
		lerpManager.StartAnimating();
	
	}

	private void Update() {
		if(SlerpManager.flagComplete && LerpManager.flagComplete){
			ReloadBtn.SetActive(true);
			StartCoroutine(StopMusic());
		}else{
			ReloadBtn.SetActive(false);
		}
	}
	public void onClick_Reload(){
		if(ReloadBtn!=null){ReloadBtn.SetActive(false);}
		StopAllCoroutines();
		Start();
		GetComponent<AudioSource>().Play();
	}
	
	IEnumerator StopMusic(){
		yield return new WaitForSeconds(StopMusicAfterSeconds);
		GetComponent<AudioSource>().Stop();
	}

}
