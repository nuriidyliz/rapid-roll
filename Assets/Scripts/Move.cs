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

		if (this.gameObject.transform.position.y < -7)
		{
			gameObject.transform.position = new Vector3(-3.1f, 10f, 0);

			if (GameManager.instance.health > 0)
			{
				StartCoroutine(RespawnBall(1));

			}
			else
			{
				Debug.Log("nası girdin");
				EventManager.instance.EndGameAction();

			}
		}
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
		//debug.drawray(rb.position, (movement * speed), color.yellow);
		//debug.drawray(rb.position, -new vector3(rb.velocity.x, 0, 0), color.red);
		//debug.drawray(rb.position, -(movement * speed), color.blue);
		#endregion
		
		// wallHit prevents sticks to side walls
		rb.AddForce((movement * speed - new Vector3(rb.velocity.x, 0, 0)), ForceMode.VelocityChange);
		if (wallHit)
			rb.AddForce(-(movement * speed), ForceMode.VelocityChange);
		if (controller.InputValue == 0)
			wallHit = false;



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

	IEnumerator RespawnBall(int second)
	{
		Debug.Log("1");
		//transform.gameObject.SetActive(false);
		yield return new WaitForSeconds(second);
		//transform.gameObject.SetActive(true);
		Debug.Log("2");

		EventManager.instance.HealthLostAction();
		//EventManager.instance.BallSpawnAction();
		BallSpawnerScript.Instance.gameObject.SetActive(true);

	}
}
