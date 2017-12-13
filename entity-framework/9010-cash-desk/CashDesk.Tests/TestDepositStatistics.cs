using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CashDesk.Tests
{
    public class TestDepositStatistics
    {
        [Fact]
        public async Task DepositStatistics()
        {
            using (var dal = new DataAccess())
            {
                await dal.InitializeDatabaseAsync();
                var memberNumber = await dal.AddMemberAsync("Foo", "DepositStatistics", DateTime.Today.AddYears(-18));
                await dal.JoinMemberAsync(memberNumber);
                await dal.DepositAsync(memberNumber, 100M);
                await dal.DepositAsync(memberNumber, 100M);

                var statistics = await dal.GetDepositStatisticsAsync();
                Assert.True(statistics.Count() > 0);
                Assert.True(statistics.Any(s => s.Member.MemberNumber == memberNumber));
                Assert.Equal(statistics.Where(s => s.Member.MemberNumber == memberNumber).Sum(s => s.TotalAmount), 200M);
            }
        }
    }
}
