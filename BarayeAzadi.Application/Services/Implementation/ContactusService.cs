﻿using BarayeAzadi.Application.Common.Interfaces;
using BarayeAzadi.Application.Services.Interface;
using BarayeAzadi.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarayeAzadi.Application.Services.Implementation
{
    public class ContactusService : IContactusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ContactusService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public void CreateContactus(Contactus contactus)
        {
            if (contactus.AttachedFile is not null)
            {

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(contactus.AttachedFile.FileName);
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Static_Files\Contactus-Attached-Files");

                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);

                contactus.AttachedFile.CopyTo(fileStream);
                contactus.AttachedFileUrl = @"Static_Files\Contactus-Attached-Files\" + fileName;

            }
            contactus.Created_Date = DateTime.Now;

            _unitOfWork.Contactus.Add(contactus);
            _unitOfWork.Save();
        }

        public IEnumerable<Contactus> GetAllContactuss()
        {
            IEnumerable<Contactus> objFromDb = _unitOfWork.Contactus.GetAll().OrderByDescending(u=>u.ContactusId);

            return objFromDb;
        }
        public bool DeleteContactus(Contactus contactus)
        {
            try
            {
                //Contactus? objFromDb = _unitOfWork.Contactus.Get(u => u.ContactusId == id);

                if (contactus is not null)
                {
                    _unitOfWork.Contactus.Remove(contactus);
                    _unitOfWork.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Contactus GetContactusById(int id)
        {
            try
            {
                Contactus? objFromDb = _unitOfWork.Contactus.Get(u => u.ContactusId == id);

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
    }
}
