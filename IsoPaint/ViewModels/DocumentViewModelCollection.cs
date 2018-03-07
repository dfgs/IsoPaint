using IsoPaint.Models;
using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;

namespace IsoPaint.ViewModels
{
	public class DocumentViewModelCollection : ListViewModelCollection< DocumentViewModel, Document>
	{
		public DocumentViewModelCollection(ILogger Logger) : base(Logger)
		{
		}


		protected override Document OnCreateModel()
		{
			return new Document() {SizeX=8,SizeY=8,SizeZ=8 };
		}

		protected override DocumentViewModel OnCreateViewModel(Document Model)
		{
			return new DocumentViewModel(Logger);
		}
		
		protected override IEnumerable<Document> OnLoadItems(List<Document> Model)
		{
			return Model;
		}

		
	}
}
