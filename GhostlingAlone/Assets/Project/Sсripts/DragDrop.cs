using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//----------------  ������ ��� �������������� ��������� � ��������� ----------------//

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Transform inventory;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    private void Awake()
    {
        // �������� ������ �� ���������� RectTransform � CanvasGroup �������
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        Debug.Log("OnBeginDrag");

        // ������������� ������������ �������, ����� �� �������� �������������� �� ����� ��������������
        canvasGroup.alpha = .6f;

        // ��������� ����������� ������ �������, ������� ���������������
        canvasGroup.blocksRaycasts = false;

        // ��������� ��������� ������� � ������������ ������ ����� ������������
        startPosition = transform.position;
        startParent = transform.parent;

        // ��������� ������ � �������� ������ ����� ��� ���������� ���������
        transform.SetParent(transform.root);

        // ������������� ������ ��� "���������������"
        itemBeingDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ���������� ������ ������ � ����� � ������ ��������� ������� ���������
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        // ������� ������ �� ������, ������� ���������������
        itemBeingDragged = null;

        // ���������, �������� �� ������ � �������� ��������� ��� ������������ ������
        if (transform.parent == startParent || transform.parent == transform.root)
        {
            // ���������� ������ �� ��������� ������� � � ��������� ��������
            transform.position = startPosition;
            transform.SetParent(startParent);
        }

        //Debug.Log("OnEndDrag");

        // ��������������� ������������ � ����������� ������ �������
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
