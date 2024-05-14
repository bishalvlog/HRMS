using HrmsSystemAdmin.Web.Dto.Menus;
using HrmsSystemAdmin.Web.Helper;
using HrmsSystemAdmin.Web.Model.Users;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace HrmsSystemAdmin.Web.Data.Repository
{
    public  class BaseRepository
    {
        protected virtual StringContent GetJsonStringContent(object inputDataObj)
        {
            return new StringContent(JsonConvert.SerializeObject(inputDataObj),Encoding.UTF8, "applicaton/json");
        }
        protected virtual MultipartFormDataContent AddUserFormContent (UserDetails inputDataObj)
        {
            var multipartFormContent = new MultipartFormDataContent();

            if (inputDataObj.Id != 0)
            {
                multipartFormContent.Add(new StringContent(inputDataObj.Id.ToString()), "Id");
            }
            multipartFormContent.Add(new StringContent(inputDataObj.UserName), "UserName");
            multipartFormContent.Add(new StringContent(inputDataObj.FullName), "FullName");
            multipartFormContent.Add(new StringContent(inputDataObj.Email), "Email");
            multipartFormContent.Add(new StringContent(inputDataObj.Mobile), "Mobile");
            multipartFormContent.Add(new StringContent(inputDataObj.Address), "Address");
            multipartFormContent.Add(new StringContent(inputDataObj.Gender.ToString()), "Gender");
            if (inputDataObj.Department != null)
            {
                multipartFormContent.Add(new StringContent(inputDataObj.Department), name: "Depertment");
            }
            if (inputDataObj.DateOfBirth != null || inputDataObj.DateOfJoining != null)
            {
                multipartFormContent.Add(new StringContent(inputDataObj.DateOfBirth.ToString()), "DateOfBirth");
                multipartFormContent.Add(new StringContent(inputDataObj.DateOfJoining.ToString()), "DateOfJoining");
            }
            multipartFormContent.Add(new StringContent(inputDataObj.Password), "Password");
            multipartFormContent.Add(new StringContent(inputDataObj.IsActive.ToString()), "IsActive");



            if (inputDataObj.ProfileImage is not null && inputDataObj.ProfileImage.Length > 0)
            {
                var bytes = FileHelper.GenerateByteArrayFromFile(inputDataObj.ProfileImage);
                multipartFormContent.Add(bytes, "ProfileImage", inputDataObj.ProfileImage.FileName);
            }
            return multipartFormContent;

        }
        protected virtual MultipartFormDataContent UpdateUserFormContent(UpdateUserDetails inputDataObj)
        {
            var multipartFormContent = new MultipartFormDataContent();

            //Add other fields
            if (inputDataObj.id != 0)
            {
                multipartFormContent.Add(new StringContent(inputDataObj.id.ToString()), "Id");
            }
            multipartFormContent.Add(new StringContent(inputDataObj.fullName), "FullName");
            if (inputDataObj.address != null)
            {
                multipartFormContent.Add(new StringContent(inputDataObj.address), "Address");
            }
            multipartFormContent.Add(new StringContent(inputDataObj.gender.ToString()), "Gender");
            if (inputDataObj.department != null)
            {
                multipartFormContent.Add(new StringContent(inputDataObj.department), name: "Depertment");
            }
            multipartFormContent.Add(new StringContent(inputDataObj.dateOfBirth.ToString()), "DateOfBirth");
            multipartFormContent.Add(new StringContent(inputDataObj.dateOfJoining.ToString()), "DateOfJoining");
            multipartFormContent.Add(new StringContent(inputDataObj.isActive.ToString()), "IsActive");



            if (inputDataObj.ProfileImage is not null && inputDataObj.ProfileImage.Length > 0)
            {
                var bytes = FileHelper.GenerateByteArrayFromFile(inputDataObj.ProfileImage);
                multipartFormContent.Add(bytes, "ProfileImage", inputDataObj.ProfileImage.FileName);
            }
            return multipartFormContent;

        }
        protected virtual MultipartFormDataContent AddMenuFormContent(AddMenu inputDataObj)
        {
            var multipartFormContent = new MultipartFormDataContent();

            //Add other fields

            multipartFormContent.Add(new StringContent(inputDataObj.Title), "Title");
            multipartFormContent.Add(new StringContent(inputDataObj.ParentId.ToString()), "ParentId");
            multipartFormContent.Add(new StringContent(inputDataObj.MenuUrl), "MenuUrl");
            multipartFormContent.Add(new StringContent(inputDataObj.IsActive.ToString()), "IsActive");
            multipartFormContent.Add(new StringContent(inputDataObj.DisplayOrder.ToString()), "DisplayOrder");

            if (inputDataObj.ImagePath is not null && inputDataObj.ImagePath.Length > 0)
            {
                var bytes = FileHelper.GenerateByteArrayFromFile(inputDataObj.ImagePath);
                multipartFormContent.Add(bytes, "MenuImage", inputDataObj.ImagePath.FileName);
            }
            return multipartFormContent;

        }
        protected virtual MultipartFormDataContent updateMenuFormContent(UpdateMenu inputDataObj)
        {
            var multipartFormContent = new MultipartFormDataContent();


            multipartFormContent.Add(new StringContent(inputDataObj.Id.ToString()), "Id");
            multipartFormContent.Add(new StringContent(inputDataObj.Title), "Title");
            multipartFormContent.Add(new StringContent(inputDataObj.ParentId.ToString()), "ParentId");
            multipartFormContent.Add(new StringContent(inputDataObj.MenuUrl), "MenuUrl");
            multipartFormContent.Add(new StringContent(inputDataObj.IsActive.ToString()), "IsActive");
            multipartFormContent.Add(new StringContent(inputDataObj.DisplayOrder.ToString()), "DisplayOrder");

            if (inputDataObj.ImagePath is not null && inputDataObj.ImagePath.Length > 0)
            {
                var bytes = FileHelper.GenerateByteArrayFromFile(inputDataObj.ImagePath);
                multipartFormContent.Add(bytes, "MenuImage", inputDataObj.ImagePath.FileName);
            }
            return multipartFormContent;

        }
        protected virtual MultipartFormDataContent MultiPartFormContent<T>(T inputDataObj)
        {
            var multipartFormContent = new MultipartFormDataContent();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in properties)
            {
                if (prop.PropertyType == typeof(int))
                {
                    multipartFormContent.Add(new StringContent(prop.GetValue(inputDataObj)?.ToString()), prop.Name);
                }

                if (prop.PropertyType == typeof(IFormFile))
                {
                    var file = (IFormFile)prop.GetValue(inputDataObj);
                    if (file is not null && file.Length > 0)
                    {
                        var bytes = FileHelper.GenerateByteArrayFromFile(file);
                        multipartFormContent.Add(bytes, prop.Name, file.FileName);
                    }
                }

                if (prop.PropertyType == typeof(string) && prop.GetValue(inputDataObj) != null)
                {
                    multipartFormContent.Add(new StringContent(prop.GetValue(inputDataObj)?.ToString()), prop.Name);
                }
                if (prop.PropertyType == typeof(bool))
                {
                    multipartFormContent.Add(new StringContent(prop.GetValue(inputDataObj)?.ToString().ToLower()), prop.Name);
                }

                if (prop.PropertyType == typeof(DateTime))
                {
                    var dateTimeValue = (DateTime)prop.GetValue(inputDataObj);
                    var dateTimeString = dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
                    multipartFormContent.Add(new StringContent(dateTimeString), prop.Name);
                }
            }

            return multipartFormContent;
        }


    }
}
