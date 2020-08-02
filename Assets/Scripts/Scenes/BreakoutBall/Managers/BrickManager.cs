using UnityEngine;

namespace BreakoutBall
{
    public class BrickManager : MonoBehaviour
    {

        public int _value = 1;

        public Material _normalMaterial;
        public Material _strongMaterial;
        public Material _crackedMaterial;
        public GameObject _explosion;

        private bool _isCracked = false;
        private BreakoutBallManager _scene;

        private void Start()
        {
            _scene = GameObject.Find("SceneManager").GetComponent<BreakoutBallManager>();
            if (_value == 1)
            {
                this.GetComponent<Renderer>().material = _normalMaterial;
            } else if (_value == 2)
            {
                this.GetComponent<Renderer>().material = _strongMaterial;
            }
        }

        void Update()
        {

        }

        void OnCollisionEnter(Collision collision)
        {

            Instantiate(_explosion, this.transform.position, Quaternion.identity);

            ////If we hit a ball, then the ball should shoot off in the direction it was hit into.
            if (collision.gameObject.name == "Ball")
            {
                if(_value == 2 && !_isCracked)
                {
                    this.GetComponent<Renderer>().material = _crackedMaterial;
                    _isCracked = true;
                } else if(_value == 1 || _isCracked == true) { 
                    if (_scene.GetBallLastHitPlayer())
                    {
                        _scene.SetPlayerBreakoutScore(_value);
                    } else
                    {
                        _scene.SetEnemyBreakoutScore(_value);
                    }
                    _scene.RemoveBrick();
                    Destroy(this.gameObject);
                }
            }

        }

    }
}