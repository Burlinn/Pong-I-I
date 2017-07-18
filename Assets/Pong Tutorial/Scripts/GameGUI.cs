using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

    public TextMesh pScore;
    public TextMesh eScore;
    public TextMesh gameMessageText;
    public Ball ball;
    public void OnGUI()
    {

        pScore.text = ball.playerScore.ToString();
        eScore.text = ball.enemyScore.ToString();

        if(pScore.text == "10")
        {
            gameMessageText.text = "YOU WIN";
            StopGame();
        }
        if (eScore.text == "10")
        {
            gameMessageText.text = "YOU LOSE";
            StopGame();
        }
    }
    private void StopGame()
    {
        Destroy(ball.gameObject);
    }
}
