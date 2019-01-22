using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CavemanTools.Logging;
using Xunit;

namespace Tests.Extensions
{
    
    public class Test
    {
        public void Method(int id)
        {
            
        }

        public int Bla()
        {
            return 0;
        }

        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Data { get; set; }
        public Guid? Uid { get; set; }
        public TestEnum Enum { get; set; }
        public Test Child { get; set; }
    }

    public enum TestEnum    
    {
        None,First,Second
    }

    public class ExpressionExtensionsTests
    {
        private Stopwatch _t = new Stopwatch();
        private Type _tp;

        public ExpressionExtensionsTests()
        {
            _tp = typeof (Test);
        }

        //[Fact]
        //public void get_method_info_from_expression_when_returning_void()
        //{
        //    var mi = ExpressionExtensions.GetMethodInfo<Test>(t => t.Method(2));
        //    Assert.Equal(_tp.GetMethod("Method"),mi);
        //    ExpressionExtensions.GetPropertyInfo<Test>(t => t.Child);
        //    ExpressionExtensions.GetPropertyInfo<Test>(t => t.Child.Child);
        //}
    
        //[Fact]
        //public void get_method_info_from_expression_when_returning_something()
        //{
        //    var mi = ExpressionExtensions.GetMethodInfo<Test>(t => t.Bla());
        //    Assert.Equal(_tp.GetMethod("Bla"),mi);
        //}


        [Fact]
        public void member_belongs_to_paramater()
        {
            Expression<Func<Test, bool>> data = t => t.Data == "23";
            Assert.True(ObjectExtend.CastAs<MemberExpression>(data.Body.CastAs<BinaryExpression>().Left).BelongsToParameter());
        }

        [Fact]
        public void get_convert_nullable_enum_value()
        {
            var c = Expression.Convert(Expression.Constant(1), typeof(TestEnum?));
            var v = (TestEnum?) c.GetValue();
            Assert.True(v.Value==TestEnum.First);
        }
        
        [Fact]
        public void get_convert_nullable_enum_null_value()
        {
            var c = Expression.Convert(Expression.Constant(null), typeof(TestEnum?));
            var v = (TestEnum?) c.GetValue();
            Assert.True(v==null);
        }
      
        [Fact]
        public void get_convert_enum_value()
        {
            var c = Expression.Convert(Expression.Constant(1), typeof(TestEnum));
            Assert.IsType<TestEnum>(c.GetValue());
            var v = (TestEnum) c.GetValue();
            Assert.True(v==TestEnum.First);
        }
        
        [Fact]
        public void get_convert_value()
        {
            var c = Expression.Convert(Expression.Constant(1), typeof(double));
            var o = c.GetValue();
            Assert.IsType<double>(o);
          
        }
      
        [Fact]
        public void enum_is_handled_correctly()
        {
            TestEnum f=TestEnum.Second;            
            Expression<Func<Test, bool>> data = t => t.Enum==f;
            var j = data.Body.CastAs<BinaryExpression>().Right;
            var s = j.GetValue();
        }
        
        [Fact]
        public void complex_member_belongs_to_paramater()
        {
            Expression<Func<Test, bool>> data = t => t.Child.Child.Child.Data == "23";
            Assert.True(ObjectExtend.CastAs<MemberExpression>(data.Body.CastAs<BinaryExpression>().Left).BelongsToParameter());
        }
        
        [Fact]
        public void member_belongs_to_paramater_of_type()
        {
            Expression<Func<Test, bool>> data = t => t.Data == "23";
            Assert.True(ObjectExtend.CastAs<MemberExpression>(data.Body.CastAs<BinaryExpression>().Left).BelongsToParameter(typeof(Test)));
        }

        [Fact]
        public void method_belongs_to_parameter()
        {
            Expression<Action<Test>> data = t => t.Child.Bla();
            Assert.True(data.Body.BelongsToParameter());
        }

        [Fact]
        public void member_doesnt_belong_to_paramater_of_type()
        {
            Expression<Func<Test, bool>> data = t => t.Data == "23";
            Assert.False(ObjectExtend.CastAs<MemberExpression>(data.Body.CastAs<BinaryExpression>().Left).BelongsToParameter(typeof(MyClass)));
        }

        [Fact]
        public void member_is_parameter()
        {
            Expression<Func<int, bool>> data = t => t == 23;
            Assert.True(ObjectExtend.CastAs<BinaryExpression>(data.Body).Left.IsParameter(typeof(int)));
        }

        [Fact]
        public void get_value_for_field()
        {
            var id = "23";
            Expression<Func<Test, bool>> data = t => t.Data == id;
            Assert.Equal("23",ObjectExtend.CastAs<MemberExpression>(data.Body.CastAs<BinaryExpression>().Right).GetValue());
        }

        [Fact]
        public void get_value_for_member_init()
        {
            Expression<Func<Test>> data=()=>new Test(){Id=12};
            var t = data.Body.GetValue() as Test;
            Assert.Equal(12,t.Id);            
        }

        [Fact]
        public void init_nullable_member_of_object()
        {
            Expression<Func<Test>> data = () => new Test() { Uid = Guid.NewGuid() };
            var t = data.Body.GetValue() as Test;
            Assert.NotNull(t.Uid);
            Assert.NotEqual(Guid.Empty,t.Uid);
        }

