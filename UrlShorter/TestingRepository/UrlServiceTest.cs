using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UrlShorter.Date;
using UrlShorter.Date.Base;
using UrlShorter.Date.Services;
using UrlShorter.Models;
using Xunit;


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
            // Arrange
            var mockSet = new Mock<DbSet<UrlTable>>();
            var mockContext = new Mock<AppDbContext>();

            // Створюємо тестовий список UrlTable, який буде повертатись з методу ToList
            var testUrls = new List<UrlTable>
                {
                    new UrlTable { Id = 1, OriginalUrl = "http://example.com" },
                    new UrlTable { Id = 2, OriginalUrl = "http://test.com" }
                };

            // Налаштовуємо поведінку методу ToList для повернення тестового списку UrlTable
            mockSet.Setup(m => m.ToList()).Returns(testUrls);
            mockContext.Setup(m => m.UrlsTabl).Returns(mockSet.Object); // Исправлено: "UrlsTabl" -> "UrlsTable"

            var urlService = new UrlService(mockContext.Object);

            // Act
            var result = urlService.GetUrls();

            // Assert
            // Перевіряємо, чи метод повернув той самий список UrlTable, який ми налаштували
            Assert.Equal(testUrls, result);
        }

        [Fact]
        public void GetUrls_ShouldReturnEmptyList_WhenNoUrlsExist()
        {
            // Arrange
            var mockSet = new Mock<DbSet<UrlTable>>();
            var mockContext = new Mock<AppDbContext>();

            // Налаштовуємо поведінку методу ToList для повернення порожнього списку UrlTable (немає Urls)
            mockSet.Setup(m => m.ToList()).Returns(new List<UrlTable>());
            mockContext.Setup(m => m.UrlsTabl).Returns(mockSet.Object); // Исправлено: "UrlsTabl" -> "UrlsTable"

            var urlService = new UrlService(mockContext.Object);

            // Act
            var result = urlService.GetUrls();

            // Assert
            // Перевіряємо, чи метод повернув порожній список
            Assert.Empty(result);
        }
    }

}
