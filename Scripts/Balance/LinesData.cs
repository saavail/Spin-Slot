using UnityEngine;

namespace Balance
{
    [CreateAssetMenu(menuName = "Scriptables/" + nameof(LinesData), fileName = nameof(LinesData))]
    public class LinesData : ScriptableObject
    {
        [SerializeField]
        private LineData[] _lines;

        public LineData[] Lines => _lines;
    }
}