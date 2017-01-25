using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public float speed;
	public float moveX = 0.0f;
	public float moveY = 0.0f;


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
		//ReadInput ();
	}

	//read for swipe inputs
	void FixedUpdate() {

		float x = this.transform.position.x;
		float y = this.transform.position.y;


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
						moveX = -speed;
						moveY = 0.0f;
						print ("left");
					} else if (swipedSideways && deltaX < 0) { 		//swipe right
						moveX = speed;
						moveY = 0.0f;
						print ("right");
					} else if (!swipedSideways && deltaY > 0) { 	//swipe down
						moveY = -speed;
						moveX = 0.0f;
						print ("down");
					} else if (!swipedSideways && deltaY < 0) {		//swipe up
						moveY = speed;
						moveX = 0.0f;
						print ("up");
					}
					hasSwiped = true;
				}
			} else if (t.phase == TouchPhase.Ended) {
				hasSwiped = false;
				initialTouch = new Touch ();
			}
		}
		x += moveX;
		y += moveY;

		if (x > world.x) {				//right edge
			x = world.x;
			killPlayer ();
		} else if (x < -world.x) {		//left edge
			x = -world.x;
			killPlayer ();
		} else if (y < -world.y) {		//bottom edge
			y = -world.y;
			killPlayer ();
		} else if (y > world.y) {		//top edge
			y = world.y;
			killPlayer ();
		}

		this.transform.position = new Vector3 (x, y, 0.0f);
	}



	//read for key inputs, this function is used for testing purposes
	void ReadInput() {
		float x = this.transform.position.x;
		float y = this.transform.position.y;

		if (Input.GetKey (KeyCode.LeftArrow)) {
			x -= speed;
			moveX = -speed;
			moveY = 0.0f;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			x += speed;
			moveX = speed;
			moveY = 0.0f;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			y += speed;
			moveY = speed;
			moveX = 0.0f;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			y -= speed;
			moveY = -speed;
			moveX = 0.0f;
		}

		x += moveX;
		y += moveY;

		if (x > world.x) {				//right edge
			x = world.x;
			killPlayer ();
		} else if (x < -world.x) {		//left edge
			x = -world.x;
			killPlayer ();
		} else if (y < -world.y) {		//bottom edge
			y = -world.y;
			killPlayer ();
		} else if (y > world.y) {		//top edge
			y = world.y;
			killPlayer ();
		}

		this.transform.position = new Vector3 (x, y, 0.0f);
	}

	public void killPlayer() {
		Destroy (this.gameObject);
		GameObject go = GameObject.Find("Controller");
		PlayerSpawn other = (PlayerSpawn) go.GetComponent(typeof(PlayerSpawn));
		other.setPlayerExists();

	}


	public void SetMovers(Vector2 movement) {
		moveX = movement.x;
		moveY = movement.y;
		print (moveX);
		print (moveY);
	}
}
