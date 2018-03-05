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
	public class DocumentViewModelCollection : ViewModelCollection<List<Document>, DocumentViewModel, Document>
	{
		public DocumentViewModelCollection(ILogger Logger) : base(Logger)
		{
		}


		protected override Document OnCreateModel()
		{
			return new Document();
		}

		protected override DocumentViewModel OnCreateViewModel(Document Model)
		{
			return new DocumentViewModel(Logger);
		}

		protected override bool OnAreModelsAreEquals(Document A, Document B)
		{
			return false;
		}
		
		protected override async Task OnAddModelAsync(Document Item, int Index)
		{
			await Dispatcher.InvokeAsync(() => { Model.Insert(Index, Item); });
		}
		protected override async Task OnRemoveModelAsync(Document Item, int Index)
		{
			await Dispatcher.InvokeAsync(() => { Model.RemoveAt(Index); });
		}
		protected override async Task OnEditModelAsync(Document Item, int Index)
		{
			await Task.Yield();
		}
		
		protected override IEnumerable<Document> OnLoadItems(List<Document> Model)
		{
			return Model;
		}



	}
}
