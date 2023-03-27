using DependencyInjector;
using UnityEngine;

namespace Graphics
{
    public interface IIconsService : IService
    {
        public Sprite GetIcon(string name);
        public Sprite GetSlotIcon(int slotIndex);
    }
}