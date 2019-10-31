using System;
using System.Threading.Tasks;
using Xunit;

namespace CashDesk.Tests
{
    public class TestInitialization
    {
        [Fact]
        public async Task MultipleInitializations()
        {
            using (var dal = new DataAccess())
            {
                await dal.InitializeDatabaseAsync();
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await dal.InitializeDatabaseAsync());
            }
        }

        [Fact]
        public void NoInitialization()
        {
            using (var dal = new DataAccess())
            {
                Assert.ThrowsAsync<InvalidOperationException>(async () => await dal.AddMemberAsync("A", "B", DateTime.Today));
                Assert.ThrowsAsync<InvalidOperationException>(async () => await dal.DeleteMemberAsync(0));
                Assert.ThrowsAsync<InvalidOperationException>(async () => await dal.JoinMemberAsync(0));
                Assert.ThrowsAsync<InvalidOperationException>(async () => await dal.CancelMembershipAsync(0));
                Assert.ThrowsAsync<InvalidOperationException>(async () => await dal.DepositAsync(0, 1M));
                Assert.ThrowsAsync<InvalidOperationException>(async () => await dal.GetDepositStatisticsAsync());
            }
        }
    }
}
