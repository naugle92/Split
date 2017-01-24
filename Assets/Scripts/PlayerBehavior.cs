using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		ReadInput ();
	}

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
		this.transform.position = new Vector3 (x, y, 0.0f);
	}
}
