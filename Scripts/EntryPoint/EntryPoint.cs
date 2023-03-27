using System;
using System.Collections.Generic;
using System.Linq;
using Backend;
using Balance;
using Cysharp.Threading.Tasks;
using DependencyInjector;
using Graphics;
using UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.Pool;

namespace EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        private const string MainSceneName = "MainScene";
        private const float FakeDelayDuration = 3f;
        
        [SerializeField]
        private SplashScreen _splashScreen;
        
        private async void Awake()
        {
            Input.multiTouchEnabled = false;
            Physics.autoSimulation = false;
            Application.targetFrameRate = 60;
            
            _splashScreen.Show();

            List<IAsyncInitializable> asyncInitializables = new();

            var resourceLoader = AllServices.Container.RegisterSingle<IResourceLoader>(new ResourceLoader());

            var gameSettingsLoader = new GameSettingsLoader(resourceLoader);
            await gameSettingsLoader.InitializeAsync();

            var logger = AllServices.Container.RegisterSingle<IGameLoggerService>(new GameLogger(gameSettingsLoader.Data.LogType));
            var sceneLoader = AllServices.Container.RegisterSingle<ISceneLoaderService>(new SceneLoader(logger));

            RegisterInitializableService<Pool, IPoolService>(asyncInitializables, new Pool());
            RegisterInitializableService<LocalBalance, IBalanceService>(asyncInitializables, new LocalBalance(resourceLoader));
            RegisterInitializableService<IconManager, IIconsService>(asyncInitializables, new IconManager(resourceLoader));
            RegisterInitializableService<FakeBackend, IBackendService>(asyncInitializables, new FakeBackend(resourceLoader, logger));
            // RegisterInitializableService(asyncInitializables, new Game(resourceLoader));
            
            await UniTask.WhenAll(asyncInitializables.Select(i => i.InitializeAsync()));
            logger.Log(LogType.Develop, $"Entry point initialized {Time.realtimeSinceStartup}");

            await UniTask.Delay(TimeSpan.FromSeconds(FakeDelayDuration));
            logger.Log(LogType.Develop, $"Fake delay for loading finished {Time.realtimeSinceStartup}");

            await sceneLoader.LoadSceneAsync(MainSceneName);

            InitializeScene();
            
            _splashScreen.Hide();
        }

        private static void InitializeScene()
        {
            foreach (GameObject rootGameObject in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (rootGameObject.TryGetComponent<UICore>(out var uiCore))
                {
                    uiCore.Initialize(
                        AllServices.Container.Single<IIconsService>(), 
                        AllServices.Container.Single<IBalanceService>(),
                        AllServices.Container.Single<IBackendService>(),
                        AllServices.Container.Single<IPoolService>());
                    
                    break;
                }
            }
        }

        private static void RegisterInitializableService<TService, TRegisterService>(List<IAsyncInitializable> asyncInitializables, TService service)
            where TRegisterService : IService
            where TService : TRegisterService, IAsyncInitializable
        {
            asyncInitializables.Add(service);
            AllServices.Container.RegisterSingle<TRegisterService>(service);
        }
    }
}