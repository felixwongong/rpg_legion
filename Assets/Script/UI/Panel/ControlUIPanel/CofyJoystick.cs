using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace CofyDev.RpgLegend
{
    public class CofyJoystick: OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        //REF
        private RectTransform _parentRect;
        private RectTransform _rectTransform;

        protected override string controlPathInternal { get => m_ControlPath; set => m_ControlPath = value; }

        public float movementRange
        {
            get => m_MovementRange;
            set => m_MovementRange = value;
        }

        //CONFIG
        [SerializeField] private float m_MovementRange = 100;
        [SerializeField] private Vector2 axisSnapZone = Vector2.zero;

        [InputControl(layout = "Vector2"), SerializeField]
        private string m_ControlPath;

        //STATE
        private Vector2 initPos;
        private Vector2 pointerStart_Local;
        private Vector2 pointerEnd_Local;
        private Vector2 pointerEnd_Screen;
        private Vector2 delta;

        private void Awake()
        {
            var tf = transform;
            _parentRect = tf.parent.GetComponentInParent<RectTransform>();
            _rectTransform = (RectTransform) tf;
        }

        private void Start()
        {
            initPos = _rectTransform.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null) throw new ArgumentNullException(nameof(eventData));
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRect, eventData.position,
                eventData.pressEventCamera, out pointerStart_Local);
            _rectTransform.anchoredPosition = pointerStart_Local;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null) throw new ArgumentNullException(nameof(eventData));


            pointerEnd_Screen = eventData.position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _parentRect, pointerEnd_Screen, eventData.pressEventCamera,
                out  pointerEnd_Local);
            
            
            delta = pointerEnd_Local - pointerStart_Local;
            delta = Vector2.ClampMagnitude(delta, movementRange);
            
            _rectTransform.anchoredPosition = initPos + delta;
            
            SendValueToControl(delta);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition = initPos;
            SendValueToControl(Vector2.zero);
        }
    }
}