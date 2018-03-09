using IsoPaint.Models;
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
    /// Logique d'interaction pour GridView.xaml
    /// </summary>
    public partial class GridView : UserControl
    {
		public event IsometricEventHandler Hover;
		public event IsometricEventHandler Click;

		public GridView()
        {
            InitializeComponent();
        }


		private void IsometricPanel_Hover(DependencyObject sender, IsometricEventArgs e)
		{
			if (Hover != null) Hover(sender, e);
		}

		private void IsometricPanel_Click(DependencyObject sender, IsometricEventArgs e)
		{
			if (Click != null) Click(sender, e);
		}
	}
}
