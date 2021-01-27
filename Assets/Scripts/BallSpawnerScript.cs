using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{
	#region Singleton
	public static BallSpawnerScript Instance;

	private void Awake()
	{
		Instance = this;
	}
	#endregion

	public GameObject ball;



	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Platform"))
		{

			ball.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.5f, 0);
			transform.gameObject.SetActive(false);
		}
	}



}
