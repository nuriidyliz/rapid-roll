using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyerScript : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			EventManager.instance.EndGameAction();
			return;
		}
		DestroyPlatform(other.gameObject);
	}

	private void DestroyPlatform(GameObject platform)
	{
		EventManager.instance.ChangeScoreAction(10);
		EventManager.instance.PlatformDestroyedAction(platform);
	}




}
