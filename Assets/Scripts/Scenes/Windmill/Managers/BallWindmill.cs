using UnityEngine;
using System.Collections;

namespace Windmill { 
    public class BallWindmill : Generic.Ball
    {
        //Speed of the ball
        public float _constantWindmillBallSpeed = 20;

        public new void Start()
        {
            BallRigidbody = this.GetComponent<Rigidbody>();
            ConstantBallSpeed = _constantWindmillBallSpeed;
        }
    }

}