using IsoPaint.Models;
using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModelLib;

namespace IsoPaint.ViewModels
{
	public class DocumentViewModel : ViewModel<Document>
	{

		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(DocumentViewModel));
		public string Name
		{
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}

		public int SizeX
		{
			get { return Model.SizeX; }
			set { Model.SizeX = value; OnPropertyChanged(); }
		}
		public int SizeY
		{
			get { return Model.SizeY; }
			set { Model.SizeY = value; OnPropertyChanged(); }
		}
		public int SizeZ
		{
			get { return Model.SizeZ; }
			set { Model.SizeZ = value; OnPropertyChanged(); }
		}


		public static readonly DependencyProperty HoverXProperty = DependencyProperty.Register("HoverX", typeof(int), typeof(DocumentViewModel));
		public int HoverX
		{
			get { return (int)GetValue(HoverXProperty); }
			set { SetValue(HoverXProperty, value); }
		}

		public static readonly DependencyProperty HoverYProperty = DependencyProperty.Register("HoverY", typeof(int), typeof(DocumentViewModel));
		public int HoverY
		{
			get { return (int)GetValue(HoverYProperty); }
			set { SetValue(HoverYProperty, value); }
		}

		public static readonly DependencyProperty HoverZProperty = DependencyProperty.Register("HoverZ", typeof(int), typeof(DocumentViewModel));
		public int HoverZ
		{
			get { return (int)GetValue(HoverZProperty); }
			set { SetValue(HoverZProperty, value); }
		}


		public VoxelViewModelCollection Voxels
		{
			get;
			private set;
		}


		public DocumentViewModel(ILogger Logger) : base(Logger)
		{
			Name = "New document";
			Voxels = new VoxelViewModelCollection(Logger);
		}

		protected override async Task OnLoadedAsync(Document Model)
		{
			await Voxels.LoadAsync(Model.Voxels);
		}


	}
}
