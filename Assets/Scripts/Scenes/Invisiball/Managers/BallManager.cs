﻿using UnityEngine;
using System.Collections;
using System;

namespace Invisiball { 
    public class BallManager : MonoBehaviour
    {
        //Speed of the ball
        public float _constantBallSpeed = 20;
        //Used to manipulate delta time
        public float _gameSpeed = 10;

        //Two variables to hold our scores
	    public bool _playerScoredLast = true;
        public bool _roundHadWinner = false;
        private GameObject _ballInstance;
        private float _currentLocation;
        private float _transparency;

        public void Start()
        {
            _ballInstance = this.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 ballVelocity = GetComponent<Rigidbody>().velocity;
            Vector3 ballSpeed = ballVelocity.normalized * _constantBallSpeed;
            GetComponent<Rigidbody>().velocity = Vector3.Lerp(ballVelocity, ballSpeed, Time.deltaTime * _gameSpeed);

            _currentLocation = Math.Abs(GetComponent<Rigidbody>().position.x);
            //On a white material, transparency takes affect when the alpha channel is down to .01. Dividing by 50 to make
            //it EXTRA invisible.
            _transparency = _currentLocation / 12.5f / 50;

            var color = this.gameObject.GetComponent<Renderer>().material.color;
            color.a = _transparency;
            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",color);

            //Make ball invisible as it gets close to players.
            if(this.transform.position.x > 6 || this.transform.position.x < -6)
            {
                this.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                this.GetComponent<MeshRenderer>().enabled = true;
            }

            //If we hit the top or the bottom, bounce off of them. 
            if (transform.position.y > 8 || transform.position.y < -8)
            {
                Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
                GetComponent<Rigidbody>().velocity.Set(currentVelocity.x, -currentVelocity.y, currentVelocity.z);
            }

            //If we somehow make it past the cieling or floor, despawn the ball
		    if (transform.position.y > 8.1 || transform.position.y < -8.1 )
		    {
                _roundHadWinner = false;
                _ballInstance.SetActive(false);
            }

            //If the ball somehow ends up in a state where it's going up and down, nudge it in the right direction.
		    if (GetComponent<Rigidbody>().velocity.x < 1 && GetComponent<Rigidbody>().velocity.x > 0 )
		    {
			    Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
                GetComponent<Rigidbody>().velocity = new Vector3(2, -2, 0);
            }
            else if  (GetComponent<Rigidbody>().velocity.x > -1 && GetComponent<Rigidbody>().velocity.x <= 0 )
		    {
			    Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
                GetComponent<Rigidbody>().velocity = new Vector3(-2, 2, 0);
            }

        }

        private GameObject getBall()
        {
            return _ballInstance;
        }


        void OnCollisionEnter(Collision collision)
        {
			//Set the round winner
            if (collision.gameObject.name == "LeftWall")
            {
                _playerScoredLast = false;
                _roundHadWinner = true;
                _ballInstance.SetActive(false);
            }
            if (collision.gameObject.name == "RightWall")
            {
                _playerScoredLast = true;
                _roundHadWinner = true;
                _ballInstance.SetActive(false);
            }

            //Bounce in a direction depending on where it hits the player's paddle.
            if (collision.gameObject.name == "Player")
            {
                if (transform.position.y <= collision.transform.position.y - .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(4, 3, 0);
                }

            }
			
			//Bounce in a direction depending on where it hits the enemy's paddle.
            if (collision.gameObject.name == "Enemy")
            {
                if (transform.position.y <= collision.transform.position.y - .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(-4, -3, 0);
                }
                if (transform.position.y >= collision.transform.position.y + .3)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(-4, 3, 0);
                }

            }

        }
    }

}