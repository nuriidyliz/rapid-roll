using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
	ObjectPooler objectPooler;
	GameManager gameManager;
	private GameObject platformManager;


	public float stepHeight;



	public List<GameObject> platforms = new List<GameObject>();
	public int numTotalPlatforms;

	[Header("Trap and Health")]
	public float trapProb;
	public int trapLimit, trapNumber;
	public float healthProb;
	public int healthLimit, healthNumber;
	public float healthDistanceFromPlatform;
	
	[Header("Paths")]
	public int numberOfPath;
	public List<float> pathPositions;

	//private float semiRange;
	[System.NonSerialized]
	public float gameSpeed;
	[System.NonSerialized]
	public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
		objectPooler = ObjectPooler.Instance;
		gameManager = GameManager.instance;

		trapNumber = 0;
		healthNumber = 0;

		SetActions();


		gameSpeed = gameManager.gameSpeed;
		//semiRange = (width - platformLength) / 2;
		platformManager = this.gameObject;

		SetPathPositions();

		SetInitPlatforms();
    }

	void SetPathPositions()
	{
		//RIGHT AND LEFT WALLS MUST BE EQUIVALENT AND MUST BE THE SAME DISTANCE FROM 0
		float leftWallPosition, rightWallPosition, wallWidth;

		GameObject sideWalls = GameObject.Find("SideWalls");
		leftWallPosition = sideWalls.transform.Find("LeftWall").transform.position.x;
		//rightWallPosition = sideWalls.transform.Find("RightWall").transform.position.x;
		wallWidth = sideWalls.transform.Find("RightWall").transform.localScale.x;

		float platformWidth;
		GameObject platformPrefab = objectPooler.pools.Find(item => item.tag.Equals("Platform")).prefab;
		platformWidth = platformPrefab.transform.localScale.x;

		float leftmostPathPosition, tolaranceDistance;
		tolaranceDistance = 0.03f;
		leftmostPathPosition = leftWallPosition + (float)(wallWidth / 2) + (float)(platformWidth / 2) + tolaranceDistance;

		float spawnRange, intervalDistance;
		spawnRange = Mathf.Abs(leftmostPathPosition) * 2;
		intervalDistance = (float)(spawnRange / (numberOfPath - 1));

		for(int i = 0; i < numberOfPath; i++)
		{
			pathPositions.Add(leftmostPathPosition + (i * intervalDistance));
		}


	}

	void SetInitPlatforms()
	{
		float tempY = 0;

		for(int i = 0; i < numTotalPlatforms; i++)
		{
			//CreateSinglePlatform(tempY);
			SpawnPlatform(tempY);
			tempY -= stepHeight;

		}

	}

	void SpawnPlatform(float lastYValue)
	{
		var xValue = pathPositions[(int)(Random.value * numberOfPath)];

		Vector3 randPos = new Vector3(xValue, lastYValue, 0);
		string platformType = IsTrap() ? "Trap" : "Platform";

		GameObject newPlatform = objectPooler.SpawnFromPool(platformType, randPos, Quaternion.identity);
		platforms.Add(newPlatform);
		newPlatform.transform.SetParent(platformManager.transform);

		if (platformType.Equals("Platform"))
		{
			if (HasHealth())
			{
				GameObject newHealth = objectPooler.SpawnFromPool("Health", new Vector3(0, 0, 0), Quaternion.identity);
				newHealth.transform.SetParent(newPlatform.transform);
				newHealth.transform.localPosition = new Vector3(0, healthDistanceFromPlatform, 0);
				healthNumber++;
			}
		}
	}

	bool IsTrap()
	{
		bool isTrap = false;

		if (trapNumber < trapLimit)
		{
			float trapProb = Random.value;

			if(trapProb <= this.trapProb)
			{
				isTrap = true;
				trapNumber++;
			}
		}

		return isTrap;
	}

	bool HasHealth()
	{
		bool hasHealth = false;

		if(healthNumber < healthLimit)
		{
			float healthProb = Random.value;

			if(healthProb <= this.healthProb)
			{
				hasHealth = true;
				healthProb++;
			}
		}

		return hasHealth;
	}


	void PlatformDestroy(GameObject platform)
	{
		if (platform.tag.Equals("Trap"))
		{
			trapNumber--;
		}
		else if (platform.tag.Equals("Health"))
		{
			healthNumber--;
			Debug.Log("asfdjıo");
		}

		SpawnPlatform((platforms[platforms.Count - 1].transform.position.y) - stepHeight);
		this.platforms.Remove(platform);

	}

	void HealthPicked()
	{
		healthNumber--;
	}

	void SetActions()
	{
		EventManager.instance.PlatformDestroyed += PlatformDestroy;
		EventManager.instance.HealthPicked += HealthPicked;
	}

}
