using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CavemanTools.Testing
{
    public class BenchmarksContainer
    {
        List<BItem> _actions= new List<BItem>();
        public string ContainerName { get; set; }
        class BItem
        {
            public BenchmarkAction Action;
            public BenchmarkResult Result;

            public BItem(BenchmarkAction action,string name)
            {
                Result=new BenchmarkResult(){Name = name};
                Action = action;
            }
        }

        public int WarmUpIterations { get; set; }
        public int Iterations { get; set; }

        public BenchmarksContainer(string name=null)
        {
            WarmUpIterations = 10;
            Iterations = 500;
            if (name != null) ContainerName = name;
        }

        
        public void Add(BenchmarkAction action,string name)
        {
            _actions.Add(new BItem(action,name));     
        }
        
        public void Execute(params object[] args)
        {
            var t = new Stopwatch();
           
            ResetAll();

           
            _actions.ForEach(act =>
                {
                    try
                    {
                        for (int i = 0; i < Iterations; i++)
                        {
                            t.Reset();
                            t.Start();
                            act.Action(args);
                            t.Stop();
                            act.Result.AddIteration(t.Elapsed);
                        }
                    }
                    catch (NotSupportedException ex)
                    {
                        act.Result.SetNoSupported(ex.Message);
                    }
                });
            
            
        }

        private void ResetAll()
        {
            _actions.ForEach(act1 => act1.Result.Reset());
        }

        public void ExecuteWarmup(params object[] args)
        {
            Execute(args);
            //ResetAll();
            //_actions.ForEach(act =>
            //    {
            //        try
            //        {
            //            for (int i = 0; i < WarmUpIterations; i++)
            //            {
            //                act.Action(args);                                 
            //            }}
            //        catch(NotSupportedException)
            //        {
                    
            //        }
            //    });
        }

        public IEnumerable<BenchmarkResult> GetResults
        {
            get { return _actions.Select(a => a.Result); }
        }

        public void ResultsToConsole()
        {
            GetResults.ForEach(Console.WriteLine);
        }
    }
}