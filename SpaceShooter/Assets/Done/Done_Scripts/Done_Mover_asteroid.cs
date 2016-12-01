using UnityEngine;
using System.Collections;

public class Done_Mover_asteroid : MonoBehaviour
{
	public static float asteroidSpeed;


	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * asteroidSpeed;
	}

}
