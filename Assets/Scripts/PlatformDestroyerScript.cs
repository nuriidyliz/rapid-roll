using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyerScript : MonoBehaviour
{
	public UIManager uiManager;
	public PlatformManager platformManager;

	private void Start()
	{
		uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();	
		platformManager = GameObject.Find("PlatformManager").GetComponent<PlatformManager>();	
	}

	private void OnTriggerEnter(Collider other)
	{
		DestroyPlatform(other.gameObject);
		platformManager.score += 10;
		
		uiManager.SetScore(platformManager.score);
		SetSpeed(other.gameObject.GetComponent<Platform>().speed + 0.1f);
	}


	private void DestroyPlatform(GameObject platform)
	{
		if (platform.tag.Equals("Player"))
		{
			EndGame();
			return;
		}
		platformManager.platforms.Remove(platform);
		Destroy(platform);
		platform.GetComponentInParent<PlatformManager>().CreateNewPlatform();
	}

	public void EndGame()
	{
		uiManager.retryButton.gameObject.SetActive(true);
		SetSpeed(0);
	}

	private void SetSpeed(float speed)
	{
		foreach (GameObject elem in platformManager.platforms)
		{
			elem.GetComponent<Platform>().speed = speed;
		}
	}

}
