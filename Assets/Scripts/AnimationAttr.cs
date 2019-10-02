using UnityEngine;


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