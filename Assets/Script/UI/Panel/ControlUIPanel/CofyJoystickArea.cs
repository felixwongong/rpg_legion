using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;

namespace CofyDev.RpgLegend
{
    public class CofyJoystickArea: MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        //Ref
        private RectTransform _parentRect;
        private RectTransform _rectTransform;

        //Config

        //State
        private Vector2 pointerStart_Local;
        private Vector2 pointerEnd_Local;
        private Vector2 pointerEnd_Screen;
        private Vector2 delta;
        
        //Action
        private Action<Vector2> _onPointerDown;
        private Action<Vector2> _onPointerDrag;
        private Action _onPointerUp;

        private void Awake()
        {
            var tf = transform;
            _parentRect = tf.parent.GetComponentInParent<RectTransform>();
            _rectTransform = (RectTransform) tf;
        }

        public void registerPointer(
            Action<Vector2> onPointerDown, 
            Action<Vector2> onPointerDrag,
            Action onPointerUp)
        {
            _onPointerDown = onPointerDown;
            _onPointerDrag = onPointerDrag;
            _onPointerUp = onPointerUp;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null) throw new ArgumentNullException(nameof(eventData));
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRect, eventData.position,
                eventData.pressEventCamera, out pointerStart_Local);
            
            _onPointerDown(pointerStart_Local);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null) throw new ArgumentNullException(nameof(eventData));


            pointerEnd_Screen = eventData.position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _parentRect, pointerEnd_Screen, eventData.pressEventCamera,
                out  pointerEnd_Local);
            
            
            delta = pointerEnd_Local - pointerStart_Local;
            
            _onPointerDrag(delta);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _onPointerUp();
        }
    }
}