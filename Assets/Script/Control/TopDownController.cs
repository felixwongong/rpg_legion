using UnityEngine;

namespace CofyDev.RpgLegend
{
    public class TopDownController : MonoBehaviour
    {
        [SerializeField] 
        private InputReceiver _receiver;

        private void Awake()
        {
            _receiver ??= GetComponent<InputReceiver>();
        }

        private void Start()
        {
            _receiver.onVec2Updated += OnMove;
        }

        private void OnMove(Vector2 value)
        {
            FLog.LogObject(value);
        }
    }
}