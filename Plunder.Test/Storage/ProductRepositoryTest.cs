using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plunder.Plugin.Storage.Models;
using Plunder.Plugin.Storage.Repositories;

namespace Plunder.Test.Storage
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void ProductInsertTest()
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Title = "test_title",
                Description = "test_description",
                Price = 0.1,
                PicUri = "http://jd.com/2323.jpg",
                Uri = "http://jd.com/2323.html",
                CommentCount = 1,
                SiteName = "京东",
                SiteDomain = "www.jd.com",
                ElapsedSecond = 1,
                Downloader = "unit_test",
                CreateTime = DateTime.Now
            };
            var repo = new ProductRepository();

            repo.Insert(product);
        }
    }
}
