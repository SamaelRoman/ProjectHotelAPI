using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ProjectHotel.BLL.DTO
{
    public class BookingInfoDTO
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        [Key]
        [Required]
        public Guid ID { get; set; }
        /// <summary>
        /// Дата начала бронирования.
        /// </summary>
        [Display(Name = "Дата заселения!")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartBooking { get; set; }
        /// <summary>
        /// Дата окончания бронирвоания.
        /// </summary>
        [Display(Name = "Дата выселения!")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndBooking { get; set; }
        /// <summary>
        /// Номер для бронирования!
        /// </summary>
        [Required]
        public virtual RoomDTO Room { get; set; }
        public string RoomID { get; set; }
        /// <summary>
        /// Постоялец забронировавший номер.
        /// </summary>
        [Required]
        public virtual CustomerDTO Customer { get; set; }
        public Guid CustomerID { get; set; }
        /// <summary>
        /// Количество дней бронирования.
        /// </summary>
        [NotMapped]
        public int NumberOfDays { get { return (EndBooking - StartBooking).Days; } }
        /// <summary>
        /// Итоговая цена.
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal TotalPrice{ get;set; }
        public BookingInfoDTO()
        {
            this.ID = Guid.NewGuid();
        }
        public void GetTotalPrice()
        {
            decimal result = 0;
            for (int i = 0; i < NumberOfDays; i++)
            {
                result += Room.Category.CategoryInfos.First(CI => CI.PriceAtTheMomentStart <= StartBooking.AddDays(i) && (CI.PriceAtTheMomentEnd >= StartBooking.AddDays(i) || CI.PriceAtTheMomentEnd == null)).Price;
            }
            this.TotalPrice = result;
        }
    }
}
