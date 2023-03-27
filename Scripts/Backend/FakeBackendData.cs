using UnityEngine;

namespace Backend
{
    [CreateAssetMenu(menuName = "Scriptables/" + nameof(FakeBackendData), fileName = nameof(FakeBackendData))]
    public class FakeBackendData : ScriptableObject
    {
        [SerializeField]
        private UserInitResponseData _initResponse;
        [SerializeField]
        private SpinResponseData[] _spinResponses;

        public UserInitResponseData InitResponse => _initResponse;
        public SpinResponseData[] SpinResponses => _spinResponses;
    }
}