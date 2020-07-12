using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Missile
{
    public class CanvasManager : MonoBehaviour
    {

        public Text pScore;
        public Text eScore;
        public Text gameMessageText;
        public BallManager ball;
        public void OnGUI()
        {

        }
        private void StopGame()
        {
            Destroy(ball.gameObject);
        }
    }
}