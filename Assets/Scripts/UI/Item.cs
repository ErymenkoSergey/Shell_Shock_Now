using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Image _iconWeapon;
    [SerializeField] private TextMeshProUGUI _nameWeapon;
    [SerializeField] private TextMeshProUGUI _levelUpgradeWeapon;
    [SerializeField] private TextMeshProUGUI _damageWeapon;
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
        _iconWeapon.sprite = info.Icon;
        _nameWeapon.text = info.Name;
        _levelUpgradeWeapon.text = info.CurrentLevelUpgrade.ToString();
        _damageWeapon.text = info.Damage.ToString();
        _uIProcess = uI;
    }
}
