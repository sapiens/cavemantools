using System;
using CavemanTools;
using FluentAssertions;
using Xunit;

namespace Tests.Security
{
    public class PasswordHashTests
    {
        private Salt _salt;
        private PasswordHash _sut;
        private const string Password = "bla 123";

        public PasswordHashTests()
        {
            _salt = Salt.Generate(PasswordHash.DefaultSaltSize);
            _sut = new PasswordHash(Password,PasswordHash.DefaultIterations,_salt);
        }

        [Fact]
        public void equatable_test()
        {
            var p1 = new PasswordHash(Password,PasswordHash.DefaultIterations ,_salt);
            var p3 = new PasswordHash(Password, PasswordHash.DefaultIterations, Salt.Generate(PasswordHash.DefaultSaltSize));
            p1.Equals(_sut).Should().BeTrue();
            p1.Equals(p3).Should().BeFalse();
        }

        [Fact]
        public void valid_password()
        {
            _sut.IsValidPassword(Password).Should().BeTrue();
            _sut.IsValidPassword(Password + "f").Should().BeFalse();

        }

        [Fact]
        public void valid_pwd_existing_hash()
        {
            var sut = PasswordHash.FromHash(_sut.ToString(), _sut.Salt.Length, PasswordHash.DefaultIterations);
            sut.IsValidPassword(Password).Should().BeTrue();
            sut.IsValidPassword("-" + Password).Should().BeFalse();
        }

        [Fact]
        public void different_salts_generate_different_hashes()
        {
            var hash2 = new PasswordHash(Password, PasswordHash.DefaultSaltSize,PasswordHash.DefaultIterations);
            _sut.ToString().Should().NotBe(hash2.ToString());
        }

        [Fact]
        public void hash_from_array()
        {
            var hash2 = new PasswordHash(_sut.Hash, _sut.Salt.Length,PasswordHash.DefaultIterations);
            hash2.Hash.Should().BeEquivalentTo(_sut.Hash);
            hash2.IsValidPassword(Password).Should().BeTrue();


        }
    }
}