using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportStore.Domain.Entities;
using System.Linq;
using SportStore.Domain.Abstract;
using SportStore.WebUI.Controllers;

namespace SportStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //Arrange - create some tests products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            //Arrange - create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1,10);
            CartLine[] results = target.Lines.OrderBy(p=>p.Product.ProductID).ToArray();

            //Assert
            Assert.AreEqual(results.Length,2);
            Assert.AreEqual(results[0].Quanity,11);
            Assert.AreEqual(results[1].Quanity,1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            //Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            //Arrange - create a new cart
            Cart target = new Cart();

            //Arrange - add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            //Act
            target.RemoveLine(p2);

            //Assert
            Assert.AreEqual(target.Lines.Where(c=>c.Product == p2).Count(),0);
            Assert.AreEqual(target.Lines.Count(),2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            //Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1",Price=100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 200M };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 300M };

            //Arrange - create test cart
            Cart target = new Cart();
            
            //Act
            target.AddItem(p1,1);
            target.AddItem(p2,2);
            target.AddItem(p3,1);
            decimal result = target.ComputeTotalValue();

            //Assert
            Assert.AreEqual(result,800M);
           
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            //Arrange - create some test products
            Product p1 = new Product {ProductID=1,Name="P1",Price=100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            //Arrange - create a new cart
            Cart target = new Cart();

            //Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            //Act - reset the cart
            target.Clear();
            //Assert
            Assert.AreEqual(target.Lines.Count(),0);

        }

        //[TestMethod]
        //public void Can__Add_To_Cart()
        //{
        //    //Arrange-create the mock repository
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m=>m.Products).Returns(new Product []{ new Product { ProductID = 1, Name = "P1", Category = "Apples" } }.AsQueryable());

        //    //Arrange - create a Cart
        //    Cart cart = new Cart();

        //    //Arrange - create the controller
        //    CartController target = new CartController(mock.Object);

        //    //Act-add a product to the cart
        //    target.AddToCart(cart,1,null);

        //    //Assert
        //    Assert.AreEqual(cart.Lines.Count(),1);
        //    Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID,1);
        //}
    }
}
