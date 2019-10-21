using System.Collections.Generic;
using Korea.Shared.Models;

namespace Korea.Core
{
    public interface IBasePage
    {
		bool HasHeader { get; set; }
        void AddBreadcrump(List<Breadcrumb> breadcrumbs);

    }
}