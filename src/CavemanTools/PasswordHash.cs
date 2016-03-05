using System;
using System.Security.Cryptography;

namespace CavemanTools
{
    public class PasswordHash:IEquatable<PasswordHash>
    {
        public class HashData
        {
            public byte[] Salt;
            public byte[] HashedPassword;
        }

       
        public static Func<HashData, byte[]> PackBytes=PackWithRandomPadding;
        public static Func<byte[],int,HashData> UnpackBytes=UnpackWithRandomPadding;

        public static byte[] PackWithRandomPadding(HashData data)
        {
           
            var result = new byte[data.Salt.Length + data.HashedPassword.Length];
            
            data.Salt.CopyTo(result,0);
            data.HashedPassword.CopyTo(result,data.Salt.Length);
            return result;
        }


        public static HashData UnpackWithRandomPadding(byte[] packed, int saltSize)
        {
            var res=new HashData();
            res.Salt=new byte[saltSize];
            var hashLength = packed.Length-saltSize;
            res.HashedPassword=new byte[hashLength];
            Array.Copy(packed,0,res.Salt,0,saltSize);
            Array.Copy(packed,saltSize,res.HashedPassword,0,hashLength);
            return res;
        }

        private Salt _salt;
        private readonly int _iterations=DefaultIterations;
        private byte[] _finalHash;
        private readonly int _saltSize=DefaultSaltSize;
        private byte[] _pwdHash;

        /// <summary>
        /// Length of the generated hash, it should be at least 32 bytes
        /// </summary>
        public static int KeySize=32;

        /// <summary>
        /// Default value is 32
        /// </summary>
        public static int DefaultSaltSize = 32;
        /// <summary>
        /// Default value is 64000
        /// </summary>
        public static int DefaultIterations = 64000;

        public Salt Salt => _salt;

        /// <summary>
        /// For developing/testing purposes only
        /// </summary>
        /// <returns></returns>
        public static PasswordHash GenerateRandom()
        {
            return new PasswordHash(Guid.NewGuid().ToString(),DefaultIterations,Salt.Generate());
        }

        public static PasswordHash FromHash(string hash,int saltSize,int iterations)
        {
            hash.MustNotBeEmpty();
            return new PasswordHash(Convert.FromBase64String(hash),saltSize,iterations);            
        }

        private PasswordHash()
        {
            
        }

        public PasswordHash(string password):this(password,DefaultSaltSize,DefaultIterations)
        {
            
        }

        /// <summary>
        /// Creates a password hash 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="saltSize">Salt length should be at least 64 bytes</param>
        /// <param name="iterations">At least 64000</param>
        public PasswordHash(string password, int saltSize, int iterations):this(password,iterations,Salt.Generate(saltSize))
        {
            
        }
        
        /// <summary>
        /// Creates a password hash 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="iterations">At least 64000</param>
        /// <param name="salt">Salt length should be at least 64 bytes</param>
        public PasswordHash(string password, Int32 iterations, Salt salt)
        {
            _iterations = iterations;
            _salt = salt;
            _saltSize = _salt.Length;

            _pwdHash = Pbkdf2Hash(password, Salt.Bytes, iterations);

            SetFinalHash();

        }

        /// <summary>
        /// Instantiates from final hash using the DefaultSaltSize and DefaultIterations values
        /// </summary>
        /// <param name="hash"></param>
        public PasswordHash(byte[] hash):this(hash,DefaultSaltSize,DefaultIterations)
        {
            
        }
        public PasswordHash(byte[] hash,int saltSize,int iterations)
        {
            _finalHash = hash;
            _saltSize = saltSize;
            _iterations = iterations;          
            ExtractParts();                        
        }

        public byte[] Hash => _finalHash;

        void ExtractParts()
        {
            var data = UnpackBytes(_finalHash, _saltSize);
            _salt=new Salt(data.Salt);
            _pwdHash = data.HashedPassword;            
        }

        public bool IsValidPassword(string pwd)
        {
            var pwdHash = Pbkdf2Hash(pwd, Salt.Bytes, _iterations);
            return _pwdHash.IsEqual(pwdHash);            
        }

     

        public bool Equals(PasswordHash other) => other != null && _finalHash.IsEqual(other._finalHash);

        public override bool Equals(object obj)
        {
            var hash = obj as PasswordHash;
            return hash != null && Equals(hash);
        }

        public override int GetHashCode() => _finalHash.GetHashCode();

        
        public override string ToString() => Convert.ToBase64String(_finalHash);


        void SetFinalHash()
        {
            var data=new HashData();
            data.Salt = _salt.Bytes;
            data.HashedPassword = _pwdHash;
            _finalHash= PackBytes(data);            
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