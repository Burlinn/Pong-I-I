using UnityEngine;

namespace BreakoutBall
{
    public class WallManager : MonoBehaviour
    {
        public Material _hitOnceMaterial;
        public Material _hitTwiceMaterial;
        public Material _hitThriceMaterial;
        public GameObject _explosion;

        private int _numTimesHit = 0;

        private void Start()
        {

        }

        void Update()
        {

        }

        void OnCollisionEnter(Collision collision)
        {

            Instantiate(_explosion, this.transform.position, Quaternion.identity);

            //If the ball hits the wall 3 times, the wall goes away.
            if (collision.gameObject.name == "Ball")
            {
                if(_numTimesHit == 0)
                {
                    this.GetComponent<Renderer>().material = _hitOnceMaterial;
                    _numTimesHit++;
                } else if(_numTimesHit == 1) {
                    this.GetComponent<Renderer>().material = _hitTwiceMaterial;
                    _numTimesHit++;
                }
                else if (_numTimesHit == 2)
                {
                    this.GetComponent<Renderer>().material = _hitThriceMaterial;
                    _numTimesHit++;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }

        }

    }
}