using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[Header("Movement Settings")]
	public float gameSpeed;
	public float ballHorizontalSpeed;
	public float gravityValue;

	[Header("Player Variables")]
	public int health;
	public int maxHealth;
	public int score;

	private PlatformManager platformManager;

	private void Awake()
	{
		instance = this;
		Physics.gravity = new Vector3(0, gravityValue, 0);
	}

	// Start is called before the first frame update
	void Start()
    {
		SetActions();

		platformManager = GameObject.Find("PlatformManager").GetComponent<PlatformManager>();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void IncrementHealth()
	{
		if(health < maxHealth)
			this.health++;
	}

	public void HealthLost()
	{
		if (health == 0)
			EventManager.instance.EndGameAction();
		else
			health--;
	}

	public void EndGame()
	{
		ChangeSpeed(0f);
	}

	public void PlatformDestroy(GameObject platform)
	{
		ChangeSpeed(0.01f);
		ChangeScore(10);
	}

	public void ChangeSpeed(float speed)
	{
		if (speed == 0)
		{
			gameSpeed = speed;
		}
		else
			gameSpeed += speed;

		platformManager.gameSpeed = gameSpeed;
	}

	public void ChangeScore(int value)
	{

		this.score += value;
	}

	private void SetActions()
	{
		EventManager.instance.HealthPicked += IncrementHealth;
		EventManager.instance.EndGame += EndGame;
		EventManager.instance.PlatformDestroyed += PlatformDestroy;
		EventManager.instance.HealthLost += HealthLost;
	}
}
