using UnityEngine;

namespace HoloToolkitExtensions.Animation
{
    public class FadeInOutController : MonoBehaviour
    {
        public float FadeTime = 0.5f;

        protected bool IsVisible { get; private set; }

        private bool _isBusy;

        public virtual void Start()
        {
            Fade(false, 0);
        }

        private void Fade(bool fadeIn, float time)
        {
            if (!_isBusy)
            {
                _isBusy = true;
                LeanTween.alpha(gameObject, fadeIn ? 1 : 0, time).setOnComplete(() 
                    => _isBusy = false);
            }
        }

        public virtual void Show()
        {
            IsVisible = true;
            Fade(true, FadeTime);
        }

        public virtual void Hide()
        {
            IsVisible = false;
            Fade(false, FadeTime);
        }
    }
}
