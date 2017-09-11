using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float cSpeed = 5;
    public float sFactor = 5;

    //Two variables to hold our scores
    public int playerScore;
    public int enemyScore;
    public float roundStartDelay = 0f;
    public float gameOverDelay = 3f;
    public float roundOverDelay = 0f;
    Vector3[] playerStartingVectors = new Vector3[4];
	Vector3[] enemyStartingVectors = new Vector3[4];
    public int ballSpawnFromPlayer;
    public int ballSpawnFromEnemy;
    public int scoreToWin;
	public bool playerScoredLast = true;
    public int startingScore;
    public BallManager ballManager;
    public GameObject ball;
    public Text gameMessageText;
    public Text playerScoreText;
    public Text enemyScoreText;
    

    private WaitForSeconds gameOverWait;
    private WaitForSeconds roundStartWait;

    // Use this for initialization
    public void Start()
    {
        gameOverWait = new WaitForSeconds(gameOverDelay);
        roundStartWait = new WaitForSeconds(roundStartDelay);


        gameMessageText.text = "";
        playerScore = startingScore;
        enemyScore = startingScore;
        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text = enemyScore.ToString();
        ballManager.ballInstance = ball;

        playerStartingVectors[0] = new Vector3(3, 2, 0);
		playerStartingVectors[1] = new Vector3(3, -2, 0);
		playerStartingVectors[2] = new Vector3(4, 3, 0);
		playerStartingVectors[3] = new Vector3(4, -3, 0);
		enemyStartingVectors[0] = new Vector3(-3, 2, 0);
		enemyStartingVectors[1] = new Vector3(-3, -2, 0);
		enemyStartingVectors[2] = new Vector3(-4, 3, 0);
		enemyStartingVectors[3] = new Vector3(-4, -3, 0);
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (GameHasWinner())
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        ResetBall();
        yield return null;
    }

    private IEnumerator RoundPlaying()
    {
        while (BallInPlay())
        {
            yield return null;
        }

    }

    private IEnumerator RoundEnding()
    {

        if (ballManager.playerScoredLast && ballManager.roundHadWinner)
        {
            playerScore++;
            playerScoreText.text = playerScore.ToString();
        }
        else if(ballManager.roundHadWinner)
        {
            enemyScore++;
            enemyScoreText.text = enemyScore.ToString();
        }

        ballManager.roundHadWinner = false;

        if (enemyScore == scoreToWin)
        {
            gameMessageText.text = "You Lose!";
            yield return gameOverWait;
        }
        else if(playerScore == scoreToWin)
        {
            gameMessageText.text = "You Win!";
            yield return gameOverWait;
        }
        else
        {
            yield return roundOverDelay;
        }

        
    }


    private bool BallInPlay()
    {
        return ballManager.ballInstance.activeSelf;
    }

    private bool GameHasWinner()
    {
        if(playerScore == 10 || enemyScore == 10)
        {
            return true;
        }
        return false;
    }

    public void ResetBall()
    {

        ballManager.ballInstance.SetActive(true);

        Vector3 startPosition = ballManager.ballInstance.GetComponent<Rigidbody>().position;
		Vector3 startingVector = new Vector3 ();

		if (playerScoredLast == true) {
			startingVector = playerStartingVectors[Random.Range(0, 4)];
            startPosition.x = ballSpawnFromPlayer;
        }

		if (playerScoredLast == false) {
			startingVector = enemyStartingVectors[Random.Range(0, 4)];
            startPosition.x = ballSpawnFromEnemy;
        }

		ballManager.ballInstance.GetComponent<Rigidbody>().velocity = startingVector;
            
        startPosition.y = 0;
        ballManager.ballInstance.GetComponent<Rigidbody>().position = startPosition;
        

    }

    
}
