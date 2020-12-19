using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

	[SerializeField]
	private float speed = 10;
	private Rigidbody rb;
	private Vector2 movement;

	Controller controller;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		SetController();
    }

    // Update is called once per frame
    void Update()
    {
		//movement = new Vector2(input, 0);
		//Debug.Log(controller.InputValue);
		//Debug.Log(movement);
	}

	private void FixedUpdate()
	{
		if(this.gameObject.transform.position.y < -7)
		{
			GameObject.Find("PlatformDestroyer").GetComponent<PlatformDestroyerScript>().EndGame();
		}
		TryMove();
	}

	private void TryMove()
	{

		movement = new Vector2(controller.InputValue, 0);

		//rb.AddForce(new Vector3(0, 0, 0), ForceMode.VelocityChange);


		//rb.velocity = movement * speed;
	
		rb.MovePosition((Vector2)transform.position + movement * speed * Time.fixedDeltaTime);
	}

	private void SetController()
	{
		//#if UNITY_EDITOR || UNITY_STANDALONE
		controller = GameObject.Find("Main Camera").GetComponent<StandaloneController>();
		controller.enabled = true;

		//#elif UNITY_ANDROID
		//controller = GameObject.Find("Main Camera").GetComponent<MobilController>();
		//controller.enabled = true;
		//#endif
	}

}
