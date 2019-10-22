using SharedModels;
using System.Collections.Generic;

namespace Korea.Core
{
    public interface IBasePage
    {
		bool HasHeader { get; set; }
        void AddBreadcrump(List<Breadcrumb> breadcrumbs);

    }
}