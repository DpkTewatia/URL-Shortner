using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UrlShorter.Date;
using UrlShorter.Date.Base;
using UrlShorter.Date.Services;
using UrlShorter.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace TestingRepository
{

    public class UrlServiceTest : IClassFixture<AppDbContext>
    {
        private readonly AppDbContext _context;
        public UrlServiceTest(AppDbContext context)
        {
            _context = context;
        }

        [Fact]
        public void GetUrls_ShouldReturnListOfUrls()
        {

            var mockSet = new Mock<DbSet<UrlTable>>();
            var mockContext = new Mock<AppDbContext>();


            var testUrls = new List<UrlTable>
                {
                    new UrlTable { Id = 1, OriginalUrl = "http://example.com" },
                    new UrlTable { Id = 2, OriginalUrl = "http://test.com" }
                };


            mockSet.Setup(m => m.ToList()).Returns(testUrls);
            mockContext.Setup(m => m.UrlsTabl).Returns(mockSet.Object);

            var urlService = new UrlService(mockContext.Object);


            var result = urlService.GetUrls();


            Assert.Equal(testUrls, result);
        }

        [Fact]
        public void GetUrls_ShouldReturnEmptyList_WhenNoUrlsExist()
        {

            var mockSet = new Mock<DbSet<UrlTable>>();
            var mockContext = new Mock<AppDbContext>();


            mockSet.Setup(m => m.ToList()).Returns(new List<UrlTable>());
            mockContext.Setup(m => m.UrlsTabl).Returns(mockSet.Object);

            var urlService = new UrlService(mockContext.Object);


            var result = urlService.GetUrls();


            Assert.Empty(result);
        }


        [Fact]
        public void DeleteUrl_ShouldRemoveUrl_WhenUrlExists()
        {

            var urlIdToDelete = 1;
            var testUrls = new List<UrlTable>
            {
                new UrlTable { Id = 1, OriginalUrl = "http://example.com" },
                new UrlTable { Id = 2, OriginalUrl = "http://test.com" }
            };

            var mockSet = new Mock<DbSet<UrlTable>>();
            mockSet.As<IQueryable<UrlTable>>().Setup(m => m.Provider).Returns(testUrls.AsQueryable().Provider);
            mockSet.As<IQueryable<UrlTable>>().Setup(m => m.Expression).Returns(testUrls.AsQueryable().Expression);
            mockSet.As<IQueryable<UrlTable>>().Setup(m => m.ElementType).Returns(testUrls.AsQueryable().ElementType);
            mockSet.As<IQueryable<UrlTable>>().Setup(m => m.GetEnumerator()).Returns(testUrls.AsQueryable().GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.UrlsTabl).Returns(mockSet.Object);

            var urlService = new UrlService(mockContext.Object);


            urlService.DeleteUrl(urlIdToDelete);


            mockSet.Verify(m => m.Remove(It.IsAny<UrlTable>()), Times.Once);


            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteUrl_ShouldNotRemove_WhenUrlDoesNotExist()
        {

            var urlIdToDelete = 3;
            var testUrls = new List<UrlTable>
            {
                new UrlTable { Id = 1, OriginalUrl = "http://example.com" },
                new UrlTable { Id = 2, OriginalUrl = "http://test.com" }
            };

            var mockSet = new Mock<DbSet<UrlTable>>();
            mockSet.As<IQueryable<UrlTable>>().Setup(m => m.Provider).Returns(testUrls.AsQueryable().Provider);
            mockSet.As<IQueryable<UrlTable>>().Setup(m => m.Expression).Returns(testUrls.AsQueryable().Expression);
            mockSet.As<IQueryable<UrlTable>>().Setup(m => m.ElementType).Returns(testUrls.AsQueryable().ElementType);
            mockSet.As<IQueryable<UrlTable>>().Setup(m => m.GetEnumerator()).Returns(testUrls.AsQueryable().GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.UrlsTabl).Returns(mockSet.Object);

            var urlService = new UrlService(mockContext.Object);


            urlService.DeleteUrl(urlIdToDelete);


            mockSet.Verify(m => m.Remove(It.IsAny<UrlTable>()), Times.Never);


            mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }
       
       
    }

}
