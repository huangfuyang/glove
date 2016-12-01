using UnityEngine;
using System.Collections;

public class Done_Mover_enemy : MonoBehaviour
{
    public static float enemySpeed;


    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward *enemySpeed;
    }

}
