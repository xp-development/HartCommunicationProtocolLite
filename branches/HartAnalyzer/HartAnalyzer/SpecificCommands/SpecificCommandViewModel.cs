using System;
using System.Collections.ObjectModel;
using System.Linq;
using Cinch;
using MEFedMVVM.ViewModelLocator;

namespace HartAnalyzer.SpecificCommands
{
    [ExportViewModel("SpecificCommandViewModel")]
    public class SpecificCommandViewModel : ViewModelBase
    {
        public CommandItemViewModel ActualCommandItem
        {
            get { return _actualCommandItem; }
            set
            {
                _actualCommandItem = value;
                NotifyPropertyChanged("ActualCommandItem");
            }
        }

        public ObservableCollection<CommandItemViewModel> CommandItems { get; set; }

        public SpecificCommandViewModel()
        {
            CommandItems = new ObservableCollection<CommandItemViewModel>();
            CommandItems.Add(new CommandItemViewModel {Value = 22});
        }

        public void TextBoxGotFocusAction()
        {
            if (CommandItems.Last() == ActualCommandItem)
                CommandItems.Add(new CommandItemViewModel());
            if (CommandItems.Count >= 3 && CommandItems.Take(CommandItems.Count - 2).FirstOrDefault(item => item == ActualCommandItem) != null)
            {
                if (CommandItems.Last().Value == null)
                {
                    CommandItems.RemoveAt(CommandItems.Count - 1);
                    TextBoxGotFocusAction();
                }
            }
        }

        private CommandItemViewModel _actualCommandItem;
    }

    public class CommandItemViewModel : ViewModelBase
    {
        public Type Type
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }

        public object Value
        {
            get { return _value; }
            set
            {
                _value = Convert.ChangeType(value, _type);
                NotifyPropertyChanged("Value");
            }
        }

        public CommandItemViewModel()
            : this(typeof(byte))
        {}

        public CommandItemViewModel(Type type)
        {
            Type = type;
        }

        private object _value;
        private Type _type;
    }
}