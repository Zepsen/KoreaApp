using Microsoft.AspNetCore.Components;

namespace Korea.Shared
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