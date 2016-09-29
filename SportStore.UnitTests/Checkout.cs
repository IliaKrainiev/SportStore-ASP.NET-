using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportStore.Domain.Abstract;
using Moq;
using SportStore.Domain.Entities;
using SportStore.WebUI.Controllers;
using System.Web.Mvc;

namespace SportStore.UnitTests
{
    [TestClass]
    public class Checkout
    {
        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            //Arrange - create an empty cart
            Cart cart = new Cart();

            //Arrange - create shipping details
            ShippingDetails shippingDetails = new ShippingDetails();

            //Arrnage - create an instance of the controller
            CartController target = new CartController(null,mock.Object);

            //Act
            ViewResult result = target.Checkout(cart,shippingDetails);

            //Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m=>
            m.ProcessOrder(It.IsAny<Cart>(),It.IsAny<ShippingDetails>()),Times.Never);

            //Assert - check that the method is returning the default view
            Assert.AreEqual("",result.ViewName);

            //Assert - check that we are passing an invalid model to the view
            Assert.AreEqual(false,result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            //Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            //Arrange - create a cart with an item
            Cart cart = new Cart();
            cart.AddItem(new Product(),1);

            //Arrange - create an instance of the controlller
            CartController target = new CartController(null,mock.Object);

            //Arrange - add an error to the model 
            target.ModelState.AddModelError("error","error");

            //Act - try to checkout 
            ViewResult result = target.Checkout(cart,new ShippingDetails());

            //Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m=>
            m.ProcessOrder(It.IsAny<Cart>(),It.IsAny<ShippingDetails>()),Times.Never());

            //Assert - check that the method is returning the default view 
            Assert.AreEqual("",result.ViewName);

            //Assert - check thatwe are passing an invalid model to the view
            Assert.AreEqual(false,result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            //Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            //Arrange - create a cart with an item 
            Cart cart = new Cart();
            cart.AddItem(new Product(),1);

            //Arrange - create an instance of the controller
            CartController target = new CartController(null,mock.Object);

            //Act - try to checkout
            ViewResult result = target.Checkout(cart,new ShippingDetails());

            //Assert- check that the order has been passed on to the processor
            mock.Verify(m =>
            m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once);

            //Assert - check that the method is returning the Completed view
            Assert.AreEqual("Completed",result.ViewName);

            //Assert - check that we are passing a valid model to the view
            Assert.AreEqual(true,result.ViewData.ModelState.IsValid);
        }
    }
}
