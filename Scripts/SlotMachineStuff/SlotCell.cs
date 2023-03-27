using Graphics;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Pool;

namespace SlotMachineStuff
{
    public class SlotCell : PooledBehaviour
    {
        [SerializeField]
        private Image _mainImage;
        [SerializeField]
        private GameObject _winFrameGroup;

        private IIconsService _iconManager;
        private RectTransform _myRect;

        public int Index { get; private set; }

        public RectTransform Rect
        {
            get
            {
                if (_myRect == null)
                    _myRect = (RectTransform) transform;
                
                return _myRect;
            }
        }

        public float HeightHalf => Rect.rect.height / 2f;

        public void Initialize(IIconsService iconManager)
        {
            _iconManager = iconManager;
        }
        
        public void Setup(int slotIndex)
        {
            Index = slotIndex;
            _mainImage.sprite = _iconManager.GetSlotIcon(slotIndex);
            _mainImage.SetNativeSize();
        }

        public void MoveVertical(float delta)
        {
            Rect.anchoredPosition += Vector2.up * delta;
        }

        public void SetPosition(float y)
        {
            Rect.anchoredPosition = new Vector2(Rect.anchoredPosition.x, y);
        }
        
        public bool IsVisibleFromRect(RectTransform boundRect)
        {
            float height = Rect.rect.height;
            
            float cellDownPos = Rect.anchoredPosition.y - height / 2f;
            float cellUpPos = Rect.anchoredPosition.y + height / 2f;

            return (cellUpPos < boundRect.rect.yMax && cellUpPos > boundRect.rect.yMin)
                   || (cellDownPos < boundRect.rect.yMax && cellDownPos > boundRect.rect.yMin);
        }

        public void ShowAnimateWin()
        {
            _winFrameGroup.SetActive(true);
        }

        public void HideWinAnimation()
        {
            _winFrameGroup.SetActive(false);
        }
    }
}