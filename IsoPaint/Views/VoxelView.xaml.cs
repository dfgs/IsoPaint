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
	/// Logique d'interaction pour VoxelView.xaml
	/// </summary>
	public partial class VoxelView : UserControl
	{

		public static readonly DependencyProperty BrushProperty = DependencyProperty.Register("Brush", typeof(Brush), typeof(VoxelView));
		public Brush Brush
		{
			get { return (Brush)GetValue(BrushProperty); }
			set { SetValue(BrushProperty, value); }
		}

		public VoxelView()
		{
			InitializeComponent();
		}
	}
}
