using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Invisiball
{
    public class CanvasManager : MonoBehaviour
    {

        public Text pScore;
        public Text eScore;
        public Text gameMessageText;
        public BallManager ball;
        public void OnGUI()
        {

            //pScore.text = ball.playerScore.ToString();
            //eScore.text = ball.enemyScore.ToString();

            //if(pScore.text == "10")
            //{
            //    gameMessageText.text = "YOU WIN";
            //    StopGame();
            //}
            //if (eScore.text == "10")
            //{
            //    gameMessageText.text = "YOU LOSE";
            //    StopGame();
            //}
        }
        private void StopGame()
        {
            Destroy(ball.gameObject);
        }
    }
}