using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SwitchPanelButton : MonoBehaviour
{
    [SerializeField] private GameObject _panelToOpen;
    [SerializeField] private GameObject _panelToClose;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnOpenButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnOpenButtonClick);
    }

    private void OnOpenButtonClick()
    {
        _panelToClose.SetActive(false);
        _panelToOpen.SetActive(true);
    }
}
