using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportStore.Domain.Abstract;
using Moq;
using SportStore.Domain.Entities;
using System.Linq;
using SportStore.WebUI.Controllers;

namespace SportStore.UnitTests
{
    [TestClass]
    public class Admins
    {
        [TestMethod]
        public void Can_Edit_Product()
        {
            //Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m=>m.Products).Returns(new Product[] {
                new Product {ProductID=1,Name="P1" },
                new Product {ProductID=2,Name="P2" },
                new Product {ProductID=3,Name="P3" }
            }.AsQueryable());

            //Arrange - create the controller
            AdminController target = new AdminController(mock.Object);

            //Act
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            //Assert
            Assert.AreEqual(1,p1.ProductID);
            Assert.AreEqual(2,p2.ProductID);
            Assert.AreEqual(3,p3.ProductID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            //Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m=>m.Products).Returns(new Product[] 
            {
                new Product {ProductID=1,Name="P1" },
                new Product { ProductID=2,Name="P2"},
                new Product { ProductID=3,Name ="P3"},
            }.AsQueryable);

            //Arrange - create the controller 
            AdminController target = new AdminController(mock.Object);

            //Act
            Product result = (Product)target.Edit(4).ViewData.Model;
            //Assert
            Assert.IsNull(result);
        }
    }
}
