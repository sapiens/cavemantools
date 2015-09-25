using System.Threading;
using System.Threading.Tasks;
using CavemanTools.Infrastructure;
using FluentAssertions;
using Xunit;

namespace XTests
{
    public class MediatorTests
    {
        private MessagingMediator _sut;

        public class AsyncQuery:IHandleQueryAsync<NoInput,NoResult>
        {
            public Task<NoResult> HandleAsync(NoInput input,CancellationToken cancel)
            {
                return Task.FromResult(NoResult.Instance);
            }
        }
        
        public MediatorTests()
        {
           _sut=new MessagingMediator(t=>new AsyncQuery());
        }

        [Fact]
        public async Task async_non_generic_handlers_are_invoked_successfully()
        {
            var res = await _sut.RequestAsync(NoInput.Instance, typeof (NoResult), CancellationToken.None);
            res.Should().NotBeNull();
            res.Should().BeOfType<NoResult>();
        }


        [Fact]
        public async Task async_generic_handlers_are_invoked_successfully()
        {
            var res = await _sut.With(NoInput.Instance).RequestAsync<NoResult>(CancellationToken.None);
            res.Should().NotBeNull();
            res.Should().BeOfType<NoResult>();
        }
    }
}