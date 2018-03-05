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

		public DocumentViewModel(ILogger Logger) : base(Logger)
		{
			Name = "New document";
		}


	}
}
