using System;

public class Wallet
{
    private uint _money = 0;

    public event Action<uint> BalanceChanged;

    public void AddCoin()
    {
        _money++;
        BalanceChanged?.Invoke(_money);
    }
}
