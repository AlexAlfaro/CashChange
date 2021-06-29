namespace CashChange
{
    public interface IPayment
    {
        IDenomination Denomination { get; set; }
        int Number { get; set; }
    }
}