using UnityEngine;

namespace BreakoutBall
{
    public class Wall : MonoBehaviour
    {
        public Material _hitOnceMaterial;
        public Material _hitTwiceMaterial;
        public Material _hitThriceMaterial;
        public GameObject _explosion;

        private int _numTimesHit = 0;
        private Renderer _renderer;

        private void Start()
        {
            _renderer = this.GetComponent<Renderer>();
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
                    _renderer.material = _hitOnceMaterial;
                    _numTimesHit++;
                } else if(_numTimesHit == 1) {
                    _renderer.material = _hitTwiceMaterial;
                    _numTimesHit++;
                }
                else if (_numTimesHit == 2)
                {
                    _renderer.material = _hitThriceMaterial;
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