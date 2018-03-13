using IsoPaint.Models;
using IsoPaint.ViewModels;
using LogLib;
using Microsoft.Win32;
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
using ViewLib;
using ViewModelLib.PropertyViewModels;

namespace IsoPaint
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DocumentViewModelCollection documentViewModel;
		private ILogger logger;

		public MainWindow()
		{
			logger = new ConsoleLogger(new DefaultLogFormatter());
			documentViewModel = new DocumentViewModelCollection(logger);

			InitializeComponent();
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			await documentViewModel.LoadAsync(new List<Document>());
			DataContext = documentViewModel;
		}

		private bool EditCallBack(IPropertyViewModelCollection Properties)
		{
			EditWindow editWindow;

			editWindow = new EditWindow() { Owner =this, PropertyViewModelCollection = Properties };
			return editWindow.ShowDialog() ?? false;
		}

		private void NewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.Handled = true; e.CanExecute = true;
		}

		private async void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{

			try
			{
				documentViewModel.SelectedItem = await documentViewModel.AddAsync(EditCallBack);
			}
			catch
			{
				documentViewModel.ErrorMessage = "Failed to create new document";
			}
		}

		

		private void OpenCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.Handled = true; e.CanExecute = true;
		}

		private async void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog dialog;
			DocumentViewModel vm;

			dialog = new OpenFileDialog();
			dialog.Filter = "xml files|*.xml|All files|*.*";
			if (!dialog.ShowDialog(this) ?? false) return;

			try
			{
				vm = new DocumentViewModel(logger);
				await vm.LoadAsync(dialog.FileName);
				await documentViewModel.AddAsync(null,vm);
				documentViewModel.SelectedItem = vm;
			}
			catch
			{
				documentViewModel.ErrorMessage = "Failed to load document";
			}
		}


		private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.Handled = true; e.CanExecute = (documentViewModel.SelectedItem != null) && (documentViewModel.SelectedItem.FileName!=null);
		}

		private async void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				await documentViewModel.SelectedItem.SaveAsync();
			}
			catch
			{
				documentViewModel.ErrorMessage = "Failed to save document";
			}
		}

		private void SaveAsCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.Handled = true; e.CanExecute = documentViewModel.SelectedItem != null;
		}

		private async void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SaveFileDialog dialog;

			dialog = new SaveFileDialog();
			dialog.InitialDirectory = System.IO.Path.GetFullPath(documentViewModel.SelectedItem.FileName);
			dialog.FileName = System.IO.Path.GetFileName( documentViewModel.SelectedItem.FileName);
			dialog.Filter = "xml files|*.xml|All files|*.*";
			if (!dialog.ShowDialog(this) ?? false) return;

			try
			{
				await documentViewModel.SelectedItem.SaveToFileAsync(dialog.FileName);
			}
			catch
			{
				documentViewModel.ErrorMessage = "Failed to save document";
			}
		}

		private void ExportPOVCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.Handled = true; e.CanExecute = documentViewModel.SelectedItem != null;
		}

		private async void ExportPOVCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SaveFileDialog dialog;

			dialog = new SaveFileDialog();
			dialog.InitialDirectory = System.IO.Path.GetFullPath(documentViewModel.SelectedItem.FileName);
			dialog.FileName = System.IO.Path.GetFileName( System.IO.Path.ChangeExtension(documentViewModel.SelectedItem.FileName,".pov"));
			dialog.Filter = "pov files|*.pov|All files|*.*";
			if (!dialog.ShowDialog(this) ?? false) return;

			try
			{
				await documentViewModel.SelectedItem.ExportToPOVFileAsync(dialog.FileName);
			}
			catch
			{
				documentViewModel.ErrorMessage = "Failed to export document";
			}
		}




	}
}
