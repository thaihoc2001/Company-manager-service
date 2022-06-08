using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using RestAPICompanyDemo.Model;

namespace RestAPICompanyDemo.util
{
    public class uploadFile
    {
        public static Cloudinary cloudinary;
        public const string CLOUND_NAME = "iuh-university";
        public const string API_KEY = "465287616829272";
        public const string API_SECRET = "uiHxEqvnFt43bXbizvqw6Lf1zEY";
        public Image uploadImage(string pathName)
        {
            Account account = new Account(CLOUND_NAME, API_KEY, API_SECRET);
            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
            Image image = new Image();
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    //File = new FileDescription(fileName, inputStream),
                    File = new FileDescription(pathName),
                };
                Console.WriteLine(uploadParams);
                var uploadResult = cloudinary.Upload(uploadParams);
                Console.WriteLine("Upload image successfully");
                var uplPath = uploadResult.Uri;
                Console.WriteLine("URL IMAGE" + uplPath.AbsoluteUri.ToString());
                Console.WriteLine("URL IMAGE" + uploadResult.PublicId);
                image.Imageid = uploadResult.PublicId;
                image.ImageUrl = uplPath.AbsoluteUri.ToString();
                image.Employee = null;
            }
            catch(Exception e)
            {
                Console.WriteLine("cath");
                Console.WriteLine(e);
            }
            return image;
        }
    }
}
