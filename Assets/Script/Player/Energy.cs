using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
  
   
    public PlayerMovement player;




    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            //Debug.Log("000");
          //  player.Health(1);
        }

    }
}
