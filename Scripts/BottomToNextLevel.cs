using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomToNextLevel : MonoBehaviour
{
    // If the bottom helix collides with the ball, the next level is loaded.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameManager.singleton.NextLevel();
        }
    }
}
