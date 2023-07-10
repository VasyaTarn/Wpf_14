using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private bool isBold;
        private bool isItalic;
        private bool isUnderline;
        private double fontSize = 12;
        private SolidColorBrush textColor = Brushes.Black;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BoldCommand { get; }
        public ICommand ItalicCommand { get; }
        public ICommand UnderlineCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand Font15Command { get; }
        public ICommand Font39Command { get; }
        public ICommand RedColorCommand { get; }
        public ICommand GreenColorCommand { get; }
        public ICommand BlueColorCommand { get; }

        public bool IsBold
        {
            get { return isBold; }
            set
            {
                if (isBold != value)
                {
                    isBold = value;
                    OnPropertyChanged();
                    ApplyFormatting();
                }
            }
        }

        public bool IsItalic
        {
            get { return isItalic; }
            set
            {
                if (isItalic != value)
                {
                    isItalic = value;
                    OnPropertyChanged();
                    ApplyFormatting();
                }
            }
        }

        public bool IsUnderline
        {
            get { return isUnderline; }
            set
            {
                if (isUnderline != value)
                {
                    isUnderline = value;
                    OnPropertyChanged();
                    ApplyFormatting();
                }
            }
        }

        public double FontSize
        {
            get { return fontSize; }
            set
            {
                if (fontSize != value)
                {
                    fontSize = value;
                    OnPropertyChanged();
                    ApplyFormatting();
                }
            }
        }

        public SolidColorBrush TextColor
        {
            get { return textColor; }
            set
            {
                if (textColor != value)
                {
                    textColor = value;
                    OnPropertyChanged();
                    ApplyFormatting();
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            BoldCommand = new RelayCommand(BoldCommandExecute);
            ItalicCommand = new RelayCommand(ItalicCommandExecute);
            UnderlineCommand = new RelayCommand(UnderlineCommandExecute);
            ClearCommand = new RelayCommand(ClearCommandExecute);
            Font15Command = new RelayCommand(Font15CommandExecute);
            Font39Command = new RelayCommand(Font39CommandExecute);
            RedColorCommand = new RelayCommand(RedColorCommandExecute);
            GreenColorCommand = new RelayCommand(GreenColorCommandExecute);
            BlueColorCommand = new RelayCommand(BlueColorCommandExecute);

            DataContext = this;
        }

        private void ApplyFormatting()
        {
            var selection = rtbEditor.Selection;
            if (selection != null)
            {
                if (IsBold)
                    selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                else
                    selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);

                if (IsItalic)
                    selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                else
                    selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);

                if (IsUnderline)
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                else
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);

                selection.ApplyPropertyValue(TextElement.FontSizeProperty, FontSize);
                selection.ApplyPropertyValue(TextElement.ForegroundProperty, TextColor);
            }
        }

        private void BoldCommandExecute()
        {
            IsBold = !IsBold;
        }

        private void ItalicCommandExecute()
        {
            IsItalic = !IsItalic;
        }

        private void UnderlineCommandExecute()
        {
            IsUnderline = !IsUnderline;
        }

        private void ClearCommandExecute()
        {
            rtbEditor.Selection.Text = string.Empty;
        }

        private void Font15CommandExecute()
        {
            FontSize = 15;
        }

        private void Font39CommandExecute()
        {
            FontSize = 39;
        }

        private void RedColorCommandExecute()
        {
            TextColor = Brushes.Red;
        }

        private void GreenColorCommandExecute()
        {
            TextColor = Brushes.Green;
        }

        private void BlueColorCommandExecute()
        {
            TextColor = Brushes.Blue;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            execute?.Invoke();
        }

    }
}
