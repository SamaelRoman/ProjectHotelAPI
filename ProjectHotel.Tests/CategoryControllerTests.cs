using Moq;
using ProjectHotel.BLL.DTO;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.Controllers;
using ProjectHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace ProjectHotel.Tests
{
    public class CategoryControllerTests
    {
        private readonly ITestOutputHelper _Output;
        private List<CategoryDTO> categoryDTO = new List<CategoryDTO>
        {
            new CategoryDTO()
            {
                ID = new Guid("655fa565-2a6e-4170-a639-e90cc2d88e48"),
                Capacity = 2,
                Title = "Lux",
                CategoryInfos = new List<CategoryInfoDTO>()
                {
                    new CategoryInfoDTO()
                    {
                        ID = new Guid("9715175d-a3ee-441c-9aba-a25652c76f66"),
                        Price = 2000,
                        CategoryID = new Guid("655fa565-2a6e-4170-a639-e90cc2d88e48"),
                    }
                }
            },
        };
        private List<CategoryViewModel> categoryViewModel = new List<CategoryViewModel>
        {
            new CategoryViewModel()
            {
                ID = new Guid("655fa565-2a6e-4170-a639-e90cc2d88e48"),
                Capacity = 2,
                Title = "Lux",
                CategoryInfos = new List<CategoryInfoViewModel>()
                {
                    new CategoryInfoViewModel()
                    {
                        ID = new Guid("9715175d-a3ee-441c-9aba-a25652c76f66"),
                        Price = 2000,
                        CategoryID = new Guid("655fa565-2a6e-4170-a639-e90cc2d88e48"),
                    }
                }
            },
        };
        public CategoryControllerTests(ITestOutputHelper _Output)
        {
            this._Output = _Output;
        }
        [Fact]
        public void Get_WithoutParams_CategoryCollection()
        {
            //Arrange
            int Counter = 0;
            Mock<ICategoryService> CategoryServiceMock = new Mock<ICategoryService>();
            CategoryServiceMock.Setup(CS => CS.Get()).Returns(categoryDTO);
            CategoryController categoryController = new CategoryController(CategoryServiceMock.Object);
            //Act
            var Result = categoryController.Get() as List<CategoryViewModel>;
            //Assert
            Assert.IsType<List<CategoryViewModel>>(Result);
            Assert.Equal(categoryViewModel.Count, Result.Count);

            foreach(var C in Result)
            {
                Assert.Equal(categoryDTO.ElementAt(Counter).ID, C.ID);
                Assert.Equal(categoryDTO.ElementAt(Counter).Title, C.Title);
                Assert.Equal(categoryDTO.ElementAt(Counter).Capacity, C.Capacity);
                Counter++;
            }
        }
        [Fact]       
        public void Get_WithNullParam_ArgumentNullException()
        {
            //Arrange
            Mock<ICategoryService> CategoryServiceMock = new Mock<ICategoryService>();
            CategoryController categoryController = new CategoryController(CategoryServiceMock.Object);

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>( () => { var result = categoryController.Get(null); });
        }
        [Fact]
        public void Get_ExsistID_Category()
        {
            //Arrange
            Guid ID = new Guid("655fa565-2a6e-4170-a639-e90cc2d88e48");
            Mock<ICategoryService> CategoryServiceMock = new Mock<ICategoryService>();
            CategoryServiceMock.Setup(CS => CS.Get(ID)).Returns(categoryDTO.First(C => C.ID == ID));
            CategoryController categoryController = new CategoryController(CategoryServiceMock.Object);

            //Act
            var Result = categoryController.Get("655fa565-2a6e-4170-a639-e90cc2d88e48");
            //Assert
            Assert.IsType<CategoryViewModel>(Result);
            Assert.Equal(ID, Result.ID);

        }
    }
}
