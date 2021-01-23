using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
	public GameObject platformPrefab;
	public GameObject trapPrefab;
	public GameObject healthPrefab;
	public int numTotalPlatforms;
	public float width, platformLength;

	[SerializeField]
	private float stepHeight;

	public float gameSpeed;

	private GameObject platformManager;
	public List<GameObject> platforms = new List<GameObject>();
	private float semiRange;



	public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
		SetActions();


		gameSpeed = GameManager.instance.gameSpeed;
		semiRange = (width - platformLength) / 2;
		platformManager = this.gameObject;
		SetInitPlatforms();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void SetInitPlatforms()
	{
		float tempY = 0;

		for(int i = 0; i < numTotalPlatforms; i++)
		{
			CreateSinglePlatform(tempY);
			tempY -= stepHeight;

		}

	}

	public  void CreateNewPlatform()
	{

		float tempY = (platforms[platforms.Count - 1].transform.position.y) - stepHeight;
		CreateSinglePlatform(tempY);
	}


	private void CreateSinglePlatform(float yValue)
	{
		float trapProb = Random.value;

		GameObject prefab = trapProb <= 0.2f ? trapPrefab : platformPrefab;

		Vector2 position = new Vector2(Random.Range(-semiRange,semiRange), yValue);
		GameObject newPlatform = Instantiate(prefab, position, Quaternion.identity);
		platforms.Add(newPlatform);
		newPlatform.transform.SetParent(platformManager.transform);

		CreateHealth(newPlatform);
	}

	private void CreateHealth(GameObject platform)
	{
		float healthProb = Random.value;

		if (healthProb < 0.2f)
		{
			GameObject newHealth = Instantiate(healthPrefab, new Vector3(0,0,0), Quaternion.identity);
			newHealth.transform.SetParent(platform.transform);
			newHealth.transform.localPosition = new Vector3(0, 1.2f, 0);
		}
		else
			return;
	}

	void PlatformDestroy(GameObject platform)
	{
		gameSpeed = GameManager.instance.gameSpeed;
		CreateNewPlatform();
		this.platforms.Remove(platform);
		Destroy(platform);

	}

	void SetActions()
	{
		EventManager.instance.PlatformDestroyed += PlatformDestroy;
	}

}
