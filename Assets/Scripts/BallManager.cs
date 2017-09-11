using UnityEngine;
using System.Collections;


public class BallManager : MonoBehaviour
{

    public float cSpeed = 5;
    public float sFactor = 5;

    //Two variables to hold our scores
	public bool playerScoredLast = true;
    public bool roundHadWinner = false;
    [HideInInspector] public GameObject ballInstance;
    private Vector3 lastUsedStartingVector;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "LeftWall")
        {
			playerScoredLast = false;
            roundHadWinner = true;
            ballInstance.SetActive(false);
        }
        if (collision.gameObject.name == "RightWall")
        {
			playerScoredLast = true;
            roundHadWinner = true;
            ballInstance.SetActive(false);
        }

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
            roundHadWinner = false;
            ballInstance.SetActive(false);
        }

		if (GetComponent<Rigidbody>().velocity.x < 1 && GetComponent<Rigidbody>().velocity.x > 0 )
		{
			Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
			GetComponent<Rigidbody>().velocity.Set(currentVelocity.x + 2, currentVelocity.y - 2, currentVelocity.z);
		}
		else if  (GetComponent<Rigidbody>().velocity.x > -1 && GetComponent<Rigidbody>().velocity.x <= 0 )
		{
			Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
			GetComponent<Rigidbody>().velocity.Set(currentVelocity.x - 2, currentVelocity.y + 2, currentVelocity.z);
		}

	

    }
}
