using System.Collections.Generic;

namespace NameSorter.Services.Interface
{
    public interface ISortByLastNameService
    {
        List<string> Sorting(List<string> stringParam);
    }
}
