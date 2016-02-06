using System;
using System.Security.Cryptography;

namespace CavemanTools
{
    public class PasswordHash:IEquatable<PasswordHash>
    {
        private Salt _salt;
        private int _iterations;
        private byte[] _finalHash;
        private byte[] _pwdHash;
        private const int KeySize=32;

        public Salt Salt => _salt;

        /// <summary>
        /// For developing/testing purposes only
        /// </summary>
        /// <returns></returns>
        public static PasswordHash GenerateRandom()
        {
            return new PasswordHash(Guid.NewGuid().ToString(),Salt.Generate());
        }

        public static PasswordHash FromHash(string hash)
        {
            hash.MustNotBeEmpty();
            var pwd = new PasswordHash();
            pwd._finalHash= Convert.FromBase64String(hash);   
            pwd.ExtractParts();
            return pwd;
        }

        private PasswordHash()
        {
            
        }

        public PasswordHash(byte[] hash)
        {
            Hash = hash;            
        }

        public byte[] Hash
        {
            get { return _finalHash; }
            private set
            {
                _finalHash = value;
                ExtractParts();
            }
        }

        void ExtractParts()
        {
           _iterations = BitConverter.ToInt32(_finalHash,0);
            _pwdHash=new byte[KeySize];
            Array.Copy(_finalHash,4,_pwdHash,0,KeySize);
            var saltBytes=  new byte[_finalHash.Length-4-KeySize];
            Array.Copy(_finalHash,KeySize+4,saltBytes,0,saltBytes.Length);
            _salt = new Salt(saltBytes);
        }

        public bool IsValidPassword(string pwd)
        {
            var pwdHash = Pbkdf2Hash(pwd, Salt.Bytes, _iterations);
            return _pwdHash.IsEqual(pwdHash);            
        }

        public PasswordHash(string password,Salt salt=null,Int32 iterations=50000)
        {
            _iterations = iterations;
           _salt = salt??Salt.Generate();
           
            _pwdHash = Pbkdf2Hash(password,Salt.Bytes,iterations);

            _finalHash = GetFinalHash(_pwdHash);
            
        }

        public bool Equals(PasswordHash other)
        {
            return other != null && _finalHash.IsEqual(other._finalHash);
        }

        public override bool Equals(object obj)
        {
            var hash = obj as PasswordHash;
            return hash != null && Equals(hash);
        }

        public override int GetHashCode()
        {
            return _finalHash.GetHashCode();
        }

        /// <summary>
        /// Returns the hash as string of 92 chars
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Convert.ToBase64String(_finalHash);
        }


        byte[] GetFinalHash(byte[] pwdHash)
        {
            byte[] final=new byte[pwdHash.Length+Salt.Length+4];
            BitConverter.GetBytes(_iterations).CopyTo(final,0);
            pwdHash.CopyTo(final,4);
            Salt.Bytes.CopyTo(final,KeySize+4);
            return final;
        }

        static byte[] Pbkdf2Hash(string data,byte[] salt,int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(data, salt, iterations))
            {
                return  pbkdf2.GetBytes(KeySize);
            };
            
        }

     
    }
}