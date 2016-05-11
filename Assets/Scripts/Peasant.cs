using UnityEngine;
using System.Collections;

public class Peasant : MonoBehaviour {

	private enum PeasantState{
		Towards, Waiting, Away
	}


	PeasantState state;
	private float waitTime = 1;
	private float timer = 0;
	private float speed = 2;
	private SpriteRenderer sr;
	private float startX;

	void Start () {
		state = PeasantState.Towards;
		sr = GetComponent<SpriteRenderer>();
		startX = Mathf.Abs(transform.position.x);
	}

	void Update () {
		switch(state){
		case PeasantState.Towards:
			if(Mathf.Abs(transform.position.x)<2){
				state = PeasantState.Waiting;
			}else{
				transform.position+=new Vector3(
					speed*Time.deltaTime*(sr.flipX?-1:1),0,0);
			}
			break;
		case PeasantState.Waiting:
			timer+=Time.deltaTime;
			if(timer>waitTime){
				state = PeasantState.Away;
				sr.flipX = !sr.flipX;
				Update();
			}
			break;
		case PeasantState.Away:
			if(Mathf.Abs(transform.position.x)<startX){
			transform.position+=new Vector3(
				speed*Time.deltaTime*(sr.flipX?-1:1),0,0);

			}else{
				Score.gold -= 25;
				Destroy(gameObject);
			}
			break;
			default:
			break;
		}




	}
}
