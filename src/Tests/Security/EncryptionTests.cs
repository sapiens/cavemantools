using System;
using System.Security.Cryptography;
using FluentAssertions;
using Xunit;

namespace Tests.Security
{
    public class EncryptionTests
    {
        public EncryptionTests()
        {

        }

        const string Key = "123456";

        [Fact]
        public void encrypt()
        {
            var test = "some";
            var enc = test.Encrypt(Key);
            var dec = enc.DecryptAsString(Key);
            test.Should().Be(dec);
            
            enc.Invoking(d =>dec= enc.DecryptAsString("434")).ShouldThrow<CryptographicException>();
          
        }

    }
}