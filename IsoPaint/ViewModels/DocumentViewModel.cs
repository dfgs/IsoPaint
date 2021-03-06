﻿using IsoPaint.Models;
using LogLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using ViewModelLib;
using ViewModelLib.Attributes;

namespace IsoPaint.ViewModels
{
	public class DocumentViewModel : ViewModel<Document>
	{

		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(DocumentViewModel));
		public string Name
		{
			get { return (string)GetValue(NameProperty); }
			private set { SetValue(NameProperty, value); }
		}


		public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(DocumentViewModel));
		public string FileName
		{
			get { return (string)GetValue(FileNameProperty); }
			private set { SetValue(FileNameProperty, value); }
		}

		[Editable(Header="Size X",IsMandatory =true)]
		public int SizeX
		{
			get { return Model.SizeX; }
			set { Model.SizeX = value; OnPropertyChanged(); }
		}
		[Editable(Header = "Size Y", IsMandatory = true)]
		public int SizeY
		{
			get { return Model.SizeY; }
			set { Model.SizeY = value; OnPropertyChanged(); }
		}
		[Editable(Header = "Size Z", IsMandatory = true)]
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


		public static readonly DependencyProperty HoverFaceProperty = DependencyProperty.Register("HoverFace", typeof(Faces), typeof(DocumentViewModel));
		public Faces HoverFace
		{
			get { return (Faces)GetValue(HoverFaceProperty); }
			set { SetValue(HoverFaceProperty, value); }
		}



		public VoxelViewModelCollection Voxels
		{
			get;
			private set;
		}

		public PaletteViewModel Palette
		{
			get;
			private set;
		}

		public DocumentViewModel(ILogger Logger) : base(Logger)
		{
			Name = "New document";
			Voxels = new VoxelViewModelCollection(Logger,this);
			Palette = new PaletteViewModel(Logger);
		}

		protected override async Task OnLoadedAsync(Document Model)
		{
			await Voxels.LoadAsync(Model.Voxels);
			Voxels.Ceiling = SizeZ;
			await Palette.LoadAsync(Model.Palette);
		}

		public async Task LoadAsync(string FileName)
		{
			
			Document model;

			this.FileName = FileName;
			this.Name = System.IO.Path.GetFileNameWithoutExtension(FileName);

			model=await Dispatcher.InvokeAsync<Document>(() => { return Document.LoadFromFile(FileName); });
			await LoadAsync(model);
		}


		public async Task SaveToFileAsync(string FileName)
		{
			this.FileName = FileName;
			this.Name = System.IO.Path.GetFileNameWithoutExtension(FileName);
			await SaveAsync();
		}

		public async Task SaveAsync()
		{
			await Dispatcher.InvokeAsync(() => { Model.SaveToFile(FileName); });
		}

		public async Task ExportToPOVFileAsync(string FileName)
		{
			await Dispatcher.InvokeAsync(() => { Model.ExportToPOV(FileName); });
		}




	}
}
