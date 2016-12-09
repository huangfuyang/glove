using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	//
	//private static Timer tm;
	//public float angle = 0f;
	//public float angleValue = (angle-50)/7.14;
	
	//tm = new System.Threading.Timer(angle++, null, 0, 1000);
	//
	
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	 
	private float nextFire;

    //
    private bool openFire = false;
	
	void Update ()
	{
        if (Done_GameController.practiceModel) {
            if (openFire && Time.time > nextFire) {
                openFire = false;
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play();
            }
        }
		else if(Time.time > nextFire) 
		//if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}

	void FixedUpdate ()
	{
		
		float XmoveHorizontal = DataConversion.GetHorizontalMove();		
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

        //		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        //		GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            XmoveHorizontal,
            0.0f,
            0.0f
        );


        GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
//
        if ( ((GetComponent<Rigidbody>().position.x < 0.3)&&(GetComponent<Rigidbody>().position.x >-0.3))  || (GetComponent<Rigidbody>().position.x == 6) || (GetComponent<Rigidbody>().position.x == -6)) {
            openFire = true;
        }
		
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
