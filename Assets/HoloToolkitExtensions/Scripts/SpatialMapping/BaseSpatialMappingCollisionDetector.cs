using UnityEngine;

namespace HoloToolkitExtensions.SpatialMapping
{
    public abstract class BaseSpatialMappingCollisionDetector : MonoBehaviour
    {
        public abstract bool CheckIfCanMoveBy(Vector3 delta);

        public abstract Vector3 GetMaxDelta(Vector3 delta);
    }
}