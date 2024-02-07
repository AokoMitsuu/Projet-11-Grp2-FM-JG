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

    private TraitController _currentTraitController;
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform.parent, cursorPosition, null, out Vector2 worldPosition);
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
            if (result.gameObject.TryGetComponent(out T component))
                return component;
        }
        return null;
    }

    public void OnPointerDown()
    {
        var traitController = GetUIHoverObject<TraitController>();
        if (traitController != null)
        {
            _currentTraitController = traitController;

            _traitText.text = _currentTraitController.Trait.Name;
            SetActive(true);
            if (!traitController.IsInfiniteSource)
            {
                traitController.IsInteractable = false;
            }
        }
    }

    public void OnPointerUp()
    {
        if (!_currentTraitController) return;

        if (!_currentTraitController.IsInfiniteSource) _currentTraitController.IsInteractable = true;

        var profile = GetUIHoverObject<ProfileController>();

        
        if (!profile) // if in void
        {
            if (!_currentTraitController.IsInfiniteSource)
            {
                _currentTraitController.LinkedNpc.RemoveTrait(_currentTraitController.Trait);
                Destroy(_currentTraitController.gameObject);
            }
        }
        else // if in list
        {
            if (!_currentTraitController.IsInfiniteSource && profile.Npc == _currentTraitController.LinkedNpc)
            {
                StopDrag();
                return;
            }

            bool success = profile.Npc.AddTrait(_currentTraitController.Trait);

            // if trait successfully added
            if (success && !_currentTraitController.IsInfiniteSource)
            {
                _currentTraitController.LinkedNpc.RemoveTrait(_currentTraitController.Trait);
                Destroy(_currentTraitController.gameObject);
            }
        }

        StopDrag();
    }

    public void StopDrag()
    {
        _currentTraitController = null;
        SetActive(false);
    }

    private void SetActive(bool isActive)
    {
        _backgroundUI.color = isActive ? _baseColor : Color.clear;
        _traitText.gameObject.SetActive(isActive);
    }
}
