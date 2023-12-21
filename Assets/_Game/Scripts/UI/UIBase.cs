using UnityEngine;

public class UIBase<T> : Singleton<T> where T : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private bool _preventClosing;

    public bool PreventClosing { get => _preventClosing; set => _preventClosing = value; }
    public CanvasGroup CanvasGroup => _canvasGroup;

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }
}
