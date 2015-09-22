using System;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace XTests.Reflection
{
    public class FasterImplementationTests
    {
        private Stopwatch _t = new Stopwatch();
        private MyClass _mc;
        private Type _tp;
        private PropertyInfo _propId;
        private PropertyInfo _propCreated;
        private PropertyInfo _propName;

        class MyClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime? CreatedOn { get; set; }
        }

        public FasterImplementationTests()
        {
            _mc = new MyClass();
            _tp = typeof (MyClass);
            _propId = _tp.GetProperty("Id");
            _propName = _tp.GetProperty("Name");
            _propCreated = _tp.GetProperty("CreatedOn");
        }

        [Fact]
        public void fast_setter_works()
        {
           _propCreated.SetValueFast(_mc,new DateTime(2012,05,03)); 
            Assert.Equal(new DateTime(2012,05,03),_mc.CreatedOn);   
            _propName.SetValueFast(_mc,"test");
            Assert.Equal("test",_mc.Name);
        }

        [Fact]
        public void fast_getter_works()
        {
            _mc.Id = 45;
            var rez = _propId.GetValueFast(_mc);
            Assert.Equal(45, (int)rez);

            _mc.Name = "45";
            rez = _propName.GetValueFast(_mc);
            Assert.Equal("45", rez);

            _mc.CreatedOn = new DateTime(2012,05,03);
            rez = _propCreated.GetValueFast(_mc);
            Assert.Equal(new DateTime(2012,05,03),(DateTime?)rez);
        }

        private const int Iterations = 1000000;

      //  [Fact]
        public void fast_setter_benchmark()
        {
            //warmup
            _propId.SetValueFast(_mc, 4);
            
            _t.Start();
            for(int i=0;i<Iterations;i++)
           {
               _propId.SetValueFast(_mc,i);
           }
            _t.Stop();
            Write("Fastsetter: {0} ms",_t.ElapsedMilliseconds);

            _t.Restart();
            for (int i = 0; i < Iterations; i++)
            {
                _propId.SetValue(_mc,i,null);
            }
            _t.Stop();
            Write("Reflection setter: {0} ms", _t.ElapsedMilliseconds);
           
        }

        
     //   [Fact]
        public void fast_getter_benchmark()
        {
            //warmup
            _propName.GetValueFast(_mc);

            _t.Start();
            for (int i = 0; i < Iterations; i++)
            {
                _propName.GetValueFast(_mc);
            }
            _t.Stop();
            Write("Fastgetter: {0} ms", _t.ElapsedMilliseconds);

           _t.Restart();
            for (int i = 0; i < Iterations; i++)
            {
                _propName.GetValue(_mc, null);
            }
            _t.Stop();
            Write("Reflection getter: {0} ms", _t.ElapsedMilliseconds);

        }


       // [Fact]
        public void fast_instantiate_benchamrk()
        {
            _t.Start();
            
            for (int i = 0; i <= 1000000; i++)
            {
                var d = new MyClass();
            }
            _t.Stop();
            Write("Manual create instance: {0} ms", _t.ElapsedMilliseconds.ToString());

            Activator.CreateInstance<MyClass>();
            _t.Start();
            
            for (int i = 0; i <= 1000000; i++)
            {
                Activator.CreateInstance(_tp);
            }
            _t.Stop();
            Write("Activator create instance: {0} ms", _t.ElapsedMilliseconds.ToString());

            var fac = TypeFactory.GetFactory(_tp);

            _t.Restart();
            for (int i = 0; i <= 1000000; i++)
            {
                TypeFactory.GetFactory(_tp);
            }
            _t.Stop();
            Write("TypeFactory create instance: {0} ms", _t.ElapsedMilliseconds.ToString());


        }

        private void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}