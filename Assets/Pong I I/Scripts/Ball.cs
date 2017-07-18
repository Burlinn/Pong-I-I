using UnityEngine;
using System.Collections;


public class Ball : MonoBehaviour
{

    public float cSpeed = 5;
    public float sFactor = 5;

    //Two variables to hold our scores
    public int playerScore = 0;
    public int enemyScore = 0;
    Vector3[] playerStartingVectors = new Vector3[4];
	Vector3[] enemyStartingVectors = new Vector3[4];
	public bool playerScoredLast = true;

    // Use this for initialization
    void Start()
    {
		
		playerStartingVectors[0] = new Vector3(3, 2, 0);
		playerStartingVectors[1] = new Vector3(3, -2, 0);
		playerStartingVectors[2] = new Vector3(4, 3, 0);
		playerStartingVectors[3] = new Vector3(4, -3, 0);
		enemyStartingVectors[0] = new Vector3(-3, 2, 0);
		enemyStartingVectors[1] = new Vector3(-3, -2, 0);
		enemyStartingVectors[2] = new Vector3(-4, 3, 0);
		enemyStartingVectors[3] = new Vector3(-4, -3, 0);
        ResetBall();
    }

    void ResetBall()
    {
        if (playerScore < 10 && enemyScore < 10)
        {
            Vector3 startPosition = transform.position;
			Vector3 startingVector = new Vector3 ();

			if (playerScoredLast == true) {
				startingVector = playerStartingVectors[Random.Range(0, 4)];
				startPosition.x = -11;
			}

			if (playerScoredLast == false) {
				startingVector = enemyStartingVectors[Random.Range(0, 4)];
				startPosition.x = 11;
			}

			GetComponent<Rigidbody>().velocity = startingVector;
            
            startPosition.y = 0;
            transform.position = startPosition;

            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "LeftWall")
        {
            enemyScore++;
            ResetBall();
			playerScoredLast = false;
        }
        if (collision.gameObject.name == "RightWall")
        {
            playerScore++;
            ResetBall();
			playerScoredLast = true;
        }

        if (collision.gameObject.name == "Player")
        {
            if (transform.position.y <= collision.transform.position.y - .3)
            {
				GetComponent<Rigidbody>().velocity = playerStartingVectors[3];
            }
            if (transform.position.y >= collision.transform.position.y + .3)
            {
				GetComponent<Rigidbody>().velocity = playerStartingVectors[2];
            }

        }
			
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cvel = GetComponent<Rigidbody>().velocity;
        Vector3 tvel = cvel.normalized * cSpeed;
        GetComponent<Rigidbody>().velocity = Vector3.Lerp(cvel, tvel, Time.deltaTime * sFactor);



        //Check the top bounds
        if (transform.position.y > 8 || transform.position.y < -8)
        {
            Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().velocity.Set(currentVelocity.x, -currentVelocity.y, currentVelocity.z);
        }

		if (transform.position.y > 8.1 || transform.position.y < -8.1 )
		{
			ResetBall();
		}

		var somesthing = GetComponent<Rigidbody>();

		if (GetComponent<Rigidbody>().velocity.x < 1 && GetComponent<Rigidbody>().velocity.x > 0 )
		{
			Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
			GetComponent<Rigidbody>().velocity.Set(currentVelocity.x + 1, currentVelocity.y - 1, currentVelocity.z);
		}
		else if  (GetComponent<Rigidbody>().velocity.x > -1 && GetComponent<Rigidbody>().velocity.x <= 0 )
		{
			Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
			GetComponent<Rigidbody>().velocity.Set(currentVelocity.x - 1, currentVelocity.y + 1, currentVelocity.z);
		}

	

    }
}
