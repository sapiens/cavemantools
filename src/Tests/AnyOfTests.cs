 
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


       
    }
} 
