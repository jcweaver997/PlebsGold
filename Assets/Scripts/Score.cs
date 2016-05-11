using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public static int gold{get{
			return pgold;
		} set{
			pgold = value;
			if(pgold <=0){
				pgold = 0;
				Time.timeScale = 0;
			}
		}
	}

	public Text tgold;
	public Text tscore;



	private static int pgold;
	private float score;


	// Use this for initialization
	void Start () {
		gold = 500;
	}
	
	// Update is called once per frame
	void Update () {
		score = Time.time*2;
		tgold.text = "Gold Left: "+gold;
		tscore.text = "Score: "+Mathf.Round(score);
	
	}
}
