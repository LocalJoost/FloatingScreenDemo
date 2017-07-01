using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HoloToolkitExtensions.Animation
{
    public class CloseButton : MonoBehaviour, IInputClickHandler
    {
        private void Start()
        { }

        void Awake()
        {
            gameObject.SetActive(true);
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            var h = gameObject.GetComponentInParent<BaseTextScreenController>();
            if (h != null)
            {
                h.Hide();
                var a = gameObject.GetComponent<AudioSource>();
                if (a != null)
                {
                    a.Play();
                }
            }
        }
    }
}
