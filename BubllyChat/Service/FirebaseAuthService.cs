using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using BubllyChat.MVVM.Models;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
namespace BubllyChat.Service
{
    public class FirebaseAuthService
    {
        private const string ApiKey = "AIzaSyCQoWUMYseTNtBdMyP0enMs4nrQrMRQ9LE";
        private const string AuthDomain = "bubblychatapp.firebaseapp.com";
        private readonly FirebaseAuthClient _authClient;


        private UserCredential _userCredential;
        public string _messageError;

        public FirebaseAuthService()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = ApiKey,
                AuthDomain = AuthDomain,
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                },

                UserRepository = new FileUserRepository("BubblyChat")
            };
            _authClient = new FirebaseAuthClient(config);

        }

        //Dang ky
        public async Task<Users> RegisterUserAsync(string email, string password)
        {
            try
            {
                _userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password);
                var user = _userCredential.User;
                var newUser = new Users
                {
                    Id = user.Uid,
                    Email = email,
                    CreatedAt = DateTime.Now,
                    DisplayName = "",
                    FirstLogin = true,


                };
                var firebaseRTDB = new FirebaseRTDB();
                await firebaseRTDB.SaveUserAsync(newUser);
                return newUser;
            }
            catch (FirebaseAuthException ex)
            {
                _messageError = GetFriendlyErrorMessage(ex);
                string message = GetFriendlyErrorMessage(ex);
                Console.WriteLine("Lỗi đăng ký: " + message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi không xác định: " + ex.Message);
                return null;
            }
        }

        public async Task<Users> LoginUserAsync(string email, string password)
        {
            try
            {
                _userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                var user = _userCredential.User;


                var firebaseRTDB = new FirebaseRTDB();
                var currentUser = await firebaseRTDB.GetUserAsync(user.Uid);
                // to deal if user is existing in The RTDB
                if (currentUser != null)
                {
                    CurrentUserService.CurrentUser = currentUser;
                    return currentUser;
                }
                else
                {
                    //to deal if user not found in RTDB
                    return new Users
                    {
                        Id = user.Uid,
                        Email = email,
                        CreatedAt = DateTime.Now
                    };

                }
            }
            catch (FirebaseAuthException ex)
            {
                _messageError = GetFriendlyErrorMessage(ex);
                Console.WriteLine("Lỗi đăng nhập: " + _messageError);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi không xác định: " + ex.Message);
                return null;
            }
        }


        //Dang xuat
        //To do: Log out account
        public Task LogOutAsync()
        {
            Logout();
            return Task.CompletedTask;
        }

        //Convert SecureString to string
        public static string SecureStringToString(SecureString secureString)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        //Doi mat khau
        public async Task<bool> SendPasswordResetEmailAsync(string email)
        {
            try
            {
                await _authClient.ResetEmailPasswordAsync(email);
                _messageError = "Đã gửi email đặt lại mật khẩu đến " + email;
                return true;
            }
            catch (FirebaseAuthException ex)
            {
                _messageError = GetFriendlyErrorMessage(ex);
                return false;
            }
            catch (Exception ex)
            {
                _messageError = "Lỗi không xác định: " + ex.Message;
                return false;
            }
        }


        //Lay token de duy tri phien dang nhap
        public async Task<string> GetidToken()
        {
            var user = _authClient.User;
            if (user != null)
            {
                var token = await user.GetIdTokenAsync();
                return token;
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            try
            {
                _authClient.SignOut();
                _userCredential = null;
            }
            catch (Exception ex)
            {
                _messageError = "Lỗi đăng xuất " + ex.Message;
            }
        }
        private string GetFriendlyErrorMessage(FirebaseAuthException ex)
        {
            switch (ex.Reason)
            {
                case AuthErrorReason.EmailExists:
                    return "Email này đã được đăng ký";
                case AuthErrorReason.WeakPassword:
                    return "Mật khẩu phải có ít nhất 6 ký tự";
                case AuthErrorReason.WrongPassword:
                    return "Sai mật khẩu";
                case AuthErrorReason.InvalidEmailAddress:
                    return "Email không hợp lệ";
                case AuthErrorReason.UserNotFound:
                    return "Người dùng không tồn tại";
                default:
                    return $"Lỗi: {ex.Reason}";
            }
        }

    }
}
