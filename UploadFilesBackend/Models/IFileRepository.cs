using System;

namespace UploadFilesBackend.Models
{
    public interface IFileRepository
    {
        Boolean isFileModelValid(FileModel fileModel);
    }
}
