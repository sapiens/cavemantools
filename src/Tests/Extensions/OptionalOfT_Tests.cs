using System;
using CavemanTools;
using FluentAssertions;
using Xunit;

namespace Tests.Extensions
{
    public class OptionalOfT_Tests
    {
        [Fact]
        public void implicit_from_value()
        {
            Optional<PasswordHash> hash=new PasswordHash("test");
            hash.HasValue.Should().BeTrue();
        }
        
        [Fact]
        public void implicit_to_value()
        {
            PasswordHash hash=new Optional<PasswordHash>(new PasswordHash("test"));
            hash.Should().NotBeNull();
        }

        [Fact]
        public void empty_optional_implicit_to_value_throws()
        {
            Optional<PasswordHash> hash = Optional<PasswordHash>.Empty;
            Assert.Throws<InvalidOperationException>(()=>
            {
                PasswordHash h = hash;
            });            
        }
    }
}