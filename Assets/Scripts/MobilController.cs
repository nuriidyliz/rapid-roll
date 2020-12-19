using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilController : Controller
{


	private Touch touch;
	//private Vector2 fingerDownPosition;
	//private Vector2 fingerUpPosition;

	private float minDistanceForSwipe = 20f;


    // Update is called once per frame
    void Update()
    {
		if (Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);

			GetInput();

		}
		else
			base.InputValue = 0;

	}

	public override void GetInput()
	{
		//if (touch.phase == TouchPhase.Began)
		//{
		//	fingerDownPosition = touch.position;
		//	fingerUpPosition = touch.position;
		//}

		//if (touch.phase == TouchPhase.Ended)
		//{
		//	fingerUpPosition = touch.position;
		//}

		if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
		{
			base.InputValue = touch.position.x > Screen.width / 2 ? 1 : -1;
		}
		else
			base.InputValue = 0;
	}

	//public bool SwipeDistanceCheckMet()
	//{
	//	return HorizontalMovementDistance() > minDistanceForSwipe;
	//}



	//public float HorizontalMovementDistance()
	//{
	//	return fingerDownPosition.x - fingerUpPosition.x;

	//}
}
