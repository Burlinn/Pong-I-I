using UnityEngine;
using System.Collections;

namespace Missile
{
    public class PlayerManager : MonoBehaviour
    {
        public GameObject _missilePrefab;
        public int _speed = 12;
        public float _shotVelocity = 12;
        public float _timeBetweenShots = 1f;
        public float _shotTimer = 2f;
        public GameObject _explosion;
        public float _timeStuck = 4f;
        public float _stuckTimer = 0f;

        private Vector3 _missileSpawnPoint;
        private float _missileSpawnX = -11.8f;
        private float _missileSpawnRotationZ = 270;

        private bool _isShot = false;
        private Missile.MissileBallManager _scene;

        private void Start()
        {
            _scene = GameObject.Find("SceneManager").GetComponent<MissileBallManager>();
        }

        // Update is called once per frame
        void Update()
        {

            //If player can shoot, display Can Shoot message.
            _shotTimer += Time.deltaTime;
            if(_shotTimer > _timeBetweenShots)
            {
                _scene.CanFire(true);
            }
            else
            {
                _scene.CanFire(false);
            }

            //If play has been hit, count down until they can move again. Display hit message.
            if (_isShot)
            {
                _stuckTimer += Time.deltaTime;
                if (_stuckTimer > _timeStuck)
                {
                    _isShot = false;
                    _stuckTimer = 0;
                    _scene.PlayerHit(false);
                }
                _scene.PlayerHit(true);
            }

            //If we can shoot, allow player to move. Otherwise, player is stuck. 
            if (!_isShot) { 
                if (Input.GetButton("UP"))
                {
                    Vector3 vec3 = new Vector3(0, _speed, 0);
                    transform.Translate(vec3 * Time.deltaTime);
                }
                if (Input.GetButton("DOWN"))
                {
                    Vector3 vec3 = new Vector3(0, -_speed, 0);
                    transform.Translate(vec3 * Time.deltaTime);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Spawn a missile, and start the timer until the player can shoot again.
                    if (_shotTimer > _timeBetweenShots)
                    {
                        _missileSpawnPoint = this.transform.position;
                        _missileSpawnPoint.x = _missileSpawnX;
                        GameObject newMissile = Instantiate(_missilePrefab, _missileSpawnPoint, Quaternion.identity);
                        newMissile.transform.Rotate(0, 0, _missileSpawnRotationZ);
                        _shotTimer = 0;
                    }
                }
            }


            //Don't allow the player to move out of bounds
            if (transform.position.y > 6.75)
            {
                Vector3 holdAtTop = transform.position;
                holdAtTop.y = 6.75f;
                transform.position = holdAtTop;
            }
            if (transform.position.y < -6.75)
            {
                Vector3 holdAtBottom = transform.position;
                holdAtBottom.y = -6.75f;
                transform.position = holdAtBottom;
            }
        }

        //Set Is Shot and create a neat explosion.
        public void Shot()
        {
            _isShot = true;
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
        }

    }
}