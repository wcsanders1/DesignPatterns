namespace Command.Commands
{
    public class AbstractCommand
    {
        protected virtual int LeftBuffer {get;} = 25;
        protected virtual string ItemField { get; } = "Item";
        protected virtual string QuantityField { get; } = "Quantity";
        protected virtual string PriceField { get; } = "Price";
        protected virtual string TotalField { get; } = "Total Cost";
    }
}
