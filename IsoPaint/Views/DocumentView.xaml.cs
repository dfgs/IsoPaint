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


		public static readonly DependencyProperty HoverXProperty = DependencyProperty.Register("HoverX", typeof(int), typeof(DocumentView), new FrameworkPropertyMetadata(-1));
		public int HoverX
		{
			get { return (int)GetValue(HoverXProperty); }
			set { SetValue(HoverXProperty, value); }
		}


		public static readonly DependencyProperty HoverYProperty = DependencyProperty.Register("HoverY", typeof(int), typeof(DocumentView), new FrameworkPropertyMetadata(-1));
		public int HoverY
		{
			get { return (int)GetValue(HoverYProperty); }
			set { SetValue(HoverYProperty, value); }
		}

		public static readonly DependencyProperty HoverZProperty = DependencyProperty.Register("HoverZ", typeof(int), typeof(DocumentView), new FrameworkPropertyMetadata(-1));
		public int HoverZ
		{
			get { return (int)GetValue(HoverZProperty); }
			set { SetValue(HoverZProperty, value); }
		}


		public static readonly DependencyProperty HoverFaceProperty = DependencyProperty.Register("HoverFace", typeof(HoverFaces), typeof(DocumentView));
		public HoverFaces HoverFace
		{
			get { return (HoverFaces)GetValue(HoverFaceProperty); }
			set { SetValue(HoverFaceProperty, value); }
		}



		public DocumentView()
		{
			InitializeComponent();
		}
		

	

		private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
		{
			int x, y, z;
			IsometricPanel panel;
			Point pt;
			
			panel = sender as IsometricPanel;
			if (panel == null) return;

			pt = e.GetPosition(panel);
			if (e.Source == sender)
			{
				z = 0; HoverFace = HoverFaces.Bottom;
				panel.UnProject(pt, out x, out y, z);
				if (x < 0)
				{
					x = 0; HoverFace = HoverFaces.Back;
					panel.UnProject(pt, x, out y, out z);
				}
				if (y < 0)
				{
					y = 0; HoverFace = HoverFaces.Left;
					panel.UnProject(pt, out x, y, out z);
				}
			}
			else
			{
				x = IsometricPanel.GetX((UIElement)e.Source);
				y = IsometricPanel.GetY((UIElement)e.Source);
				z = IsometricPanel.GetZ((UIElement)e.Source);
				HoverFace = HoverAdorner.GetFace((UIElement)e.OriginalSource);
			}
			this.HoverX = x; this.HoverY = y; this.HoverZ = z;
		}

		private async void IsometricPanel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Voxel model;
			int x, y, z;
			VoxelViewModel viewModel;

			if (DocumentViewModel == null) return;

			if (e.LeftButton == MouseButtonState.Pressed)
			{
				switch (HoverFace)
				{
					case HoverFaces.Top:
						x = HoverX; y = HoverY; z = HoverZ + 1;
						break;
					case HoverFaces.Right:
						x = HoverX; y = HoverY + 1; z = HoverZ;
						break;
					case HoverFaces.Front:
						x = HoverX + 1; y = HoverY; z = HoverZ;
						break;
					default:
						x = HoverX; y = HoverY; z = HoverZ;
						break;
				}
				if ((x < 0) || (y < 0) || (z < 0) || (x >= DocumentViewModel.SizeX) || (y >= DocumentViewModel.SizeY) || (z >= DocumentViewModel.SizeZ)) return;

				if (DocumentViewModel.Voxels.ContainsVoxelAtPos(x, y, z)) return;
				model = new Voxel() { X = x, Y = y, Z = z };
				try
				{
					await DocumentViewModel.Voxels.AddAsync(null, model);
				}
				catch
				{
					DocumentViewModel.ErrorMessage = "Failed to add voxel";
				}
			}
			else if (e.RightButton==MouseButtonState.Pressed)
			{
				x = HoverX;y = HoverY;z = HoverZ;

				if ((x < 0) || (y < 0) || (z < 0) || (x >= DocumentViewModel.SizeX) || (y >= DocumentViewModel.SizeY) || (z >= DocumentViewModel.SizeZ)) return;

				viewModel = DocumentViewModel.Voxels.GetVoxelAtPos(x, y, z);
				if (viewModel==null) return;
				try
				{
					await DocumentViewModel.Voxels.RemoveAsync(null, viewModel);
				}
				catch
				{
					DocumentViewModel.ErrorMessage = "Failed to remove voxel";
				}
			}
		}



		

	}
}
