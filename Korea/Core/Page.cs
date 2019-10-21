using Microsoft.AspNetCore.Components;

namespace Korea.Core
{
	
	public abstract class Page : ComponentBase
	{
		[CascadingParameter]
		protected IBasePage BasePage { get; set; }

		protected override void OnInitialized()
		{			
			Breadcrumbs();
		}

		protected abstract void Breadcrumbs();

	}
}