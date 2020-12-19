using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
	public float InputValue { get; set; } = 0;

	public abstract void GetInput();
}
