using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TraitMouseController : MonoBehaviour
{
    public static TraitMouseController Instance { get; private set; }

    [SerializeField] private TMP_Text _traitText;
    [SerializeField] private Image _backgroundUI;

    private TraitSo _actualTrait;
    private Color _baseColor;

    void Awake()
    {
        Instance = this;
        _baseColor = _backgroundUI.color;
        SetActive(false);
    }

    private void Update()
    {
        Vector2 cursorPosition = Input.mousePosition;
        Vector2 worldPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform.parent, cursorPosition, null, out worldPosition);
        transform.localPosition = worldPosition;

        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnPointerUp();
        }
    }

    public T GetUIHoverObject<T>() where T : Component
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent(out T component) 
                return component;
        }
        return null;
    }

    public void OnPointerDown()
    {
        var traitController = GetUIHoverObject<TraitController>();
        if (traitController != null)
        {
            _actualTrait = traitController.Trait;
            _traitText.text = _actualTrait.Name;
            SetActive(true);
        }
    }

    public void OnPointerUp()
    {
        var profile = GetUIHoverObject<ProfileController>();
        if (_actualTrait != null && profile != null)
        {
            profile.AddTrait(_actualTrait);
        }
        _actualTrait = null;

        SetActive(false);
    }

    private void SetActive(bool isActive)
    {
        _backgroundUI.color = isActive ? _baseColor : Color.clear;
        _traitText.gameObject.SetActive(isActive);
    }
}
