using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;

namespace MvvmDemo
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            CalculateCommand = new DelegateCommand(
                () =>
                {
                    ResultItems = new TurmrechnenLogic().Calculate(BaseValue, Height);
                    RaisePropertyChanged(nameof(ResultItems));
                },
                () => Height >= 2);
        }

        public int BaseValue { get; set; } = 3;

        private int HeightValue = 8;
        public int Height
        {
            get => HeightValue;
            set
            {
                HeightValue = value;
                CalculateCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<ResultItem> ResultItems { get; set; }

        public DelegateCommand CalculateCommand { get; set; }
    }
}
