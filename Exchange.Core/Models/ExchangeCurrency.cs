using System;

namespace Exchange.Core.Models
{
    public class ExchangeCurrency : IComparable<ExchangeCurrency>
    {
        public string Exchange { get; set; }
        public string Currency { get; set; }

        public int CompareTo(ExchangeCurrency other)
        {
            if (other == null && this == null) return 0;
            if (other == null && this != null) return +1;
            if (other != null && this == null) return -1;
            return Exchange.CompareTo(other.Exchange) * Currency.CompareTo(other.Currency);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Exchange != null ? Exchange.GetHashCode() : 0) ^ (Currency != null ? Currency.GetHashCode() : 0);
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ExchangeCurrency))
                return false;
            var excc = (ExchangeCurrency)obj;
            return this.Exchange == excc.Exchange && this.Currency == excc.Currency;
        }

        public override string ToString()
        {
            return $"({Exchange}, {Currency})";
        }
    }
}
