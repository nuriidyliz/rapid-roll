using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public float speed;


	public GameObject prefab;
	private Vector2 movement;
	private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
		movement = new Vector2(0, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		MovePlatform();
    }

	void MovePlatform()
	{
		rb.MovePosition((Vector2)transform.position + movement * speed * Time.fixedDeltaTime);

	}
}
