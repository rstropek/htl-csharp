using System;
using System.Threading.Tasks;
using Xunit;

namespace CashDesk.Tests
{
    public class TestDeleteMember
    {
        [Fact]
        public void InvalidParameters()
        {
            using (var dal = new DataAccess())
            {
                Assert.ThrowsAsync<ArgumentException>(async () => await dal.DeleteMemberAsync(Int32.MaxValue));
            }
        }

        [Fact]
        public async Task DeleteMember()
        {
            using (var dal = new DataAccess())
            {
                await dal.InitializeDatabaseAsync();
                var memberNumber = await dal.AddMemberAsync("Foo", "DeleteMember", DateTime.Today.AddYears(-18));
                await dal.DeleteMemberAsync(memberNumber);
            }
        }

        [Fact]
        public async Task CascadeDeleteMember()
        {
            using (var dal = new DataAccess())
            {
                await dal.InitializeDatabaseAsync();
                var memberNumber = await dal.AddMemberAsync("Foo", "CascadeDeleteMember", DateTime.Today.AddYears(-18));
                await dal.JoinMemberAsync(memberNumber);
                await dal.DepositAsync(memberNumber, 100);
                await dal.DeleteMemberAsync(memberNumber);
            }
        }
    }
}
