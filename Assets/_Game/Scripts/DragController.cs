using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragController : Singleton<DragController>
{
    private bool _isDragActive = false;
    private Vector2 _screenPos;
    private Vector3 _worldPos;
    private Draggable _lastDragged;

    public Draggable LastDragged { get => _lastDragged; set => _lastDragged = value; }
    [SerializeField] GraphicRaycaster m_Raycaster;
    [SerializeField] EventSystem m_EventSystem;
    PointerEventData m_PointerEventData;

    private void Update()
    {
        if (_isDragActive)
        {
            if (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Drop();
                return;
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseButton = Input.mousePosition;
            _screenPos = new Vector2(mouseButton.x, mouseButton.y);
        }
        else if (Input.touchCount > 0)
        {
            _screenPos = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }
        _worldPos = Camera.main.ScreenToWorldPoint(_screenPos);
        if (_isDragActive)
        {
            Drag();
        }
        else
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);
            foreach (RaycastResult result in results)
            {
                Draggable draggable = result.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {
                    _lastDragged = draggable;
                    InitDrag();
                    break;
                }
            }

        }
    }
    private void InitDrag()
    {
        _lastDragged.startPos = _lastDragged.transform.position;
        UpdateDragStatus(true);
    }

    private void Drag()
    {
        _lastDragged.transform.position = new Vector2(_worldPos.x, _worldPos.y);
    }

    private void Drop()
    {
        UpdateDragStatus(false);
    }
    void UpdateDragStatus(bool isDragging)
    {
        _isDragActive = _lastDragged.IsDragging = isDragging;
        _lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default;
        _lastDragged.GetComponent<RectTransform>().SetAsLastSibling();
    }
}
