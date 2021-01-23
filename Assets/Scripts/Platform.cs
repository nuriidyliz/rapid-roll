using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	private float speed;
	private bool haveHealth;

	public GameObject prefab;
	private Vector2 movement;
	private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
		movement = new Vector2(0, 1);
		Time.fixedDeltaTime = 1f / 50f;		//default 50
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		MovePlatform();
    }

	void MovePlatform()
	{

		speed = GameObject.Find("PlatformManager").GetComponent<PlatformManager>().gameSpeed;
		rb.MovePosition((Vector2)transform.position + movement * speed * Time.fixedDeltaTime);
	}
}
