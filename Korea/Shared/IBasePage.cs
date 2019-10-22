using SharedModels;
using System.Collections.Generic;

namespace Korea.Shared
{
    public interface IBasePage
    {
		bool HasHeader { get; set; }
        void AddBreadcrump(List<Breadcrumb> breadcrumbs);

    }
}