        string Id()
        {
            return "44";
        }

        string Id(int d)
        {
            return d.ToString();
        }

        Test TestId()
        {
            return new Test() {Data = "29"};
        }

        [Fact]
        public void get_value_for_property()
        {
            var dt = new {id = "23"};
            Expression<Func<Test, bool>> data = t => t.Data == dt.id;
            Assert.Equal("23",ObjectExtend.CastAs<MemberExpression>(data.Body.CastAs<BinaryExpression>().Right).GetValue());
        }

        [Fact]
        public void get_value_for_methoid_call()
        {
            Expression<Func<Test, bool>> data = t => t.Data == this.Id();
            Assert.Equal("44", ObjectExtend.CastAs<MethodCallExpression>(data.Body.CastAs<BinaryExpression>().Right).GetValue());
        }

        [Fact]
        public void get_value_for_method_call_with_argument()
        {
            Expression<Func<Test, bool>> data = t => t.Data == this.Id(2);
            Assert.Equal("2", ObjectExtend.CastAs<MethodCallExpression>(data.Body.CastAs<BinaryExpression>().Right).GetValue());
        }

        [Fact]
        public void get_value_for_property_returning_from_method_call()
        {
            Expression<Func<Test, bool>> data = t => t.Data == this.TestId().Bla().ToString();
            Assert.Equal("0", ObjectExtend.CastAs<MethodCallExpression>(data.Body.CastAs<BinaryExpression>().Right).GetValue());
        }

        [Fact]
        public void get_value_for_property_returned_by_method()
        {
            Expression<Func<Test, bool>> data = t => t.Data == this.TestId().Data;
            Assert.Equal("29", ObjectExtend.CastAs<BinaryExpression>(data.Body).Right.GetValue());
        }

        //[Fact]
        public void get_value_for_property_returned_by_method1()
        {
            var l = new[] {"a", "b"};
            Expression<Func<Test, bool>> data = t => l.Any(s=>s==t.Data);
            Assert.Equal("29", data.Body.GetValue());
        }

        [Fact]
        public void parameter_is_argument_of_method_call()
        {
            var l = new[] { "a", "b" };
            Expression<Func<Test, bool>> data = t => l.Contains(t.Data);
            Assert.True(ObjectExtend.CastAs<MethodCallExpression>(data.Body).HasParameterArgument());
        }

        [Fact]
        public void expression_with_initializer()
        {
            Expression<Func<Test, bool>> data = t => DateTime.UtcNow > new DateTime(2,2,2);
            Assert.Equal(new DateTime(2,2,2), ObjectExtend.CastAs<BinaryExpression>(data.Body).Right.GetValue());
        }

        

        [Fact]
        public void expression_with_anonymous_object()
        {
           
            Expression<Func<object>> data = () => new {Id = 23};
            dynamic d = data.Body.GetValue();
            Assert.Equal(23,d.Id);
        }

        [Fact]
        public void expression_with_init_array()
        {
            Expression<Func<Test,bool>> data = (t) =>  new[]{1,2}.Contains(2) ;
            Assert.True((bool)data.Body.GetValue());
        }

        [Fact]
        public void get_value_from_array_index()
        {
            var l = new[] {1, 2};
            Expression<Func<int>> data = () => l[0];
            
            Assert.Equal(1,data.Body.GetValue());
        }

        [Fact]
        public void get_value_from_property_index()
        {
            var dict = new List<int>();
            dict.Add(23);
            Expression<Func<int>> data = () => dict[0];
            Assert.Equal(23,data.Body.GetValue());
        }

        [Fact]
        public void belongs_to_parameter_operand()
        {
            Expression<Func<Test, bool>> data = d => !d.IsActive;
            Assert.True(data.Body.BelongsToParameter());

            data = d => d.IsActive;
            Assert.True(data.Body.BelongsToParameter());
        }


        //[Fact]
        //public void evaluate_lambda()
        //{
        //    var l = new[] { 1, 2 };
        //    Expression<Func<bool>> data = () => l.Any(d=>d==1);
        //    Assert.Equal(true,data.Body.GetValue());
        //}

        [Fact]
        public void FactMethodName()
        {
            Expression<Func<Test, bool>> data = t => new[] {1, 2}.Contains(t.Id);
            var meth = ObjectExtend.CastAs<MethodCallExpression>(data.Body);
            
            Assert.Equal("Contains",meth.Method.Name);

            var param = ObjectExtend.CastAs<MemberExpression>(meth.Arguments[1]);
            Assert.True(param.BelongsToParameter());
            var sb = new StringBuilder();
            
            sb.Append(param.Member.Name).Append(" in (");
            
            var list = ObjectExtend.CastAs<IEnumerable>(meth.Arguments[0].GetValue());
            var en=list.GetEnumerator();
            while (en.MoveNext())
            {
                sb.Append(en.Current).Append(",");
            }
            sb.RemoveLast();
            sb.Append(")");
            Write(sb.ToString());
        }

        protected void Write(string format, params object[] param)
        {
            this.LogDebug(format);
        }

        class MyClass
        {
             
        }
    }
}