using System;

namespace CavemanTools
{
    public struct Percentage:IEquatable<Percentage>
    {
        public static readonly Percentage Empty=new Percentage();
        
        private decimal _value;

        /// <summary>
        /// A percentage as a number.
        /// Example: for 3%, it's 3 not .03
        /// </summary>
        /// <param name="value">Percentage amount</param>
        public Percentage(decimal value)
        {
            _value = value/100;
        }

        public decimal Value
        {
            get { return _value * 100; }            
        }

        public decimal ApplyTo(decimal amount)
        {
            return amount * _value;
        }


        public override string ToString()
        {
            return _value.ToString("P");
        }
        
        public string ToString(int digits)
        {
            return _value.ToString("P"+digits);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Percentage other)
        {
            return other._value == _value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType()!=typeof(Percentage)) return false;
            return Equals((Percentage)obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool TryParse(string value,out Percentage p)
        {
            decimal init;
            p=new Percentage();
            if (decimal.TryParse(value,out  init))
            {
                p=new Percentage(init);
                return true;
            }
            return false;
        }

        public static Percentage From(decimal value)
        {
            return new Percentage(value);
        }

        #region operator overload

        public static implicit operator Percentage(decimal d)
        {
            return new Percentage(d);
        }

        public static Percentage operator +(Percentage p1, Percentage p2)
        {
            return new Percentage(p1.Value + p2.Value);
        }

        public static Percentage operator -(Percentage p1, Percentage p2)
        {
            return new Percentage(p1.Value - p2.Value);
        }

        public static bool operator ==(Percentage p1, Percentage p2)
        {
            return p1._value == p2._value;
        }

        public static bool operator !=(Percentage p1, Percentage p2)
        {
            return !(p1 == p2);
        }

        public static bool operator >(Percentage p1, Percentage p2)
        {
            return p1._value > p2._value;
        }

        public static bool operator <(Percentage p1, Percentage p2)
        {
            return p1._value < p2._value;
        } 
        #endregion
    }
}
