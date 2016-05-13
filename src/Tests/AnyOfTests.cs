 
using FluentAssertions;
using Xunit;
using System;
using CavemanTools;


namespace Tests
{
    public class AnyOfTests
    {

        class Apple
        {
            
        }

        class Orange
        {
            
        }

        

        public AnyOfTests()
        {

        }

        [Fact]
        public void test_type()
        {
            var f=new AnyOf<Apple,Orange>(new Apple());
            f.Is<Apple>().Should().BeTrue();
            f.As<Apple>().Should().NotBeNull();
        }


        [Fact]
        public void optional()
        {
            var opt = new Optional<Apple>();
            opt.IsEmpty.Should().BeTrue();
            opt.ValueOr(new Apple()).Should().NotBeNull();

            var app=new Apple();
            var d=new Optional<Apple>(app);
            d.IsEmpty.Should().BeFalse();
            d.HasValue.Should().BeTrue();
            d.Value.Should().Be(app);
            d.ValueOr(new Apple()).Should().Be(app);
        }
    }
} 
