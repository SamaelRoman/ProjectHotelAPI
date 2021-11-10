using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ProjectHotel.ViewModels
{
    public class RoomViewModel
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
        public virtual ICollection<RoomImageViewModel> RoomImages { get; set; }
        /// <summary>
        /// Путь к превью фотографии.Превью фото это случайная из его фотографий!.
        /// </summary>
        [NotMapped]
        [DataType(DataType.ImageUrl)]
        public string Preview
        {
            get
            { 
                if(RoomImages != null && RoomImages.Count >= 1)
                {
                    var rnd = new Random();
                    var index = rnd.Next(RoomImages.Count - 1);
                    return $"{RoomImages.ElementAt(index).ImgUrl}";
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Категория номера.
        /// </summary>
        [JsonIgnore]
        public CategoryViewModel Category { get; set; }
        public Guid CategoryID { get; set; }
        /// <summary>
        /// Список объектов класса BookingIfo содержащих информацию о бронировании номера (Кем забронирован, на какую дату, и тд).
        /// </summary>
        public virtual ICollection<BookingInfoViewModel> BookingInfos { get; set; }
        /// <summary>
        /// Вычесляемое свойство. Возвращает ответ есть ли активные брони на данный номер.В случае наличия брони возвращает true!
        /// </summary>
        [NotMapped]
        public bool IsBooked { get { if (BookingInfos.Count > 0) { return true; } else { return false; }; } }
        public RoomViewModel()
        {
            RoomImages = new List<RoomImageViewModel>();
            BookingInfos = new List<BookingInfoViewModel>();
        }
    }
    public class AvaiableRoomViewModel
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
        [JsonIgnore]
        public virtual ICollection<RoomImageViewModel> RoomImages { get; set; }
        /// <summary>
        /// Путь к превью фотографии.Превью фото это случайная из его фотографий!.
        /// </summary>
        [NotMapped]
        [DataType(DataType.ImageUrl)]
        public string Preview
        {
            get
            {
                if (RoomImages != null && RoomImages.Count >= 1)
                {
                    var rnd = new Random();
                    var index = rnd.Next(RoomImages.Count - 1);
                    return $"{RoomImages.ElementAt(index).ImgUrl}";
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Категория номера.
        /// </summary>
        [JsonIgnore]
        public CategoryViewModel Category { get; set; }
        public Guid CategoryID { get; set; }
        public AvaiableRoomViewModel()
        {
            RoomImages = new List<RoomImageViewModel>();
        }
    }
}
