using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Image _iconItem;
    [SerializeField] private TextMeshProUGUI _nameItem;
    [SerializeField] private TextMeshProUGUI _levelUpgradeItem;
    [SerializeField] private TextMeshProUGUI _abilityLevelItem;
    [SerializeField] private Button _button;
    private UIProcess _uIProcess;
    private int _index;

    private void OnEnable()
    {
        _button.onClick.AddListener(ClickThisItem);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ClickThisItem);
    }

    private void ClickThisItem()
    {
        _uIProcess.SetChangeItem(_index);
    }

    public void SetData(int id, ItemInfo info, UIProcess uI)
    {
        _index = id;
        _iconItem.sprite = info.Icon;
        _nameItem.text = info.AbilityName;
        _levelUpgradeItem.text = info.CurrentLevelUpgrade.ToString();
        _abilityLevelItem.text = info.AbilityLevel.ToString();
        _uIProcess = uI;
    }
}
