using System;
using System.Threading.Tasks;
using Xunit;

namespace CashDesk.Tests
{
    public class TestJoining
    {
        [Fact]
        public void InvalidParameters()
        {
            using (var dal = new DataAccess())
            {
                Assert.ThrowsAsync<ArgumentException>(async () => await dal.JoinMemberAsync(Int32.MaxValue));
            }
        }

        [Fact]
        public async Task JoinMember()
        {
            using (var dal = new DataAccess())
            {
                await dal.InitializeDatabaseAsync();
                var memberNumber = await dal.AddMemberAsync("Foo", "JoinMember", DateTime.Today.AddYears(-18));
                await dal.JoinMemberAsync(memberNumber);
            }
        }

        [Fact]
        public async Task AlreadyMember()
        {
            using (var dal = new DataAccess())
            {
                await dal.InitializeDatabaseAsync();
                var memberNumber = await dal.AddMemberAsync("Foo", "AlreadyMemberJoining", DateTime.Today.AddYears(-18));
                await dal.JoinMemberAsync(memberNumber);
                await Assert.ThrowsAsync<AlreadyMemberException>(async () => await dal.JoinMemberAsync(memberNumber));
            }
        }
    }
}
