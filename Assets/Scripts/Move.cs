using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

	private float speed;
	private float gameSpeed;
	private Rigidbody rb;
	private Vector3 movement;
	private bool wallHit = false;
	Controller controller;

	private float prevInput;


	bool canFall;

	// Start is called before the first frame update
	void Start()
    {
		//Application.targetFrameRate = 50;
		rb = GetComponent<Rigidbody>();
		SetController();
		speed = GameManager.instance.ballHorizontalSpeed;
		//EventManager.instance.BallSpawnAction();
		BallSpawnerScript.Instance.gameObject.SetActive(true);
	}


	public void FixedUpdate()
	{

		//if (this.gameObject.transform.position.y < -7)
		//{
		//	gameObject.transform.position = new Vector3(-3.1f, 10f, 0);

		//	if (GameManager.instance.health > 0)
		//	{
		//		StartCoroutine(GameManager.instance.RespawnBall());

		//	}
		//	else
		//	{
		//		Debug.Log("nası girdin");
		//		EventManager.instance.EndGameAction();

		//	}
		//}
		//Debug.Log("x: " + rb.velocity.x + "y: " + rb.velocity.y + "gra : " + Physics.gravity);

		if (true)
		{
			TryMove();
		}
		else
			Debug.Log("asdfasdfas");


		//Fall();

	}


	private void TryMove()
	{

		// Get movement direction vector
		movement = new Vector3(controller.InputValue, 0, 0);

		#region			Some movement method
		//rb.AddForce(new Vector3(controller.InputValue, 0, 0), ForceMode.Acceleration);
		//rb.velocity = movement * speed;
		//rb.MovePosition(transform.position + (movement * Time.fixedDeltaTime * speed));
		#endregion
		#region Draw velocity lines
		Debug.DrawRay(rb.position, (movement * speed), Color.yellow);
		//Debug.DrawRay(rb.position, -new Vector3(rb.velocity.x, 0, 0), Color.red);
		//Debug.DrawRay(rb.position, -(movement * speed), Color.blue);
		#endregion

		// wallHit prevents sticks to side walls
		rb.AddForce((movement * speed - new Vector3(rb.velocity.x, 0, 0)), ForceMode.VelocityChange);
		if (wallHit)
		{
			rb.AddForce(-(movement * speed), ForceMode.VelocityChange);
			Debug.DrawRay(rb.position, -(movement * speed), Color.blue);
		}


		if (controller.InputValue != prevInput)
		{
			wallHit = false;
			prevInput = controller.InputValue;
		}



		// rotates the balls according to move direction
		Vector3 m_EulerAngleVelocity = new Vector3(0, 0, -1000 * controller.InputValue);
		Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
		rb.MoveRotation(rb.rotation * deltaRotation);
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
		else if (collision.gameObject.tag == "Wall")
		{
			wallHit = true;
		}

	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Trap")
		{

		}
		else if (collision.gameObject.tag == "Wall")
		{
			wallHit = false;
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "PlatformSide")
		{
			Debug.Log("side");
			wallHit = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "PlatformSide")
		{
			wallHit = false;
			Debug.Log("cıktı");
		}
	}
}
