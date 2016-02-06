using System;
using System.Threading.Tasks;
using CavemanTools.Infrastructure;
using FluentAssertions;
using Xunit;

namespace Tests.Infrastructure
{

    public class CommandResult
    {
         
    }
    public class CommandResultMediatorTests
    {
        public CommandResultMediatorTests()
        {

        }

        [Fact]
        public async Task ok_scenario()
        {
            var m=new CommandResultMediator();
            var l = m.GetListener(Guid.Empty);

            m.ActiveListeners.Should().Be(1);


            await Task.Run(() =>
            {
                this.Sleep(TimeSpan.FromMilliseconds(840));
                m.AddResult(Guid.Empty, new CommandResult());
            });

            var r=await l.GetResult<CommandResult>().ConfigureAwait(false);
            r.Should().NotBeNull();
            m.ActiveListeners.Should().Be(0);
        }
        
        [Fact]
        public void timeout_throws()
        {
            var m=new CommandResultMediator();
            var l = m.GetListener(Guid.Empty,TimeSpan.FromMilliseconds(250));
            
            Task.Run(() =>
            {
                this.Sleep(TimeSpan.FromMilliseconds(840));
                m.AddResult(Guid.Empty, new CommandResult());
            });

            l.Invoking(d => d.GetResult<CommandResult>().Wait()).ShouldThrow<TimeoutException>();
       }


        [Fact]
        public async Task if_timeout_listener_is_removed()
        {
            var m = new CommandResultMediator();
            var l = m.GetListener(Guid.Empty, TimeSpan.FromMilliseconds(250));

            await Task.Run(() =>
            {
                this.Sleep(TimeSpan.FromMilliseconds(840));
                m.AddResult(Guid.Empty, new CommandResult());
            });

            try
            {
                m.ActiveListeners.Should().Be(1);
                await l.GetResult<CommandResult>();
                throw new Exception("Should not be thrown");
            }
            catch (TimeoutException)
            {
                m.ActiveListeners.Should().Be(0);
            }
        }
    }
}