using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightDetector : MonoBehaviour {

    public CharacterMovment movment;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("obstacle")) {
            IObstacle obstacle = other.gameObject.GetComponent<IObstacle>();
            movment.invokeSeeEvent(obstacle);
        }
    }
}
