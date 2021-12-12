using NameSorter.Services.Interface;
using System.Collections.Generic;
using System.Linq;

namespace NameSorter.Services
{
    public class SortByLastNameService : ISortByLastNameService
    {
        public List<string> Sorting(List<string> stringParam)
        {
            stringParam.Sort((n1, n2) => n1.Split(" ").Last().CompareTo(n2.Split(" ").Last()));
            return stringParam;
        }
    }
}
