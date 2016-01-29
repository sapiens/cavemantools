using System;
using System.Security.Cryptography;

namespace CavemanTools
{
    public class Salt:IEquatable<Salt>
    {
        private readonly byte[] _bytes;
        const int MinSize = 8;
        public byte[] Bytes => _bytes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length">For security purposes length must be at least 8 bytes</param>
        /// <returns></returns>
        public static Salt Generate(int length = 32)
        {
            length.Must(d => d >= MinSize, "Salt length must be at least 8 bytes");
            var bytes = new byte[length];
#if COREFX
            RandomNumberGenerator.Create().GetBytes(bytes);
#else
            RandomNumberGenerator.Create().GetNonZeroBytes(bytes);
#endif
            return new Salt(bytes);
        }

        public Salt(byte[] bytes)
        {
            bytes.MustNotBeNull();
            _bytes = bytes;            
        }

        public int Length => _bytes.Length;

        public bool Equals(Salt other)
        {
            return other != null && _bytes.IsEqual(other._bytes);
        }

        public override bool Equals(object obj)
        {
            var salt = obj as Salt;
            return salt != null && Equals(salt);
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString() => Convert.ToBase64String(_bytes);
    }
}