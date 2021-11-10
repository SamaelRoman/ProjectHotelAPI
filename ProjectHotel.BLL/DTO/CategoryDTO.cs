using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ProjectHotel.BLL.DTO
{
    public class CategoryDTO
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
        public virtual ICollection<CategoryInfoDTO> CategoryInfos { get; set; }
        /// <summary>
        /// Список номеров пренадлежащих к категории.
        /// </summary>
        public virtual ICollection<RoomDTO> Rooms { get; set; }
        public CategoryDTO()
        {
            this.ID = Guid.NewGuid();
            Rooms = new List<RoomDTO>();
            CategoryInfos = new List<CategoryInfoDTO>();
        }
    }
}
