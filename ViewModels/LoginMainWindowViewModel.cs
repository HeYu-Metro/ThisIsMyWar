using Prism.Commands;
using Prism.Mvvm;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using ThisIsMyWar.COM;
using ThisIsMyWar.Models;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace ThisIsMyWar.ViewModels
{
    public class LoginMainWindowViewModel : BindableBase
    {
        private string registerUserName;
        /// <summary>
        /// 注册用户名
        /// </summary>
        public string RegisterUserName
        {
            get { return registerUserName; }
            set { SetProperty(ref registerUserName, value); }
        }
        private string registerPassWord;
        /// <summary>
        /// 注册密码
        /// </summary>
        public string RegisterPassWord
        {
            get { return registerPassWord; }
            set { SetProperty(ref registerPassWord, value); }
        }
        private string registerEmail;
        /// <summary>
        /// 注册邮箱
        /// </summary>
        public string RegisterEmail
        {
            get { return registerEmail; }
            set { SetProperty(ref registerEmail, value); }
        }
        private string registerVerification;
        /// <summary>
        /// 注册验证码
        /// </summary>
        public string RegisterVerification
        {
            get { return registerVerification; }
            set { SetProperty(ref registerVerification, value); }
        }
        private string verification;
        /// <summary>
        /// 验证码
        /// </summary>
        public string Verification
        {
            get { return verification; }
            set { SetProperty(ref verification, value); }
        }
        private string registerPermissions;
        /// <summary>
        /// 注册用户权限码
        /// </summary>
        public string RegisterPermissions
        {
            get { return registerPermissions; }
            set { SetProperty(ref registerPermissions, value); }
        }

        private string userName;
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }
        private string passWord;
        /// <summary>
        /// 登录密码
        /// </summary>
        public string PassWord
        {
            get { return passWord; }
            set { SetProperty(ref passWord, value); }
        }
        private string message="欢迎来到This Is My War!";
        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public DelegateCommand RegisterCommand { get; set; }

        public DelegateCommand<Object> LoginCommand { get; set; }

        public DelegateCommand GetVerificationCommand { get; set; }

        private LoginModel loginModel;
        public LoginMainWindowViewModel()
        {
            var passwordBox = new PasswordBox();
            loginModel = new LoginModel();

            LoginCommand = new DelegateCommand<Object>(LoginEven);
            RegisterCommand = new DelegateCommand(RegisterEven);
            GetVerificationCommand = new DelegateCommand(GetVerificationEven);

        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        private void GetVerificationEven()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RegisterEmail))
                {
                    Message = "请输入邮箱！";
                }
                else
                {
                    if (!Regex.IsMatch(RegisterEmail, @"\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}"))
                    {
                        Message = "邮箱格式不正确！";
                    }
                    else
                    {
                        var var1 = new Message();
                        Verification = var1.Code();
                        string email = RegisterEmail;
                        string subject = "验证码";
                        string body = $"验证码是{Verification}";

                        var var = new EmailSender();
                        var.SendEmail(email, subject, body);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        private void RegisterEven()
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (string.IsNullOrWhiteSpace(RegisterUserName))
            {
                Message = "请输入用户名！";
            }
            else
            {
                if (loginModel.GetUserName(RegisterUserName))
                {
                    Message = "用户名已存在！";
                }
                else
                {
                    if (RegisterUserName.Length > 20)
                    {
                        Message = "用户名太长！";
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(RegisterPassWord))
                        {
                            Message = "请输入密码！";
                        }
                        else
                        {
                            if (!Regex.IsMatch(RegisterPassWord, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$"))
                            {
                                Message = "密码格式错误！";
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(RegisterEmail))
                                {
                                    Message = "请输入邮箱！";
                                }
                                else
                                {
                                    if (string.IsNullOrWhiteSpace(RegisterVerification))
                                    {
                                        Message = "请输入验证码！";
                                    }
                                    else
                                    {
                                        if (RegisterVerification != Verification)
                                        {
                                            Message = "验证码错误！";
                                        }
                                        else
                                        {
                                            if (string.IsNullOrWhiteSpace(RegisterPermissions))
                                            {
                                                Message = "请输入用户权限码！";
                                            }
                                            else
                                            {
                                                int Permissions = 4;
                                                if (RegisterPermissions == "B3R8A5P2T9C1D6E7")
                                                {
                                                    Permissions = 1;
                                                }
                                                if (RegisterPermissions == "K7J1H6G2F5E8D4C9")
                                                {
                                                    Permissions = 2;
                                                }
                                                if (RegisterPermissions == "X5Y9Z1W2V7U4T3S6")
                                                {
                                                    Permissions = 3;
                                                }
                                                if (RegisterPermissions == "M2N5B8V1C4X7Z3Y6")
                                                {
                                                    Permissions = 4;
                                                }
                                                //密码加密

                                                byte[] encrypted = Encryption.Encrypt(RegisterPassWord);
                                                passWord = Convert.ToBase64String(encrypted);

                                                loginModel.AddUser(RegisterUserName, passWord, RegisterEmail, Permissions, dateTime);
                                                if (loginModel.GetUserName(RegisterUserName))
                                                {
                                                    Message = "注册成功！";
                                                    RegisterUserName = string.Empty;
                                                    RegisterPassWord = string.Empty;
                                                    RegisterEmail = string.Empty;
                                                    RegisterVerification = string.Empty;
                                                    RegisterPermissions = string.Empty;
                                                }
                                                else
                                                {
                                                    Message = "注册失败";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        private void LoginEven(object obj)
        {
            HandyControl.Controls.PasswordBox passwordBox = obj as HandyControl.Controls.PasswordBox;
            string pwd = passwordBox.Password;
            if (string.IsNullOrWhiteSpace(UserName))
            {
                Message = "请输入用户名！";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(pwd))
                {
                    Message = "请输入密码！";
                }
                else
                {
                    if (!loginModel.GetUserName(UserName))
                    {
                        Message = "用户名不存在！";
                    }
                    else
                    {
                        string password = loginModel.PassWord(UserName);
                        byte[] encrypted = Encryption.Encrypt(pwd);
                        passWord = Convert.ToBase64String(encrypted);


                        if (!passWord.Equals(password))
                        {
                            Message = "密码错误！";
                        }
                        else
                        {
                            Message = "登录成功！";
                            UserName = string.Empty;
                            passwordBox.Password = "";
                        }
                    }
                }
            }
        }
    }
}
