using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyerScript : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			if(GameManager.instance.health > 0)
			{
				other.gameObject.transform.position = new Vector3(-3.1f, 10f, 0);
				StartCoroutine(GameManager.instance.RespawnBall());
			}
			else
			{
				EventManager.instance.EndGameAction();
				return;
			}
		}
		else
		{
			ObjectPooler.Instance.Deactivate(other.gameObject);
		}
		DestroyPlatform(other.gameObject);
	}

	private void DestroyPlatform(GameObject platform)
	{
		EventManager.instance.ChangeScoreAction(10);
		EventManager.instance.PlatformDestroyedAction(platform);
	}




}
