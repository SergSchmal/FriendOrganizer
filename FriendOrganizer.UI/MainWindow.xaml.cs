using System;
using System.Windows;
using FriendOrganizer.UI.ViewModel;

namespace FriendOrganizer.UI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _viewModel = mainViewModel;
            DataContext = _viewModel;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel.Load();
        }
    }
}