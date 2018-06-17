#if !COREFX
using System;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace Tests.Reflection
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
#endif