using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
	[SerializeField]
	private GameObject platformPrefab;
	[SerializeField]
	private int numTotalPlatforms;
	[SerializeField]
	private float width, platformLength;

	[SerializeField]
	private float stepHeight;

	private GameObject platformManager;
	public List<GameObject> platforms = new List<GameObject>();
	private float semiRange;

	public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
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

		float tempY = (platforms[platforms.Count - 1].transform.position.y) - 2.5f;
		CreateSinglePlatform(tempY);
	}


	private void CreateSinglePlatform(float yValue)
	{
		Vector2 position = new Vector2(Random.Range(-semiRange,semiRange), yValue);
		GameObject newPlatform = Instantiate(platformPrefab, position, Quaternion.identity);
		platforms.Add(newPlatform);
		newPlatform.transform.SetParent(platformManager.transform);

	}
}
