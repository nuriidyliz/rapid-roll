using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			EventManager.instance.HealthPickedAction();
		}
		ObjectPooler.Instance.Deactivate(transform.gameObject);
	}
}
