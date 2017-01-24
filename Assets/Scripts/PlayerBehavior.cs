using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public float speed;

	Vector3 world;

	private Touch initialTouch = new Touch();
	private float distance = 0;
	private bool hasSwiped = false;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		world = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
	}
	
	// Update is called once per frame
	void Update () {
		ReadInput ();
	}


	//read for swipe inputs
	void FixedUpdate() {
		foreach (Touch t in Input.touches) {
			if (t.phase == TouchPhase.Began) {
				initialTouch = t;
			} else if (t.phase == TouchPhase.Moved && !hasSwiped) {
				float deltaX = initialTouch.position.x - t.position.x;
				float deltaY = initialTouch.position.y - t.position.y;
				distance = Mathf.Sqrt ((deltaX * deltaX) + (deltaY * deltaY));
				bool swipedSideways = Mathf.Abs (deltaX) > Mathf.Abs (deltaY);

				if (distance > 50f) {
					if (swipedSideways && deltaX > 0) {				//swipe left
						print("left");
					} else if (swipedSideways && deltaX < 0) { 		//swipe right
						print("right");
					} else if (!swipedSideways && deltaY > 0) { 	//swipe down
						print("down");
					} else if (!swipedSideways && deltaY < 0) {		//swipe up
						print("up");
					}
					hasSwiped = true;
				}
			} else if (t.phase == TouchPhase.Ended) {
				hasSwiped = false;
				initialTouch = new Touch ();
			}
		}
	}



	//read for key inputs
	void ReadInput() {
		float x = this.transform.position.x;
		float y = this.transform.position.y;

		if (Input.GetKey (KeyCode.LeftArrow)) {
			x -= speed;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			x += speed;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			y += speed;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			y -= speed;
		}

		if (x > world.x) {
			x = world.x;
			print ("right edge");
		} else if (x < -world.x) {
			x = -world.x;
			print ("left edge");
		} else if (y < -world.y) {
			y = -world.y;
			print ("top edge");
		} else if (y > world.y) {
			y = world.y;
			print ("bottom edge");
		}

		this.transform.position = new Vector3 (x, y, 0.0f);
	}
}
