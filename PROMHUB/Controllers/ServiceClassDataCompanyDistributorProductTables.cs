﻿using System.Collections.Generic;
using System.Linq;
using global::PROMHUB.Data;
using Microsoft.EntityFrameworkCore;
using PROMHUB.Data.Models;

namespace PROMHUB.Controllers
{
    public class ServiceClassDataCompanyDistributorProductTables
    {
        private readonly AppDbContext _context;
        private readonly ImageService _imageService; // Внедряем службу ImageService

        public ServiceClassDataCompanyDistributorProductTables(AppDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService; // Инициализируем службу ImageService
        }

        public IEnumerable<CombinedDataCompanyDistributorProductTables> GetCombinedData()
        {
            var combinedData = from product in _context.Product
                               join productDistributor in _context.ProductDistributor on product.Id equals productDistributor.ProductId
                               join distributor in _context.Distributor on productDistributor.DistributorId equals distributor.Id
                               join company in _context.Company on distributor.CompanyId equals company.Id
                               join companyInfo in _context.CompanyInfo on company.Id equals companyInfo.CompanyId
                               select new CombinedDataCompanyDistributorProductTables
                               {
                                   ProductId = product.Id,
                                   ProductName = product.Name,
                                   ProductPrice = product.Price,
                                   ProductPhoto = _imageService.GetImageUrl(product.Photo),
                                   ProductDiscount = product.Discount,
                                   DistributorId = distributor.Id,
                                   DistributorAddress = distributor.AddressString,
                                   DistributorRating = distributor.Rating,
                                   CompanyId = company.Id,
                                   CompanyName = company.Name,
                                   CompanyDescription = companyInfo.Description,
                                   CompanyContactPhone = companyInfo.ContactPhone,
                                   CompanyContactEmail = companyInfo.ContactEmail,
                                   CompanyPhoto = _imageService.GetImageUrl(companyInfo.Photo)
                               };

            return combinedData.ToList();
        }
    }
}

