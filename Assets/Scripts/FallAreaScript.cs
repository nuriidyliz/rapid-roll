using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAreaScript : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			other.gameObject.transform.position = new Vector3(-3.1f, 10f, 0);

			if (GameManager.instance.health > 0)
			{
				StartCoroutine(GameManager.instance.RespawnBall());
			}
			else
			{
				EventManager.instance.EndGameAction();
				return;
			}
		}
	}
}
