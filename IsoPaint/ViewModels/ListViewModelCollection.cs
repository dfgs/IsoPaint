using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogLib;
using ViewModelLib;

namespace IsoPaint.ViewModels
{
	public abstract class ListViewModelCollection<ViewModelType, ModelType> : ViewModelCollection<List<ModelType>, ViewModelType, ModelType>
		where ViewModelType:ViewModel<ModelType>
	{
		public ListViewModelCollection(ILogger Logger) : base(Logger)
		{
		}

		protected override bool OnAreModelsAreEquals(ModelType A, ModelType B)
		{
			return ValueType.Equals(A,B);
		}

		protected override async Task OnAddModelAsync(ModelType Item)
		{
			await Dispatcher.InvokeAsync(() => { Model.Add(Item); });
		}
		protected override async Task OnRemoveModelAsync(ModelType Item)
		{
			await Dispatcher.InvokeAsync(() => { Model.Remove(Item); });
		}
		protected override async Task OnEditModelAsync(ModelType Item)
		{
			await Task.Yield();
		}


	}
}
