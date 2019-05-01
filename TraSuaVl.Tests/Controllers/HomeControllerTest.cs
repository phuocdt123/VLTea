using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraSuaVl;
using TraSuaVl.Controllers;
using TraSuaVl.Models;

namespace TraSuaVl.Tests.Controllers
{
    [TestClass]
    public class VLTeaControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new CS4PEEntities();
            var controller = new VLTeaController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as List<BubleTea>;
            Assert.IsInstanceOfType(result.Model, typeof(List<BubleTea>));
            Assert.AreEqual(db.BubleTeas.Count(), (result.Model as List<BubleTea>).Count);
        }
        [TestMethod]
        public void TestEdit()
        {
            var db = new CS4PEEntities();
            var update = db.BubleTeas.First();
            var controller = new VLTeaController();
            var result = controller.Edit(update.id);
            var item = db.BubleTeas.Find(update.id);
            Assert.IsNotNull(update);
            Assert.AreEqual(update.Name, item.Name);
            Assert.AreEqual(update.Price, item.Price);
            Assert.AreEqual(update.Topping, item.Topping);
        }
        [TestMethod]
        public void TestDetails()
        {
            var db = new CS4PEEntities();
            var item = db.BubleTeas.First();
            var controller = new VLTeaController();
            var result = controller.Details(item.id);
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            var model = view.Model as BubleTea;
            Assert.IsNotNull(model);
            Assert.AreEqual(item.id, model.id);
        }
        [TestMethod]
        public void TestCreate()
        {
            var controller = new VLTeaController();
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void TestCreateP()
        {
            var db = new CS4PEEntities();
            var model = new BubleTea
            {
                Name = " Tra sua VL ",
                Price = 25000,
                Topping = "tran chau trang"
            };
            var controller = new VLTeaController();
            var result = controller.Create(model);
            var redirect = result as RedirectToRouteResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.RouteValues["action"]);
            var item = db.BubleTeas.Find(model.id);
            Assert.IsNotNull(item);
            Assert.AreEqual(model.Name, item.Name);
            Assert.AreEqual(model.Price, item.Price);
            Assert.AreEqual(model.Topping, item.Topping);
        }
    }


}
