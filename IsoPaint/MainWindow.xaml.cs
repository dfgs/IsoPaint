using IsoPaint.Models;
using IsoPaint.ViewModels;
using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace IsoPaint
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DocumentViewModelCollection vm;
		private ILogger logger;

		public MainWindow()
		{
			InitializeComponent();
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{

			logger = new ConsoleLogger(new DefaultLogFormatter());
			vm = new DocumentViewModelCollection(logger);
			await vm.LoadAsync(new List<Document>());
			DataContext = vm;
		}

		private void NewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.Handled = true; e.CanExecute = true;
		}

		private async void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{

			try
			{
				vm.SelectedItem = await vm.AddAsync(null);
			}
			catch
			{
				vm.ErrorMessage = "Failed to create new document";
			}
		}

		
	}
}
