using UnityEngine;

public class PlayerBootstraper : CharacterBootstraper
{
    [SerializeField] private Collector _collector;
    [SerializeField] private WalletView _walletView;

    private Wallet _wallet;

    protected override void Awake()
    {
        base.Awake();
        _wallet = new Wallet();
        _collector.InitWallet(_wallet);
        _walletView.Init(_wallet);
    }
}
