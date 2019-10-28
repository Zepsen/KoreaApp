using SharedModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Korea.Shared
{
    public interface IBasePage
    {
		bool HasHeader { get; set; }
        void AddBreadcrump(List<Breadcrumb> breadcrumbs);
        Task ShowError(string message);

    }
}