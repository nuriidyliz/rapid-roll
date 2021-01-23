using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Button retryButton;
	public Text scoreText;
	public Text fpsText;
	public Text healthText;
	public Text speedText;

	private float timer;
	private float fps;

	// Start is called before the first frame update
	void Start()
    {
		SetActions();
		timer = 0;
		retryButton.onClick.AddListener(Retry);


    }

    // Update is called once per frame
    void Update()
    {
		fps = 1.0f / Time.deltaTime;

		if (timer >= 2)
		{
			fpsText.text = "avg FPS: " + (int)fps;
			timer = 0;

		}
		else
			timer += Time.deltaTime;
    }


	public void Retry()
	{
		SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
	}

	public void SetScore(int score)
	{
		scoreText.text = "Score: " + GameManager.instance.score;
		speedText.text = "Speed: " + GameManager.instance.gameSpeed.ToString("F2");
	}

	private void SetHealth()
	{
		healthText.text = "Health: " + GameManager.instance.health;
	}

	private void EndGame()
	{
		retryButton.gameObject.SetActive(true);
	}

	private void SetActions()
	{
		EventManager.instance.HealthPicked += SetHealth;
		EventManager.instance.HealthLost += SetHealth;
		EventManager.instance.EndGame += EndGame;
		EventManager.instance.ChangeScore += SetScore;
	}
}
