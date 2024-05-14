namespace HrmsSystemAdmin.Web.Helper
{
    public class FileHelper
    {
        public static ByteArrayContent GenerateByteArrayFromFile(IFormFile file)
        {
            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
                data = br.ReadBytes((int)file.OpenReadStream().Length);

            ByteArrayContent bytes = new ByteArrayContent(data);
            return bytes;
        }
    }
}
