using UnityEngine;
using Utilities.Pool;
using Utilities.UIUtilitiesLineRenderer;

namespace Utilities.PoolCustom
{
    public class UILineRendererHolder : PooledBehaviour
    {
        [SerializeField]
        private UILineRenderer _lineRenderer;
        [SerializeField]
        private RectTransform _rect;

        public UILineRenderer LineRenderer => _lineRenderer;
        public RectTransform Rect => _rect;
    }
}