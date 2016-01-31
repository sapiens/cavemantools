using System;
using CavemanTools;
using FluentAssertions;
using Xunit;

namespace Tests.Security
{
    public class PasswordHashTests
    {
        private const string Password = "bla 123";


        [Fact]
        public void equatable_test()
        {
            var salt = Salt.Generate();
            var p1 = new PasswordHash(Password, salt);
            var p2 = new PasswordHash(Password, salt);
            var p3 = new PasswordHash(Password, Salt.Generate());
            p1.Equals(p2).Should().BeTrue();
            p1.Equals(p3).Should().BeFalse();
        }

        [Fact]
        public void valid_password()
        {
            var sut = new PasswordHash(Password,Salt.Generate());
            sut.IsValidPassword(Password).Should().BeTrue();
            sut.IsValidPassword(Password + "f").Should().BeFalse();
            Console.WriteLine(sut.ToString());
            Console.Write(sut.ToString().Length);
        }

        [Fact]
        public void valid_pwd_existing_hash()
        {
            var hash = new PasswordHash(Password,Salt.Generate()).ToString();
            var sut = PasswordHash.FromHash(hash);
            sut.IsValidPassword(Password).Should().BeTrue();
            sut.IsValidPassword("-" + Password).Should().BeFalse();
        }

        [Fact]
        public void different_salts_generate_different_hashes()
        {
            var hash1 = new PasswordHash(Password,Salt.Generate());
            var hash2 = new PasswordHash(Password,Salt.Generate());
            hash1.ToString().Should().NotBe(hash2.ToString());
        }

        [Fact]
        public void hash_from_array()
        {
            var hash = new PasswordHash(Password, Salt.Generate());
            var bytes = hash.Hash;
            
            var hash2 = new PasswordHash(bytes);
            hash2.Should().Be(hash);
            hash.ToString().ToConsole();
            hash2.ToString().ToConsole();

        }
    }
}