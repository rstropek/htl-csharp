using System;
using System.Threading.Tasks;
using Xunit;

namespace CashDesk.Tests
{
    public class TestAddMember
    {
        [Fact]
        public void InvalidParameters()
        {
            using (var dal = new DataAccess())
            {
                Assert.ThrowsAsync<ArgumentException>(async () => await dal.AddMemberAsync(null, null, DateTime.Today));
            }
        }

        [Fact]
        public async Task AddMember()
        {
            using (var dal = new DataAccess())
            {
                await dal.InitializeDatabaseAsync();
                var memberNumber = await dal.AddMemberAsync("Foo", "AddMember", DateTime.Today.AddYears(-18));
                Assert.True(memberNumber >= 0);
            }
        }

        [Fact]
        public async Task DuplicateLastName()
        {
            using (var dal = new DataAccess())
            {
                await dal.InitializeDatabaseAsync();
                await dal.AddMemberAsync("Foo", "DuplicateLastName", DateTime.Today.AddYears(-18));
                await Assert.ThrowsAsync<DuplicateNameException>(async () => await dal.AddMemberAsync("Foo", "DuplicateLastName", DateTime.Today.AddYears(-18)));
            }
        }
    }
}
