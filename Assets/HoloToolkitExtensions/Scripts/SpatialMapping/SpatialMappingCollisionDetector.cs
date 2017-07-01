using UnityEngine;

namespace HoloToolkitExtensions.SpatialMapping
{
    public class SpatialMappingCollisionDetector : BaseSpatialMappingCollisionDetector
    {
        public float MinDistance = 0.0f;

        public void Awake()
        {
            var r = Rigid_Body;
        }

        private Rigidbody Rigid_Body
        {
            get
            {
                var rigidBody = GetComponent<Rigidbody>();
                if (rigidBody == null)
                {
                    rigidBody = gameObject.AddComponent<Rigidbody>();
                }
                rigidBody.isKinematic = true;
                rigidBody.useGravity = false;
                return rigidBody;
            }

        }


        public override bool CheckIfCanMoveBy(Vector3 delta)
        {
            RaycastHit hitInfo;
            // Sweeptest wisdom from 
            // http://answers.unity3d.com/questions/499013/cubecasting.html
            return !Rigid_Body.SweepTest(delta, out hitInfo, delta.magnitude);
        }

        public override Vector3 GetMaxDelta(Vector3 delta)
        {
            RaycastHit hitInfo;
            if(!Rigid_Body.SweepTest(delta, out hitInfo, delta.magnitude))
            {
                return KeepDistance(delta, hitInfo.point);
            }

            delta *= (hitInfo.distance / delta.magnitude);
            for (var i = 0; i <= 9; i += 3)
            {
                var dTest = delta / (i + 1);
                if (!Rigid_Body.SweepTest(dTest, out hitInfo, dTest.magnitude))
                {
                    return KeepDistance(dTest, hitInfo.point);
                }
            }
            return Vector3.zero;
        }

        private  Vector3 KeepDistance(Vector3 delta, Vector3 hitPoint)
        {
            var distanceVector = hitPoint - transform.position;
            return delta - (distanceVector.normalized * MinDistance);
        }
    }
}


