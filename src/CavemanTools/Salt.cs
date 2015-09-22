using System;
using System.Security.Cryptography;

namespace CavemanTools
{
    public class Salt:IEquatable<Salt>
    {
        private readonly byte[] _bytes;

        public byte[] Bytes
        {
            get { return _bytes; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length">Length must be at least 8 bytes</param>
        /// <returns></returns>
        public static Salt Generate(int length = 32)
        {
            length.MustComplyWith(d => d >= 8, "Salt length must be at least 8 bytes");
            var bytes = new byte[length];
            RandomNumberGenerator.Create().GetNonZeroBytes(bytes);
            return new Salt(bytes);
        }

        public Salt(byte[] bytes)
        {
            bytes.MustNotBeNull();
            _bytes = bytes;            
        }

        public int Length
        {
            get { return _bytes.Length; }
        }

        public bool Equals(Salt other)
        {
            if (other == null) return false;
            return _bytes.IsEqual(other._bytes);
        }

        public override bool Equals(object obj)
        {
            if (obj is Salt) return Equals((Salt) obj);
            return false;
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return Convert.ToBase64String(_bytes);
        }
    }
}