using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HoloToolkitExtensions.Animation
{
    public class BaseTextScreenController : FadeInOutController
    {
        private List<MonoBehaviour> _allOtherBehaviours;

        // Use this for initialization
        public override void Start()
        {
            base.Start();
            _allOtherBehaviours = GetAllOtherBehaviours();
            SetComponentStatus(false);
        }

        public override void Show()
        {
            if (IsVisible)
            {
                return;
            }
            SetComponentStatus(true);
            var a = GetComponent<AudioSource>();
            if (a != null)
            {
                a.Play();
            }
            base.Show();

        }

        public override void Hide()
        {
            if (!IsVisible)
            {
                return;
            }
            base.Hide();
            StartCoroutine(WaitAndDeactivate());
        }

        IEnumerator WaitAndDeactivate()
        {
            yield return new WaitForSeconds(0.5f);
            SetComponentStatus(false);
        }
        private List<MonoBehaviour> GetAllOtherBehaviours()
        {
            var result = new List<Component>();
            GetComponents(result);
            var behaviors = result.OfType<MonoBehaviour>().Where(p => p != this).ToList();
            GetComponentsInChildren(result);
            behaviors.AddRange(result.OfType<MonoBehaviour>());
            return behaviors;
        }

        private void SetComponentStatus(bool active)
        {
            foreach (var c in _allOtherBehaviours)
            {
                c.enabled = active;
            }
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.gameObject.SetActive(active);
            }
        }
    }
}
