using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

	public float speed;
	float moveX;
	float moveY;

	float x = 0.0f;
	float y = 0.0f;

	public Object player;
	Vector3 world;

	private Touch initialTouch = new Touch();
	private float distance = 0;
	private bool hasSwiped = false;
	private bool needsToSpawn = false;
	private bool playerExists = false;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		world = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
		moveX = 0.0f;
		moveY = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//read for swipe inputs
	void FixedUpdate() {

		if (!playerExists) {


			foreach (Touch t in Input.touches) {
				if (t.phase == TouchPhase.Began) {
					initialTouch = t;
				} else if (t.phase == TouchPhase.Moved && !hasSwiped) {
					float deltaX = initialTouch.position.x - t.position.x;
					float deltaY = initialTouch.position.y - t.position.y;
					distance = Mathf.Sqrt ((deltaX * deltaX) + (deltaY * deltaY));
					bool swipedSideways = Mathf.Abs (deltaX) > Mathf.Abs (deltaY);

					if (distance > 50f) {
						Vector3 touchpoint = Camera.main.ScreenToWorldPoint (new Vector3 (initialTouch.position.x, initialTouch.position.y, 0.0f));
						if (swipedSideways && deltaX > 0) {				//swipe left
							x = world.x;
							y = touchpoint.y;
							moveX = -speed;
							moveY = 0.0f;
							print ("left");
						} else if (swipedSideways && deltaX < 0) { 		//swipe right
							x = -world.x;
							y = touchpoint.y;
							moveX = speed;
							moveY = 0.0f;
							print ("right");
						} else if (!swipedSideways && deltaY > 0) { 	//swipe down
							x = touchpoint.x;
							y = world.y;
							moveX = 0.0f;
							moveY = -speed;
							print ("down");
						} else if (!swipedSideways && deltaY < 0) {		//swipe up
							x = touchpoint.x;
							y = -world.y;
							moveX = 0.0f;
							moveY = speed;
							print ("up");
						}
						hasSwiped = true;
						needsToSpawn = true;

					}
				} else if (t.phase == TouchPhase.Ended) {
					hasSwiped = false;
					initialTouch = new Touch ();
					if (needsToSpawn) {
						//spawn the player
						GameObject dot = Instantiate (player, new Vector3(x, y, 0.0f), this.transform.rotation) as GameObject;
						dot.SendMessage("SetMovers", new Vector2(moveX, moveY));
						print (moveX);
						print (moveY);
						x = 0.0f;
						y = 0.0f;
						needsToSpawn = false;
						playerExists = true;
					}
				}
			}
		}
	}

	public void setPlayerExists() {
		playerExists = false;
	}
}
