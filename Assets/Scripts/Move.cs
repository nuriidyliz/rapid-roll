using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

	[SerializeField]
	private float speed = 10;
	private float input = 0;
	private Rigidbody rb;
	private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
		input = Input.GetAxis("Horizontal");
		movement = new Vector2(input, 0);
		Debug.Log(movement);
	}

	private void FixedUpdate()
	{
		//rb.AddForce(new Vector3(input, 0, 0), ForceMode.Impulse);
		rb.MovePosition((Vector2)transform.position + movement * speed * Time.deltaTime);
		//rb.velocity = movement * speed;
	}
}
