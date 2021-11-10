using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectHotel.ViewModels
{
    public class CategoryInfoViewModel
    {

        /// <summary>
        /// Уникальный индентификатор.
        /// </summary>
        [Key]
        [Required]
        public Guid ID { get; set; }
        /// <summary>
        /// Категория.
        /// </summary>
        public virtual CategoryViewModel Category { get; set; }
        public Guid CategoryID { get; set; }
        /// <summary>
        /// Начальная дата с которой будет применятся данный ценник.
        /// </summary>
        [Required]
        public DateTime PriceAtTheMomentStart { get; }
        /// <summary>
        /// Конечная дата по которую будут применятся данный ценник.
        /// </summary>

        public DateTime? PriceAtTheMomentEnd { get; set; }
        /// <summary>
        /// Цена номера за сутки.
        /// </summary>
        [Display(Name = "Цена номера за сутки")]
        [Required(ErrorMessage = "Поле \"цена номера за сутки\" обязательно к заполнению!")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public CategoryInfoViewModel()
        {
            this.ID = Guid.NewGuid();
            PriceAtTheMomentStart = DateTime.Now;
            PriceAtTheMomentEnd = null;
        }
    }
}
