using Cysharp.Threading.Tasks;
using UnityEngine;

namespace EntryPoint
{
    public abstract class AsyncInitializableAndLoad<TData> : IAsyncInitializable
        where TData : ScriptableObject
    {
        public const string PathPrefixKey = "Data/";
        
        private readonly IResourceLoader _resourceLoader;
        protected virtual string LoadPath => typeof(TData).Name;
        
        public TData Data { get; private set; }

        public AsyncInitializableAndLoad(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }
        
        public virtual async UniTask InitializeAsync()
        {
            Data = await _resourceLoader.LoadAsync(PathPrefixKey + LoadPath) as TData;
        }
    }
}