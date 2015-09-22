using System.Threading;
using System.Threading.Tasks;
using CavemanTools.Infrastructure;
using FluentAssertions;
using Xunit;

namespace XTests
{
    public class MediatorTests
    {
        public class AsyncQuery:IHandleQueryAsync<NoInput,NoResult>
        {
            public Task<NoResult> HandleAsync(NoInput input,CancellationToken cancel)
            {
                return Task.FromResult(NoResult.Instance);
            }
        }
        
        public MediatorTests()
        {
            MessagingMediator.Resolver = t => new AsyncQuery();
        }

        [Fact]
        public async Task async_non_generic_handlers_are_invoked_successfully()
        {
            var res = await NoInput.Instance.QueryAsyncTo(typeof (NoResult),CancellationToken.None);
            res.Should().BeOfType<NoResult>();
        }


        [Fact]
        public async Task async_generic_handlers_are_invoked_successfully()
        {
            var res = await NoInput.Instance.QueryAsyncTo<NoResult>(CancellationToken.None);
            res.Should().NotBeNull();
        }
    }
}