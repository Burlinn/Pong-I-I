using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int speed = 10;
    public GameObject ball;

    // Update is called once per frame
    void Update() {
        if (ball != null) { 
            //Make our enemy track the ball
            if (ball.transform.position.y > transform.position.y)
            {
                transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
            }
            if (ball.transform.position.y < transform.position.y)
            {
                transform.Translate(new Vector3(0, -speed, 0) * Time.deltaTime);
            }

            //Check top bounds
            if (transform.position.y > 6.75)
            {
                Vector3 holdAtTop = transform.position;
                holdAtTop.y = 6.75f;
                transform.position = holdAtTop;
            }
            if (transform.position.y < -6.75)
            {
                Vector3 holdAtBottom = transform.position;
                holdAtBottom.y = -6.75f;
                transform.position = holdAtBottom;
            }
        }   
    }
}
