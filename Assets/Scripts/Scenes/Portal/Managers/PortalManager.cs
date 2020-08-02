using UnityEngine;
using System.Collections;

namespace Portal { 
    public class PortalManager : MonoBehaviour
    {
        public GameObject _pairedPortal;
        public GameObject _explosion;
        public float _timer;
        public float _timeToTeleport = 1f;

        private bool _canTeleport = true;

        public void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _timer += Time.deltaTime;
            //Wait two seconds before teleporting again.
            if (_timer > _timeToTeleport)
            {
                _canTeleport = true;
                _timer = 0;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
			//If the ball touches the portal, teleport the ball to the paired portal and create an explosion at both points.
            if (collision.gameObject.name == "Ball" && _canTeleport)
            {
                collision.gameObject.transform.position = _pairedPortal.transform.position;
                Instantiate(_explosion, this.transform.position, Quaternion.identity);
                Instantiate(_explosion, _pairedPortal.transform.position, Quaternion.identity);
                _canTeleport = false;
                _pairedPortal.GetComponent<PortalManager>().SetCanTeleport(false);
            }

        }

        public void SetCanTeleport(bool canTeleport)
        {
            _canTeleport = false;

        }

    }

}