using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace EntryPoint
{
    public class Game : AsyncInitializableAndLoad<GameSettingsData>, IGameService
    {
        protected override string LoadPath => nameof(GameSettingsData);

        public Game(IResourceLoader resourceLoader)
            : base(resourceLoader) { }
    }
}