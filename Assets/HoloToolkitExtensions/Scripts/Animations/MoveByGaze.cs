using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkitExtensions.SpatialMapping;
using HoloToolkitExtensions.Utilities;

namespace HoloToolkitExtensions.Animation
{
    public class MoveByGaze : MonoBehaviour
    {
        public float MaxDistance = 2f;

        public float DistanceTrigger = 0.2f;

        public float Speed = 1.0f;

        private float _startTime;
        private float _delay = 0.5f;

        private bool _isJustEnabled;

        private Vector3 _lastMoveToLocation;

        public BaseRayStabilizer Stabilizer = null;

        public BaseSpatialMappingCollisionDetector CollisonDetector;

        // Use this for initialization
        void Start()
        {
            _startTime = Time.time + _delay;
            _isJustEnabled = true;
            if (CollisonDetector == null)
            {
                CollisonDetector = new DefaultMappingCollisionDetector();
            }
        }

        void OnEnable()
        {
            _isJustEnabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            if ( _isBusy || _startTime > Time.time)
                return;

            var newPos = LookingDirectionHelpers.GetPostionInLookingDirection(2.0f, 
                GazeManager.Instance.Stabilizer);
            if ((newPos - _lastMoveToLocation).magnitude > DistanceTrigger || _isJustEnabled)
            {
                _isJustEnabled = false;
                var maxDelta = CollisonDetector.GetMaxDelta(newPos - transform.position);
                if (maxDelta != Vector3.zero)
                {
                    _isBusy = true;
                    newPos = transform.position + maxDelta;
                    LeanTween.moveLocal(gameObject, transform.position + maxDelta, 
                        2.0f * maxDelta.magnitude / Speed).setEaseInOutSine().setOnComplete(MovingDone);
                    _lastMoveToLocation = newPos;
                }
            }
        }

        private void MovingDone()
        {
            _isBusy = false;
        }

        private bool _isBusy;

    }
}
