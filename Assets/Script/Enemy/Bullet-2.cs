using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
 private Rigidbody rb;

private void Awake() {
     rb = GetComponent<Rigidbody>();
}
 private void Start() {
       
        rb.velocity = transform.forward * 100;
 }

private void OnTriggerEnter(Collider other) {
    Destroy(gameObject);
}


   
}
