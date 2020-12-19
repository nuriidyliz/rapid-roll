using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Button retryButton;
	public Text scoreText;

	// Start is called before the first frame update
	void Start()
    {
		retryButton.onClick.AddListener(Retry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void Retry()
	{
		SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
	}

	public void SetScore(int score)
	{
		scoreText.text = "Score: " + score;
	}
}
