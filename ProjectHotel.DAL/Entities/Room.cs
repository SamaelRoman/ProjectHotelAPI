 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ProjectHotel.DAL.Entities
{
    public class Room
    {
        /// <summary>
        /// Уникальный идентификатор, является номером комнаты.
        /// </summary>
        [Key]
        [Display(Name = "Номер комнаты")]
        [Required(ErrorMessage = "Поле \"номер комнаты\" обязательно к заполнению!")]
        public string ID { get; set; }
        /// <summary>
        /// Фотографии номера
        /// </summary>
        [Display(Name = "Фотографии номера")]
        [InverseProperty("Room")]
        public virtual ICollection<RoomImage> RoomImages { get; set; }
        /// <summary>
        /// Категория номера.
        /// </summary>
        [Display(Name = "Категория:")]
        public Category Category { get; set; }
        public Guid CategoryID { get; set; }
        /// <summary>
        /// Список объектов класса BookingIfo содержащих информацию о бронировании номера (Кем забронирован, на какую дату, и тд).
        /// </summary>
        public virtual ICollection<BookingInfo> BookingInfos { get; set; }
        /// <summary>
        /// Вычесляемое свойство. Возвращает ответ есть ли активные брони на данный номер.В случае наличия брони возвращает true!
        /// </summary>
        [NotMapped]
        public bool IsBooked { get { if (BookingInfos.Count > 0) { return true; } else { return false; }; } }
        public Room()
        {
            RoomImages = new List<RoomImage>();
            BookingInfos = new List<BookingInfo>();
        }
    }
}
