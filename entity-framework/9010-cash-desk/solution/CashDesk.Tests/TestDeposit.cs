using System;
using System.Threading.Tasks;
using Xunit;

namespace CashDesk.Tests
{
    public class TestDeposit
    {
        [Fact]
        public void InvalidParameters()
        {
            using (var dal = new DataAccess())
            {
                Assert.ThrowsAsync<ArgumentException>(async () => await dal.DepositAsync(Int32.MaxValue, 100M));
                Assert.ThrowsAsync<ArgumentException>(async () => await dal.DepositAsync(1, -1M));
            }
        }

        [Fact]
        public async Task Deposit()
        {
            using (var dal = new DataAccess())
            {
                await dal.InitializeDatabaseAsync();
                var memberNumber = await dal.AddMemberAsync("Foo", "Deposit", DateTime.Today.AddYears(-18));
                await dal.JoinMemberAsync(memberNumber);
                await dal.DepositAsync(memberNumber, 100M);
            }
        }

        [Fact]
        public async Task NoMember()
        {
            using (var dal = new DataAccess())
            {
                await dal.InitializeDatabaseAsync();
                var memberNumber = await dal.AddMemberAsync("Foo", "NoMemberDeposit", DateTime.Today.AddYears(-18));
                await Assert.ThrowsAsync<NoMemberException>(async () => await dal.DepositAsync(memberNumber, 100M));
            }
        }
    }
}
