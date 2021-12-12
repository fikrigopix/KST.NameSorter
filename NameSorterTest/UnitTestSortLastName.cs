using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameSorter.Services;
using NameSorter.Services.Interface;
using System.Collections.Generic;

namespace NameSorterTest
{
    [TestClass]
    public class UnitTestSortLastName
    {
        [TestMethod]
        public void Test_SortingMethod()
        {
            List<string> input = new List<string>(new string[] { 
                                    "Orson Milka Iddins",
                                    "Erna Dorey Battelle",
                                    "Flori Chaunce Franzel",
                                    "Odetta Sue Kaspar",
                                    "Roy Ketti Kopfen",
                                    "Madel Bordie Mapplebeck",
                                    "Selle Bellison",
                                    "Leonerd Adda Mitchell Monaghan",
                                    "Debra Micheli",
                                    "Hailey Avie Annakin"
                                    });

            List<string> expectedResult = new List<string>(new string[] {
                                    "Hailey Avie Annakin",
                                    "Erna Dorey Battelle",
                                    "Selle Bellison",
                                    "Flori Chaunce Franzel",
                                    "Orson Milka Iddins",
                                    "Odetta Sue Kaspar",
                                    "Roy Ketti Kopfen",
                                    "Madel Bordie Mapplebeck",
                                    "Debra Micheli",
                                    "Leonerd Adda Mitchell Monaghan"
                                    });


            ISortByLastNameService lastName = new SortByLastNameService();

            List<string> listStringActual = lastName.Sorting(input);

            CollectionAssert.AreEqual(expectedResult, listStringActual, "Sorting by last name failed");
        }
    }
}
