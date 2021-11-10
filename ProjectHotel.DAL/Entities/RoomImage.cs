using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectHotel.DAL.Entities
{
    public class RoomImage
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
        [Required]
        public virtual Room Room { get; set; }
        public string RoomID { get; set; }
    }
}
