using MvvmDemo;
using Xunit;

namespace Turmrechnen.ViewModel.Tests
{
    public class ViewModelTests
    {
        [Fact]
        public void ButtonDisabledIfHeightTooLow()
        {
            var vm = new MainWindowViewModel();

            vm.Height = 1;
            Assert.False(vm.CalculateCommand.CanExecute());

            vm.Height = 5;
            Assert.True(vm.CalculateCommand.CanExecute());
        }
    }
}
