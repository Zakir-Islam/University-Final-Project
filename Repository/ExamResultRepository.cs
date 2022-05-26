using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ExcelDataReader;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class ExamResultRepository : IExamResultRepository
    {
        private readonly ExamContext examContext;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleManager<IdentityRole> RoleManager { get; }

        public ExamResultRepository(ExamContext examContext, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.examContext = examContext;
            this.userManager = userManager;
            RoleManager = roleManager;
        }

        public async Task<ExamResult> AddExamResultAsync(ExamResult ExamResult, IFormFile file, int semester)
        {
            var result = string.Empty;
            string worksheetsName = "data";

            bool firstRowIsHeader = false;
            var format = new ExcelTextFormat();
            format.Delimiter = ',';
            format.TextQualifier = '"';

            var reader = new StreamReader(file.OpenReadStream());
            ExcelPackage package = new ExcelPackage();

            result = reader.ReadToEnd();
            ExcelWorksheet worksheet =
            package.Workbook.Worksheets.Add(worksheetsName);
            worksheet.Cells["A1"].LoadFromText(result, format, OfficeOpenXml.Table.TableStyles.Medium27, firstRowIsHeader);



            int rowCount = worksheet.Dimension.End.Row;
            int colCount = worksheet.Dimension.End.Column;
            int col = 0;
            List<ExamResult> examresults = new List<ExamResult>();
            for (int row = 2; row <= rowCount; row++)
            {


                col = 1;



                var val1 = worksheet.Cells[row, col].Value;
                ++col;
                var val2 = worksheet.Cells[row, col].Value;
            
                if (val1 != null && val2 != null)
                {
                    examresults.Add(new ExamResult()
                    {

                        Student_FId = val1.ToString(),
                        Marks = (double)val2

                    });
                }


            }


            foreach (var item in examresults)
            {
                item.subject_FId = ExamResult.subject_FId;
                item.semester = semester;
                await examContext.ExamResults.AddAsync(item);
                await examContext.SaveChangesAsync();

            }
            return ExamResult;

        }

        public async Task<List<ExamResult>> GetAllExamResultsAsync(string searchString)
        {
            var examResults = await examContext.ExamResults.ToListAsync();

            if (searchString != null)
            {
                var results = await examContext.ExamResults.
                    Where(e => e.Student_FId == searchString)
                  .OrderBy(e => e.semester).ToListAsync();
            }

            return examResults;
        }

        public async Task<ExamResult> GetExamResultAsync(int Id)
        {
            var ExamResult = await examContext.ExamResults.FindAsync(Id);

            return ExamResult;

        }

        public async Task<ExamResult> DeleteExamResultAsync(int id)
        {
            var ExamResult = examContext.ExamResults.Find(id);
            examContext.ExamResults.Remove(ExamResult);
            await examContext.SaveChangesAsync();
            return ExamResult;
        }

        public async Task<ExamResult> UpdateExamResultAsync(ExamResult ExamResult,int semester)
        {
            if (ExamResult != null)
            {
    
                ExamResult.semester = semester;
              
                examContext.Update(ExamResult);
            }
         
            await examContext.SaveChangesAsync();

            return ExamResult;
        }

        public async Task<ExamResult> FindExamResultAsync(int id)
        {
            var ExamResult = await examContext.ExamResults.FindAsync(id);


            return ExamResult;
        }

        public async Task<List<ExamResult>> StudentTranscriptAsync(string std_id)
        {
            return await examContext.ExamResults.Where(s=>s.Student_FId==std_id).Include(s => s.Student_FId)
                .Include(sub => sub.subject_FId).ToListAsync();
        }



    }
}
