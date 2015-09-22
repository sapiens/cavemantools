
using System;
using System.Diagnostics;
using System.Text;
using CavemanTools.Logging;
using Xunit;

namespace XTests
{

    class Test
    {
        public void Do(string t)
        {
           Console.Write(t);
        } 
    }
    public class General
    {
        private Stopwatch _t = new Stopwatch();
      
        public General()
        {
            
        }

        [Fact]
        public void FactMethodName()
        {
            var text = "MONTRÉAL ÉLITE SÉCURITÉ";
            Write(ConvertAccentedString(text));
        }

        public string ConvertAccentedString(string accentedString,int codepage=1251)
        {
            return Encoding.ASCII.GetString(Encoding.GetEncoding(codepage).GetBytes(accentedString));
        }

        [Fact]
        public void exception_log()
        {
            LogManager.OutputToConsole();
            this.LogError(new InvalidOperationException("something"));
        }

        private void Write(object format, params object[] param)
        {
            Console.WriteLine(format.ToString(), param);
        }
    }  
}