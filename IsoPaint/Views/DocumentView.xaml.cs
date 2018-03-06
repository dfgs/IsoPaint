using IsoPaint.Models;
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
	/// Logique d'interaction pour DocumentView.xaml
	/// </summary>
	public partial class DocumentView : UserControl
	{


		public static readonly DependencyProperty DocumentViewModelProperty = DependencyProperty.Register("DocumentViewModel", typeof(DocumentViewModel), typeof(DocumentView));
		public DocumentViewModel DocumentViewModel
		{
			get { return (DocumentViewModel)GetValue(DocumentViewModelProperty); }
			set { SetValue(DocumentViewModelProperty, value); }
		}



		public DocumentView()
		{
			InitializeComponent();
		}
			

		private async void IsometricPanel_Click(DependencyObject sender, ClickEventArgs e)
		{
			Voxel model;

			if (DocumentViewModel == null) return;

			model = new Voxel() { X = e.X, Y = e.Y, Z = e.Z };
			try
			{
				await DocumentViewModel.Voxels.AddAsync(model, null);
			}
			catch
			{
				DocumentViewModel.ErrorMessage = "Failed to add voxel";
			}
		}


	}
}
