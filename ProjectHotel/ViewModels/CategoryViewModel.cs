using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ProjectHotel.ViewModels
{
    public class CategoryViewModel
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        [Key]
        [Required]
        public Guid ID { get; set; }
        /// <summary>
        /// Заголовок категории.
        /// </summary>
        [Display(Name = "Заголовок категории")]
        [Required(ErrorMessage = "Поле \"Заголовок категории\" должно быть заполненно!")]
        [StringLength(45, MinimumLength = 1, ErrorMessage = "Поле \"Заголовок категории\" Должно содержать от 1го до 45ти символов!")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        /// <summary>
        /// Вместимость номеров, на прибывание какого количества постояльцев расчитан.
        /// </summary>
        [Display(Name = "Вместимость номеров")]
        [Range(minimum: 1, maximum: 4, ErrorMessage = "Вместимость номеров может иметь значение от 1го человека до 4х!")]
        [Required(ErrorMessage = "Поле \"вместимость номеров\" обязательно к заполнению!")]
        public int Capacity { get; set; }
        /// <summary>
        /// Коллекция объектов CategoryInfo, в которых содержатся ценники на определенный период времени.
        /// </summary>
        [Required]
        public virtual ICollection<CategoryInfoViewModel> CategoryInfos { get; set; }
        /// <summary>
        /// Фотографии категории.Вычисляемое свойство, превью фото категории это,превью случайного номера из данной категории.
        /// </summary>
        [Display(Name = "Преью фотография категории")]
        [NotMapped]
        [DataType(DataType.ImageUrl)]
        public string Preview
        {
            get
            {
                if(Rooms != null && Rooms.Count >= 1)
                {
                    var rnd = new Random();
                    var index = rnd.Next(Rooms.Count - 1);
                    return Rooms.ElementAt(index).Preview;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Список номеров пренадлежащих к категории.
        /// </summary>
        public virtual ICollection<RoomViewModel> Rooms { get; set; }
        public CategoryViewModel()
        {
            this.ID = Guid.NewGuid();
            Rooms = new List<RoomViewModel>();
            CategoryInfos = new List<CategoryInfoViewModel>();
        }
    }
}
