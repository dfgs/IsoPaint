using IsoPaint.Models;
using IsoPaint.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		[Category("Appearance")]
		public static readonly DependencyProperty HoverBrushProperty = DependencyProperty.Register("HoverBrush", typeof(Brush), typeof(DocumentView), new FrameworkPropertyMetadata(Brushes.Red));
		public Brush HoverBrush
		{
			get { return (Brush)GetValue(HoverBrushProperty); }
			set { SetValue(HoverBrushProperty, value); }
		}

			


		public DocumentView()
		{
			InitializeComponent();
		}

		private ControlType FindParent<ControlType>(DependencyObject Source)
			where ControlType:Visual
		{
			while (Source != null)
			{
				if (Source is ControlType) return (ControlType)Source;
				Source = VisualTreeHelper.GetParent(Source);
			}
			return null;
		}


		private void GridView_Hover(DependencyObject sender, IsometricEventArgs e)
		{
			if (DocumentViewModel == null) return;
			DocumentViewModel.HoverX = e.X; DocumentViewModel.HoverY = e.Y; DocumentViewModel.HoverZ = e.Z;DocumentViewModel.HoverFace = e.Face;
		}

		protected async Task OnLeftClickAsync(IsometricEventArgs e)
		{
			int x, y, z;
			VoxelViewModel viewModel;
			Voxel model;

			

			viewModel = DocumentViewModel.Voxels.GetVoxelAtPos(e.X, e.Y, e.Z);
			if ((viewModel!=null) && (Keyboard.Modifiers == ModifierKeys.Control))
			{
				viewModel.ColorID = DocumentViewModel.Palette.Colors.SelectedItem?.ID ?? 0;
				return;
			}

			switch (e.Face)
			{
				case Faces.Top:
					x = e.X; y = e.Y; z = e.Z + 1;
					break;
				case Faces.Right:
					x = e.X; y = e.Y + 1; z = e.Z;
					break;
				case Faces.Front:
					x = e.X + 1; y = e.Y; z = e.Z;
					break;
				default:
					x = e.X; y = e.Y; z = e.Z;
					break;
			}

			if ((x < 0) || (y < 0) || (z < 0) || (x >= DocumentViewModel.SizeX) || (y >= DocumentViewModel.SizeY) || (z >= DocumentViewModel.SizeZ)) return;

			if (DocumentViewModel.Voxels.ContainsVoxelAtPos(x, y, z)) return;

			model = new Voxel() { X = x, Y = y, Z = z, ColorID = DocumentViewModel.Palette.Colors.SelectedItem?.ID ?? 0 };
			try
			{
				await DocumentViewModel.Voxels.AddAsync(null, model);
			}
			catch
			{
				DocumentViewModel.ErrorMessage = "Failed to add voxel";
			}

		}

		protected async Task OnRightClickAsync(IsometricEventArgs e)
		{
			VoxelViewModel viewModel;

			viewModel = DocumentViewModel.Voxels.GetVoxelAtPos(e.X, e.Y, e.Z);
			if (viewModel == null) return;
			try
			{
				await DocumentViewModel.Voxels.RemoveAsync(null, viewModel);
			}
			catch
			{
				DocumentViewModel.ErrorMessage = "Failed to remove voxel";
			}
		}
		

		private async void GridView_Click(DependencyObject sender, IsometricEventArgs e)
		{
			if (DocumentViewModel == null) return;
			if ((e.X < 0) || (e.Y < 0) || (e.Z < 0) || (e.X >= DocumentViewModel.SizeX) || (e.Y >= DocumentViewModel.SizeY) || (e.Z >= DocumentViewModel.SizeZ)) return;
			
			if (Mouse.LeftButton == MouseButtonState.Pressed)
			{
				await OnLeftClickAsync(e);
			}
			else if (Mouse.RightButton == MouseButtonState.Pressed)
			{
				await OnRightClickAsync(e);
			}
		}
	}
}
