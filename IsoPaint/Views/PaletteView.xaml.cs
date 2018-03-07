using IsoPaint.ViewModels;
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

namespace IsoPaint.Views
{
	/// <summary>
	/// Logique d'interaction pour PaletteView.xaml
	/// </summary>
	public partial class PaletteView : UserControl
	{

		public static readonly DependencyProperty PaletteViewModelProperty = DependencyProperty.Register("PaletteViewModel", typeof(PaletteViewModel), typeof(PaletteView));
		public PaletteViewModel PaletteViewModel
		{
			get { return (PaletteViewModel)GetValue(PaletteViewModelProperty); }
			set { SetValue(PaletteViewModelProperty, value); }
		}

		public PaletteView()
		{
			InitializeComponent();
		}

		private void AddCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.Handled = true; e.CanExecute = PaletteViewModel!=null;
		}

		private async void AddCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				await PaletteViewModel.Colors.AddAsync(null);
			}
			catch
			{
				PaletteViewModel.Colors.ErrorMessage = "Failed to add color";
			}
		}
		private void RemoveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.Handled = true; e.CanExecute = (PaletteViewModel != null) && (PaletteViewModel.Colors.SelectedItem!=null);
		}

		private async void RemoveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				await PaletteViewModel.Colors.RemoveAsync(null);
			}
			catch
			{
				PaletteViewModel.Colors.ErrorMessage = "Failed to remove color";
			}
		}



	}
}
