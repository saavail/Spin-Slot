using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace EntryPoint
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField]
        private Camera _supportCamera;
        [SerializeField]
        private Image _loadingSpinImage;

        public void Show()
        {
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(true);

            _loadingSpinImage.transform.rotation = Quaternion.Euler(0, 0, 0);
            
            _loadingSpinImage.transform.DOLocalRotate(new Vector3(0, 0, -360), 3f, RotateMode.LocalAxisAdd)
                .SetLoops(-1)
                .SetEase(Ease.Linear);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
            _loadingSpinImage.transform.DOKill();
        }
    }
}