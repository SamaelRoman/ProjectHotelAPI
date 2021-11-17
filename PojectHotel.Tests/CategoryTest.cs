using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectHotel.BLL.DTO;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.Controllers;
using ProjectHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PojectHotel.Tests
{
    [TestClass]
    public class CategoryTest
    {
        private List<CategoryDTO> CategoryDTOlist = new List<CategoryDTO>()
        {
            new CategoryDTO{
                ID = new System.Guid("2941df24-3075-4c02-adf2-ac4145c785cb"),
                Capacity = 2,
                Title = "Люкс",
                CategoryInfos = new List<CategoryInfoDTO>()
                {
                    new CategoryInfoDTO
                    {
                        ID = new System.Guid("afc45c4b-1489-4eaf-8238-d45f2cdf3e92"),
                        CategoryID = new System.Guid("2941df24-3075-4c02-adf2-ac4145c785cb"),
                        Price = 4000
                    }
                }      
            },
        };
        private List<CategoryViewModel> CategoryViewModellist = new List<CategoryViewModel>
        {
            new CategoryViewModel{
                ID = new System.Guid("2941df24-3075-4c02-adf2-ac4145c785cb"),
                Capacity = 2,
                Title = "Люкс",
                CategoryInfos = new List<CategoryInfoViewModel>()
                {
                    new CategoryInfoViewModel
                    {
                        ID = new System.Guid("afc45c4b-1489-4eaf-8238-d45f2cdf3e92"),
                        CategoryID = new System.Guid("2941df24-3075-4c02-adf2-ac4145c785cb"),
                        Price = 4000
                    }
                }
            },
        };

        [TestMethod]
        public void Get_Test()
        {
            //Arrange
            Mock<ICategoryService> CategoryServiceMock = new Mock<ICategoryService>();
            CategoryServiceMock.Setup(CategoryService => CategoryService.Get()).Returns(CategoryDTOlist);
            var Category_Controller = new CategoryController(CategoryServiceMock.Object);

            //Act
            var result = (List<CategoryViewModel>)Category_Controller.Get();

            //Assert
            Assert.AreEqual(CategoryViewModellist.Count, result.Count,"Несоответствие в колличестве элементов!");
            int Counter = 0;
            foreach (var C in result)
            {
                Assert.AreEqual(CategoryViewModellist.ElementAt(Counter).ID, result.ElementAt(Counter).ID);
                Counter++;
            }
        }
        [TestMethod]
        public void Get_WithParam_Test()
        {
            //Arrange
            string ID = "2941df24-3075-4c02-adf2-ac4145c785cb";
            Mock<ICategoryService> CategoryServiceMock = new Mock<ICategoryService>();
            CategoryServiceMock.Setup(CategoryService => CategoryService.Get(new Guid(ID))).Returns(CategoryDTOlist.FirstOrDefault(C=>C.ID == new Guid(ID)));
            var Category_Controller = new CategoryController(CategoryServiceMock.Object);

            //Act
            var result = Category_Controller.Get(ID);

            //Assert
            Assert.AreEqual(CategoryViewModellist.FirstOrDefault(C => C.ID == new Guid(ID)).ID, result.ID,"ID элементов не идентичны!");
            Assert.AreEqual(CategoryViewModellist.FirstOrDefault(C => C.ID == new Guid(ID)).Title, result.Title, "Title элементов не идентичны!");

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_WithParamNull_Test()
        {
            //Arrange
            string ID = null;
            Mock<ICategoryService> CategoryServiceMock = new Mock<ICategoryService>();
            var Category_Controller = new CategoryController(CategoryServiceMock.Object);
            //Act
            //Assert
            var result = Category_Controller.Get(ID);
        }
        [TestMethod]
        public void Get_WithParamNotContains_Test()
        {
            //Arrange
            string ID = "2941df24-1275-4c02-ade2-ac4145c785cb";//this ID not contains in DB
            Mock<ICategoryService> CategoryServiceMock = new Mock<ICategoryService>();
            CategoryServiceMock.Setup(Service => Service.Get(new Guid(ID))).Returns(CategoryDTOlist.FirstOrDefault(C=>C.ID == new Guid(ID)));
            var Category_Controller = new CategoryController(CategoryServiceMock.Object);
            //Act
            //Assert
            var result = Category_Controller.Get(ID);
            Assert.IsNull(result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Post_WithNullParam_Test()
        {
            //Arrange
            Mock<ICategoryService> CategoryServiceMock = new Mock<ICategoryService>();
            var Category_Controller = new CategoryController(CategoryServiceMock.Object);
            ///Act
            ///Assert
            Category_Controller.Post(null);
        }
        [TestMethod]
        public void Post_WithNotValidParamTest()
        {
            //Arrange
            Mock<ICategoryService> CategoryServiceMock = new Mock<ICategoryService>();
            var Category_Controller = new CategoryController(CategoryServiceMock.Object);
            CategoryServiceMock.Setup(S => S.Add(new CategoryDTO()));
            CategoryViewModel category = new CategoryViewModel(); 
            Category_Controller.ModelState.AddModelError("Title", "Requried");
            ///Act
            try
            {
                Category_Controller.Post(category);
                Debug.WriteLine("1");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("0" + ex);
                throw;
            }
           
            ///Assert
            
        }

    }
}
