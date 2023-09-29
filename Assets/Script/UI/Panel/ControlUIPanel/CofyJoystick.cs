using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace CofyDev.RpgLegend
{
    public class CofyJoystick : OnScreenControl
    {
        //Ref
        [SerializeField] private CofyJoystickArea _area;
        [SerializeField] private RectTransform _knob;
        public float movementRange
        {
            get => m_MovementRange;
            set => m_MovementRange = value;
        }
        
        //Config
        [InputControl(layout = "Vector2"), SerializeField]
        private string m_ControlPath;
        [SerializeField] private float m_MovementRange = 100;
        [SerializeField] private Vector2 axisSnapZone = Vector2.zero;
        
        //State
        private RectTransform _rect;
        private Vector2 _initPos;
        
        //OnScreenControl
        protected override string controlPathInternal { get => m_ControlPath; set => m_ControlPath = value; }

        private void Awake()
        {
            if (!_area)
            {
                FLog.LogWarning("Joystick area not set");
                _area = transform.parent.GetComponentInChildren<CofyJoystickArea>();
            }

            _rect = (RectTransform)transform;
        }

        private void Start()
        {
            _area.registerPointer(OnPointerDown, OnPointerDrag, OnPointerUp);

            _initPos = _rect.anchoredPosition;
        }

        private void OnPointerUp()
        {
            _rect.anchoredPosition = _initPos;
            _knob.anchoredPosition = Vector2.zero;
            
            SendValueToControl(Vector2.zero);
        }

        private void OnPointerDrag(Vector2 delta)
        {
            delta = Vector2.ClampMagnitude(delta, movementRange);
            _knob.anchoredPosition = delta;

            delta /= movementRange;

            if (math.abs(delta.x) < axisSnapZone.x) delta.x = 0;
            if (math.abs(delta.y) < axisSnapZone.y) delta.y = 0;

            SendValueToControl(delta);
        }

        private void OnPointerDown(Vector2 location)
        {
            _rect.anchoredPosition = location;
        }
    }
}