using BarayeAzadi.Application.Common.Interfaces;
using BarayeAzadi.Application.Common.Utility;
using BarayeAzadi.Application.Services.Interface;
using BarayeAzadi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BarayeAzadi.Application.Services.Implementation
{
    
    public class StatementService : IStatementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StatementService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public void CreateStatement(Statement statement)
        {
            if (statement.Image is not null)
            {

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(statement.Image.FileName);
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Static_Files\Docs\Statements\Images");

                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);

                statement.Image.CopyTo(fileStream);
                statement.ImageUrl = @"Static_Files\Docs\Statements\Images\" + fileName;

            }
            if (statement.Pdf is not null)
            {

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(statement.Pdf.FileName);
                string PdfPath = Path.Combine(_webHostEnvironment.WebRootPath, @"Static_Files\Docs\Statements");

                using var fileStream = new FileStream(Path.Combine(PdfPath, fileName), FileMode.Create);

                statement.Pdf.CopyTo(fileStream);
                statement.PdfUrl = @"Static_Files\Docs\Statements\" + fileName;

            }
            statement.Created_Date = DateOnly.FromDateTime(DateTime.Now);

            _unitOfWork.Statement.Add(statement);
            _unitOfWork.Save();
        }

        public IEnumerable<Statement> GetAllStatement()
        {
            IEnumerable<Statement> objFromDb = _unitOfWork.Statement.GetAll().OrderByDescending(u => u.StatementId);

            return objFromDb;
        }

        public Statement GetStatementById(int id)
        {
            try
            {
                Statement? objFromDb = _unitOfWork.Statement.Get(u => u.StatementId == id);

                if (objFromDb is not null)
                {
                    return objFromDb;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateStatement(Statement statement)
        {
            if (statement.Image is not null)
            {

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(statement.Image.FileName);
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Static_Files\Docs\Statements\Images");

                if (statement.ImageUrl != "Static_Files\\Docs\\Statements\\Images\\default.jpg" && statement.ImageUrl is not null)
                {
                   
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, statement.ImageUrl);

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    
                }

                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);

                statement.Image.CopyTo(fileStream);
                statement.ImageUrl = @"Static_Files\Docs\Statements\Images\" + fileName;

            }

            if (statement.Pdf is not null)
            {

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(statement.Pdf.FileName);
                string PdfPath = Path.Combine(_webHostEnvironment.WebRootPath, @"Static_Files\Docs\Statements");

                if (statement.PdfUrl != "Static_Files\\Docs\\Statements\\default.pdf")
                {
                    var oldPdfPath = Path.Combine(_webHostEnvironment.WebRootPath, statement.PdfUrl);

                    if (System.IO.File.Exists(oldPdfPath))
                    {
                        System.IO.File.Delete(oldPdfPath);
                    }
                }

                using var fileStream = new FileStream(Path.Combine(PdfPath, fileName), FileMode.Create);

                statement.Pdf.CopyTo(fileStream);
                statement.PdfUrl = @"Static_Files\Docs\Statements\" + fileName;

            }

            _unitOfWork.Statement.Update(statement);
            _unitOfWork.Save();
        }
        public bool DeleteStatement(int id)
        {
            try
            {
                Statement? objFromDb = _unitOfWork.Statement.Get(u => u.StatementId == id);

                if (objFromDb is not null)
                {
                    if (!string.IsNullOrEmpty(objFromDb.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    if (!string.IsNullOrEmpty(objFromDb.PdfUrl))
                    {
                        var oldPdfPath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.PdfUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldPdfPath))
                        {
                            System.IO.File.Delete(oldPdfPath);
                        }
                    }
                    if (!string.IsNullOrEmpty(objFromDb.MediaUrl))
                    {
                        var oldMediaPath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.MediaUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldMediaPath))
                        {
                            System.IO.File.Delete(oldMediaPath);
                        }
                    }
                    _unitOfWork.Statement.Remove(objFromDb);
                    _unitOfWork.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}
