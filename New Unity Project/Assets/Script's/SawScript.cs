using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
	public float speed = 100f;

	void Update()
	{
		transform.Rotate(new Vector3(0f, 0f, speed * Time.deltaTime));
	}
}
