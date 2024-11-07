using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WalletView : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Wallet _wallet;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Init(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.BalanceChanged += UpdateText;
    }

    private void OnDisable()
    {
        _wallet.BalanceChanged -= UpdateText;
    }

    private void UpdateText(uint money)
    {
        _text.text = money.ToString();
    }
}
