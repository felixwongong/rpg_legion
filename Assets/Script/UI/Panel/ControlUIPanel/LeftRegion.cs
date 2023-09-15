using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace CofyDev.RpgLegend
{
    public class LeftRegion: MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform joystick;

        private RectTransform regionRect;

        private void Awake()
        {
            regionRect = GetComponent<RectTransform>();
        }

        // public void OnPointerDown(PointerEventData eventData)
        // {
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(regionRect, eventData.position,
        //         eventData.pressEventCamera, out var anchored);
        //     joystick.anchoredPosition = anchored;
        //
        //     var screenControl = joystick.GetComponentInParent<OnScreenControl>();
        // }
        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}