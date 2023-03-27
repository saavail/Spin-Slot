using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace UISystem.Core
{
    [CreateAssetMenu(menuName = "Scriptables/" + nameof(PopupsData), fileName = nameof(PopupsData))]
    public class PopupsData : ScriptableObject
    {
        [SerializeField]
        private Popup[] _popups;

        public Popup[] Popups => _popups;

        [Button(ButtonSizes.Medium)]
        public void CollectPopups()
        {
            _popups = AssetDatabase.GetAllAssetPaths()
                .Where(i => i.Contains(".prefab"))
                .Select(path => AssetDatabase.LoadMainAssetAtPath(path) as GameObject)
                .Where(i => i != null && i.GetComponent<Popup>() != null)
                .Select(i => i.GetComponent<Popup>())
                .ToArray();
            
            AssetDatabase.SaveAssets();
        }
    }
}