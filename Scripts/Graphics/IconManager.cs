using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using EntryPoint;
using UnityEngine;

namespace Graphics
{
    public class IconManager : AsyncInitializableAndLoad<IconsData>, IIconsService
    {
        public const string EmptyKey = "Empty";
        
        private static readonly Dictionary<int, string> SlotIcons = new()
        {
            {0, "Money"},
            {1, "Diamonds"},
            {2, "Jewelry"},
            {3, "Painting"},
            {4, "Watch"},
            {5, "Cop"},
            {6, "Prison"},
            {7, "Robber"},
            {8, "Handcuffs"},
            {9, "Wild"},
            {10, "Scatter"},
        };

        private Dictionary<string, Sprite> _icons;

        public IconManager(IResourceLoader resourceLoader)
            : base(resourceLoader) { }

        public override async UniTask InitializeAsync()
        {
            await base.InitializeAsync();
            
            _icons = await Data.AllIcons.ToUniTaskAsyncEnumerable().ToDictionaryAsync(i => i.name, i => i);
            _icons.Add(EmptyKey, null);
        }

        public Sprite GetIcon(string name)
        {
            if (!_icons.TryGetValue(name, out var sprite))
            {
                Debug.LogErrorFormat("Icon {0} was not found", name);
            }

            return sprite;
        }

        public Sprite GetSlotIcon(int slotIndex)
            => GetIcon(SlotIcons, slotIndex);

        private Sprite GetIcon<T>(Dictionary<T, string> icons, T type)
        {
            if (!icons.TryGetValue(type, out var itemIconPath))
            {
                Debug.LogException(new Exception($"Not found {type} icon in {nameof(IconManager)}"));
            }

            return GetIcon(itemIconPath);
        }
    }
}