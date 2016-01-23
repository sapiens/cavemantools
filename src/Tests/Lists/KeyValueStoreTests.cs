using System;
using System.Collections;
using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace XTests.Lists
{
    public class KeyValueStoreTests
    {
        private Stopwatch _t = new Stopwatch();
        private KeyValueDataStore _kv;

        public KeyValueStoreTests()
        {
            _kv = new KeyValueDataStore();
        }

        [Fact]
        public void cant_set_null_or_empty_key()
        {
            Assert.Throws<ArgumentNullException>(() => _kv[null].Add("test"));
        }

        [Fact]
        public void accepted_key_has_an_empty_bag_automatically()
        {
           Assert.Equal(0,_kv["test"].Count);
        }

        [Fact]
        public void setting_null_values_is_accepted()
        {
            _kv["test"].Invoking(d=>d.Add(null)).ShouldNotThrow();            
        }

        [Fact]
        public void clear_removes_all_items()
        {
            _kv["t"].Add("h");
            Assert.Equal(1,_kv.Count);
           
            _kv.Clear();
            Assert.Equal(0,_kv.Count);
        }

        private void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}