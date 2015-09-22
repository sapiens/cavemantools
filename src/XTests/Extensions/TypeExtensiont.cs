using Xunit;
using System;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Xunit.Extensions;

namespace XTests.Extensions
{
    public class TypeExtensiont
    {
        private Stopwatch _t = new Stopwatch();

        public TypeExtensiont()
        {

        }

        [Fact]
        public void user_defined_reference_types()
        {
            var objects = new object[] {2,Guid.Empty,new byte[0], EnvironmentVariableTarget.Process,"string"};
            
            objects.ForEach(o=>o.GetType().IsUserDefinedClass().Should().BeFalse("{0}".ToFormat(o)));

            typeof (int?).IsUserDefinedClass().Should().BeFalse();

            this.GetType().IsUserDefinedClass().Should().BeTrue();
        }

        public interface ISaveSaga<T>
        {
            void Save(T data);
        }

        public class SaveSaga : ISaveSaga<string>
        {
            public void Save(string data)
            {
                throw new NotImplementedException();
            }
        }

        public abstract class MyClass<T>
        {
             public interface IInternal<V>
             {
                 T Bla(V arg);
             }
        }

        public class Myimpl:MyClass<string>.IInternal<int>
        {
            public string Bla(int arg)
            {
                throw new NotImplementedException();
            }
        }

        public class MyImpl2:MyClass<string>
        {
            
        }

     
        [Fact]
        public void type_extends_generic_class_with_args()
        {
            var t = typeof(MyImpl2);
            t.InheritsGenericType(typeof(MyClass<>)).Should().BeTrue();
            t.InheritsGenericType(typeof(MyClass<string>)).Should().BeTrue();
        }

        [Theory]
        [InlineData(typeof(ISaveSaga<>), true)]
        [InlineData(typeof(ISaveSaga<string>), true)]
        [InlineData(typeof(ISaveSaga<int>), false)]
        public void implements_generic_interface(Type type,bool result)
        {
            typeof (SaveSaga).ImplementsGenericInterface(type).Should().Be(result);            
        }

        [Theory]
        [InlineData(typeof(MyClass<>.IInternal<>), true)]
        [InlineData(typeof(MyClass<string>.IInternal<int>), true)]
        [InlineData(typeof(MyClass<string>.IInternal<string>), false)]
        public void implements_nested_generic_interface(Type type, bool result)
        {
            typeof(Myimpl).ImplementsGenericInterface(type).Should().Be(result);  
        }


        [Fact]
        public void implements_generic_interface_with_specified_type_argument()
        {
            typeof(Myimpl).ImplementsGenericInterface(typeof(MyClass<>.IInternal<>),typeof(string),typeof(int)).Should().BeTrue();   
        }
        
        [Fact]
        public void extends_generic_type_with_specified_type_argument()
        {
            typeof(MyImpl2).InheritsGenericType(typeof(MyClass<>),typeof(string)).Should().BeTrue();
        }
        protected void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}