using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish : MonoBehaviour
{
	private bool touchingDoor = false;
	public GameObject winningSign;
	public bool isWin = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKey("r")) {
			isWin=false;
			winningSign.SetActive(false);
		}
		if (touchingDoor)
			FinishLevel();
		/*
		if(Input.GetKeyDown (KeyCode.F))
		{
			Debug.Log("F be pressed");
			//check for player's collision with game object tagged Dock
			if (touchingDoor)
				FinishLevel();
		}*/
    }

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Touch door");
		if (other.tag == "Player") {
			touchingDoor = true;
			Debug.Log("Touch door");
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			touchingDoor = false;
		}
	}

	void FinishLevel() {
		winningSign.SetActive(true);
		isWin = true;
		//Time.timeScale=0;
	}
}
