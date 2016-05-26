using CavemanTools;

namespace System
{
    public static class Numbers
    {
        public static Percentage AsPercentageOf(this decimal part, decimal total)
        {
            total.Must(d => d != 0m, "Can't divide by 0");
            return Math.Round(part * 100 / total, 2);
        }
    }
}