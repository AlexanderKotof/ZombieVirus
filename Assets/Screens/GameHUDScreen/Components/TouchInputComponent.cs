using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Screens
{
    public class TouchInputComponent : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public event Action<Vector3> Tap;

        public event Action<Vector3> Draging;

        public bool _isDraging;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDraging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Draging?.Invoke(eventData.delta);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            _isDraging = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isDraging)
                Tap?.Invoke(eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }
    }
}