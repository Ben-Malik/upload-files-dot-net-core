using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UploadFilesBackend.Models
{
    // An entity class wrapping up all about the files to be uploaded.
    // Postgresql is to be used to save the data.
    [Table("UPLOADED_FILE")]
    public class FileModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // The name of the file alongside it's extension.
        [MaxLength(100)]
        [Required(ErrorMessage = "File Name is required")]
        public string FileName { get; set; }

        // The type of the file.
        [MaxLength(100)]
        [Required(ErrorMessage = "The file type is required")]
        public string FileType { get; set; }

        [Required(ErrorMessage = "File size cannot be left empty")]
        public long FileSize { get; set; }

        [Required(ErrorMessage = "The date is required")]
        public DateTime? CreatedAt { get; set; }

        [Required(ErrorMessage = "File content cannot be empty")]
        public byte[] FileContent { get; set; }
    }
}
