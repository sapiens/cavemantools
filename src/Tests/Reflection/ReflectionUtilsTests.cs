﻿using System;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace Tests.Reflection
{
    public class ReflectionUtilsTests
    {
        private Stopwatch _t = new Stopwatch();

        public ReflectionUtilsTests()
        {

        }

        class MyClass
        {
            public int Id { get; set; }
            public DateTime? Date { get; set; }
            public string Name { get; set; }
        }

        [Fact]
        public void get_property_Value_from_normal_object()
        {
            var c = new MyClass();
            c.Id = 23;
            c.Date=new DateTime(2012,05,03);
            Assert.Equal(23,c.GetPropertyValue("Id"));
            Assert.Equal(new DateTime(2012,05,03),c.GetPropertyValue("Date"));
        }

     

        [Fact]
        public void object_dictionary()
        {
            var mc = new MyClass() {Id = 45, Name = "Test"};
            var d = mc.ValuesToDictionary();
            Assert.Equal(45,d["Id"]);
            Assert.Equal("Test",d["Name"]);
            Assert.Null(d["Date"]);

            mc.Id = 23;

            var d2 = mc.ValuesToDictionary();
            Assert.Equal(23,d2["Id"]);
        }

        private void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}