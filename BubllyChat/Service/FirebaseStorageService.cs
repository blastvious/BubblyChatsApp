using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BubllyChat.Service
{
    public class FirebaseStorageService
    {
        private FirebaseStorage _storage;
        private const string StorageBucket = "bubblychatapp.firebasestorage.app";

        public FirebaseStorageService()
        {
            _storage = new FirebaseStorage(StorageBucket);
        }
        //Tai anh len

        public async Task<string> UpLoadAvatarAsync(string filename, Stream fileStream)
        {
            try
            {
                var imgUrl = await _storage.Child("Avatar_USER")
                    .Child(filename)
                    .PutAsync(fileStream);
                return imgUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tải ảnh lên Firebase Storage: " + ex.Message);
                return null;
            }
        }

        //Lay anh avatar
        public async Task<string> GetAvatarUrlAsync(string filename)
        {
            try
            {
                return await _storage.Child("Avatar_USER")
                    .Child(filename)
                    .GetDownloadUrlAsync();
                //Co the bo jpg
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy ảnh từ Firebase Storage: " + ex.Message);
                return null;
            }
        }
        //Xoa anh avatar
        public async Task<bool> DeleteAvatarAsync(string userID)
        {
            try
            {
                await _storage.Child("Avatar_USER")
                    .Child(userID + ".jpg")
                    .DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xóa ảnh từ Firebase Storage: " + ex.Message);
                return false;
            }
        }

    }
}
