using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager instance;

	public event Action HealthPicked;
	public event Action<GameObject> PlatformDestroyed;
	public event Action EndGame;
	public event Action<int> ChangeScore;
	public event Action HealthLost;

	private void Awake()
	{
		instance = this;
	}

	public void HealthPickedAction()
	{
		HealthPicked?.Invoke();
	}

	public void PlatformDestroyedAction(GameObject platform)
	{
		PlatformDestroyed?.Invoke(platform);
	}

	public void EndGameAction()
	{
		EndGame?.Invoke();
	}

	public void ChangeScoreAction(int value)
	{
		ChangeScore?.Invoke(value);
	}

	public void HealthLostAction()
	{
		HealthLost?.Invoke();
	}


}
