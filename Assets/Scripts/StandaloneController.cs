using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneController : Controller
{

	private float keyValue;
    // Update is called once per frame
    void Update()
	{
		GetInput();
	}

	public override void GetInput()
	{
		keyValue = Input.GetAxis("Horizontal");

		base.InputValue = keyValue > 0 && Input.anyKey ? 1 : keyValue < 0 && Input.anyKey ? -1 : 0;
	}

}
