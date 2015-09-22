
using System.Globalization;
using CavemanTools;
using CavemanTools.Logging;
using FluentAssertions;
using Xunit;
using System;
using System.Diagnostics;

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
            return System.Text.Encoding.ASCII.GetString(System.Text.Encoding.GetEncoding(codepage).GetBytes(accentedString));
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