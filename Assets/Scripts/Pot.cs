using UnityEngine;
using System.Collections;

public class Pot : MonoBehaviour {

	public enum PotState{
		Waiting, Dropping, CoolDown
	}

	public Transform topLeft, botRight;
	public SpriteRenderer oil;

	private SpriteRenderer sr;

	private float minX, maxX, minY, maxY;
	private float timer = 0;

	private float dropTime = .75f;
	private float coolDown = 2;
	public PotState state;
	private float potSize = .3f;

	private Color clear = new Color(1,1,1,0);
	private Color opaque = new Color(1,1,1,1);

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		minX = Camera.main.WorldToScreenPoint(topLeft.position).x;
		maxX = Camera.main.WorldToScreenPoint(botRight.position).x;
		minY = Camera.main.WorldToScreenPoint(botRight.position).y;
		maxY = Camera.main.WorldToScreenPoint(topLeft.position).y;
		state = PotState.Waiting;
		Input.simulateMouseWithTouches = false;
	}
	
	// Update is called once per frame
	void Update () {

		switch(state){
		case PotState.Waiting:
			Touch[] touches = Input.touches;
			foreach(Touch t in touches){
				Debug.Log("touched");
				if(InBounds(t.position)){
					oil.color = opaque;
					sr.flipY = !sr.flipY;
					state = PotState.Dropping;
					timer = 0;
					Update();
				}
			}

			// Mouse for debug
			if(Input.GetMouseButtonDown(0)){
				if(InBounds(Input.mousePosition)){
					oil.color = opaque;
					sr.flipY = !sr.flipY;
					state = PotState.Dropping;
					timer = 0;
					Update();
				}
			}

			break;
		case PotState.Dropping:
			timer += Time.deltaTime;

			RaycastHit2D[] rc = Physics2D.RaycastAll(
				new Vector2(transform.position.x+potSize, transform.position.y),
				Vector2.down*12);
			
			foreach(RaycastHit2D r in rc){
				Destroy(r.collider.gameObject);
			}

				rc = Physics2D.RaycastAll(
				new Vector2(transform.position.x-potSize, transform.position.y),
				Vector2.down*12);
			
			foreach(RaycastHit2D r in rc){
				Destroy(r.collider.gameObject);
			}

			if(timer>=dropTime){
				oil.color = clear;
				state = PotState.CoolDown;
				timer = 0;
				Update();
			}
			break;

		case PotState.CoolDown:
			timer += Time.deltaTime;

			if(timer>=coolDown){
				sr.flipY = !sr.flipY;
				state = PotState.Waiting;
				timer = 0;
				Update();
			}
			break;
			default:
			break;
		}
	}


	bool InBounds(Vector3 v){ // for mouse testing
		return (v.x < maxX && v.x > minX && v.y < maxY && v.y > minY);
	}
	bool InBounds(Vector2 v){
		return (v.x < maxX && v.x > minX && v.y < maxY && v.y > minY);
	}
}
