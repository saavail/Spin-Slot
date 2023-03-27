using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using EntryPoint;
using UnityEngine;

namespace UISystem.Core
{
    public sealed class PopupsSystem : AsyncInitializableAndLoad<PopupsData>, IPopupSystemService
    {
        private readonly Transform _popupsRoot;
        private readonly List<Popup> _spawnedPopups = new();

        public PopupsSystem(IResourceLoader resourceLoader, Transform popupsRoot)
            : base(resourceLoader)
        {
            _popupsRoot = popupsRoot;
            
            InitializeAsync().Forget();
        }
        
        public Popup Show<TPopup>()
        {
            Popup popup;
            
            if (!_spawnedPopups.Contains(Data.Popups.FirstOrDefault(i => i.GetType() == typeof(TPopup))))
            {
                popup = Object.Instantiate(Data.Popups.FirstOrDefault(i => i.GetType() == typeof(TPopup)), _popupsRoot);
                popup.Open();
                _spawnedPopups.Add(popup);
            }
            else
            {
                popup = _spawnedPopups.FirstOrDefault(i => i.GetType() == typeof(TPopup));
                _spawnedPopups.Add(popup);
                
                if (popup != null && !popup.IsOpen)
                    popup.Open();
            }

            return popup;
        }
    }
}