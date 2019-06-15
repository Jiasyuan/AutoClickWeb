using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoClickWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            List<AccountInfo> taskAccount = default(List<AccountInfo>);
            string _account = string.Empty;
            string _passWord = string.Empty;
            string _companyID = "CTBC";
            while (true)
            {
                Console.WriteLine("輸入帳號");
                _account = Console.ReadLine();

                Console.WriteLine("輸入密碼");
                _passWord = Console.ReadLine();
                if (string.IsNullOrEmpty(_account))
                {
                    break;
                }
                else
                {
                    taskAccount.Add(new AccountInfo {
                        Account = _account,
                        Password = _passWord
                    });
                }
            }
            int _waitS = 0;
            IWebDriver driver = new ChromeDriver();
            string _url = @"https://www.leadercampus.com.tw/desktop/login";//URL
            Random crandom = new Random();
            int classInt = 1150;
            int papers =crandom.Next(80,100);
            string baseUrl = @"https://www.leadercampus.com.tw/desktop/course/";
            taskAccount.ForEach(item => {
                driver.Navigate().GoToUrl(_url);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30000);
                IWebElement btn = driver.FindElement(By.Id("agree_btn"));
                btn.Click();
                IWebElement inputCompanyId = driver.FindElement(By.Name("company_account"));
                Thread.Sleep(500);
                inputCompanyId.Clear();
                Thread.Sleep(500);
                inputCompanyId.SendKeys(_companyID);
                Thread.Sleep(500);

                IWebElement inputAcc = driver.FindElement(By.Name("account"));
                Thread.Sleep(500);
                inputAcc.Clear();
                Thread.Sleep(500);
                inputAcc.SendKeys(_account);
                Thread.Sleep(500);

                IWebElement inputPassWrod = driver.FindElement(By.Name("password"));
                Thread.Sleep(500);
                inputPassWrod.Clear();
                Thread.Sleep(500);
                inputPassWrod.SendKeys(_passWord);
                Thread.Sleep(500);
                IWebElement submitButton = driver.FindElement(By.ClassName("rbtn"));
                Thread.Sleep(2000);
                submitButton.Click();
                Thread.Sleep(1300);
                if (classInt < papers)
                {
                    classInt = 1150;
                }
                for (int i = 0; i <= papers; i++)
                {
                    _url = $"{baseUrl}{classInt - i}";
                    driver.Navigate().GoToUrl(_url);
                    Thread.Sleep(500);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);
                    _waitS = crandom.Next(10000, 16000);
                    Thread.Sleep(_waitS);
                }
                driver.Quit();
                driver.Dispose();
                driver = new ChromeDriver();
            });
            Console.WriteLine("任務完成");
            Console.ReadLine();
        }
    }

    public class AccountInfo
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
