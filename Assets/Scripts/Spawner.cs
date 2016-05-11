using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Transform left, right;
	public Object peasant;
	private float timer = 0;

	private float delta = 2f;

	void Update () {
		timer+=Time.deltaTime;
		delta = -Mathf.Pow(Time.time, .5f)/10 + 2f; 
		if(timer >= delta){
			timer = 0;
			Spawn();
		}
	}

	void Spawn(){
		bool side = (Random.Range(0,2)==0?true:false);
		Transform t = null;
		if(side){
			t = right;
		}else{
			t = left;
		}
		GameObject person = (GameObject)GameObject.Instantiate(peasant,
			t.position,Quaternion.Euler(0,0,0));
		person.GetComponent<SpriteRenderer>().flipX = side;
	}
}
