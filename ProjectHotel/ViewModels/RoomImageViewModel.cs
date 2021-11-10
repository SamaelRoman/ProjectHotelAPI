using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectHotel.ViewModels
{
    public class RoomImageViewModel
    {
        /// <summary>
        /// Уникальный идентификатор + имя файла.
        /// </summary>
        [Key]
        [Required]
        public Guid ID { get; set; }
        /// <summary>
        /// Ссылка на фотографию комнаты.
        /// </summary>
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImgUrl { get; set; }
        /// <summary>
        /// Сылка на номер, которому пренадлежит фотография.
        /// </summary>     
        public virtual RoomViewModel Room { get; set; }
        [Required]
        public string RoomID { get; set; }
        public RoomImageViewModel()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
