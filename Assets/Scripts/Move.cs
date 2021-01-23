using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

	private float speed;
	private float gameSpeed;
	private Rigidbody rb;
	private Vector3 movement;

	Controller controller;


	bool canFall;

	// Start is called before the first frame update
	void Start()
    {
		//Application.targetFrameRate = 50;
		rb = GetComponent<Rigidbody>();
		SetController();
		speed = GameManager.instance.ballHorizontalSpeed;
	}

	// Update is called once per frame
	//void Update()
 //   {
	//	//movement = new Vector2(input, 0);
	//	//Debug.Log(controller.InputValue);
	//	//Debug.Log(movement);
	//}

	public void FixedUpdate()
	{

		if (this.gameObject.transform.position.y < -7)
		{
			EventManager.instance.EndGameAction();
		}
		TryMove();
		//Fall();

	}


	private void TryMove()
	{
		float fallSpeed;

		fallSpeed = 0;


		movement = new Vector3(controller.InputValue, 0, 0);

		//rb.AddForce(new Vector3(controller.InputValue, 0, 0),ForceMode.Acceleration);


		//rb.velocity = movement * speed;

		//gameSpeed = GameObject.Find("PlatformManager").GetComponent<PlatformManager>().gameSpeed;
		Vector3 fall = new Vector3(0, -2 - gameSpeed, 0);

		//rb.MovePosition(transform.position + (movement * Time.fixedDeltaTime * speed));
		rb.AddForce((movement * speed - new Vector3(rb.velocity.x, 0, 0)), ForceMode.VelocityChange);
		//Debug.Log(rb.velocity);

		//rb.velocity = movement*Time.fixedDeltaTime*speed;
		Vector3 m_EulerAngleVelocity = new Vector3(0, 0, -1000 * controller.InputValue);
		Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
		rb.MoveRotation(rb.rotation * deltaRotation);
	}

	private void Fall()
	{
		//rb.velocity += new Vector3(0, -2, 0);
		Vector3 fall = new Vector3(0, -2, 0);
		gameSpeed = GameObject.Find("PlatformManager").GetComponent<PlatformManager>().gameSpeed;
		rb.MovePosition(transform.position + (fall * (10) * Time.deltaTime));

	}
	private void SetController()
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		controller = GameObject.Find("Main Camera").GetComponent<StandaloneController>();
		controller.enabled = true;
#elif UNITY_ANDROID && !UNITY_EDITOR
		controller = GameObject.Find("Main Camera").GetComponent<MobilController>();
		controller.enabled = true;
#endif
	}

	private void OnCollisionEnter(Collision collision)
	{

		if (collision.gameObject.tag == "Trap")
		{
			//EventManager.instance.EndGameAction();
			EventManager.instance.HealthLostAction();

		}

	}


}
