using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NameSorter.Services.Interface;
using NameSorter.Validation;

namespace NameSorter.Pages
{
    public class SortNameModel : PageModel
    {
        private readonly ISortByLastNameService _sortLastNameService;
        private readonly IWebHostEnvironment _environment;

        public SortNameModel(ISortByLastNameService sortByLastNameService,
                                IWebHostEnvironment environment)
        {
            _sortLastNameService = sortByLastNameService;
            _environment = environment;
        }

        [BindProperty]
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".txt" })]
        public IFormFile Upload { get; set; }
        public List<string> SortedNames { get; set; }

        public string fileName = "sorted-names-list.txt";

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var file = await UploadFileAsync(Upload);

                List<string> unsortedNames = System.IO.File.ReadLines(file).ToList();

                this.SortedNames = _sortLastNameService.Sorting(unsortedNames);

                SaveFileResult(this.SortedNames);
            }
        }

        private async Task<string> UploadFileAsync(IFormFile formFile)
        {
            var file = Path.Combine(_environment.ContentRootPath, "uploads", formFile.FileName);

            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return file;
        }

        private void SaveFileResult(List<string> content)
        {
            var docPath = Path.Combine(_environment.WebRootPath, "sortedFiles", fileName);
            System.IO.File.Delete(docPath);
            System.IO.File.AppendAllLines(docPath, content);
        }


        public FileResult OnPostDownloadFile()
        {
            //Build the File Path.
            string path = Path.Combine(_environment.WebRootPath, "sortedFiles/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
