using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace TestTasks
{
    public class Tests
    {
        private IWebDriver driver;

        private readonly By createButton = By.XPath("//a[text() ='Create Account']");
        private readonly By emailInputButton = By.XPath("//input[@class='form-control']");
        private readonly By passwordInputButton = By.XPath("//input[@type='password']");
        private readonly By confifmPasswordInputButton = By.XPath("//input[@name='confirmPassword']");   
        private readonly By singUpButton = By.XPath("//button[@class='btn btn-success']");

        private readonly By signInButton = By.XPath("//a[@href='/login']");
        private readonly By loginButton = By.XPath("//button[@type='submit']");
        private readonly By profile = By.XPath("//span[text()='test2@mail.ua']");



        private const string testEmail = "test2@mail.ua";
        private const string testPassword = "12345678";
        
        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://sample-project.tech-stack.dev/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void RegistrationNewUserOnWebsite()
        {

            var randomNumber = new Random().Next(1, 1000).ToString();
            var loginName = randomNumber + testEmail;

            var create = driver.FindElement(createButton);
            create.Click();

            var email = driver.FindElement(emailInputButton);
            email.SendKeys(loginName);

            var password = driver.FindElement(passwordInputButton);
            password.SendKeys(testPassword);

            var confirmPassword = driver.FindElement(confifmPasswordInputButton);
            confirmPassword.SendKeys(testPassword);

            var singUp = driver.FindElement(singUpButton);
            singUp.Click();

            var userLoginSelector = By.XPath($"//span[text()='{loginName}']");


            var actualLogin = driver.FindElement(userLoginSelector).Text;
            

            Assert.AreEqual(loginName.ToUpper(), actualLogin, "registration is faled");
        }


        [Test]
        public void LoginExistingUserOnWebsite()
        {

            var signIn = driver.FindElement(signInButton);
            signIn.Click();

            var login = driver.FindElement(emailInputButton);
            login.SendKeys(testEmail);

            var password = driver.FindElement(passwordInputButton);
            password.SendKeys(testPassword);

            var loginConfirm = driver.FindElement(loginButton);
            loginConfirm.Click();

            var actualProfile = driver.FindElement(profile).Text;

            Assert.AreEqual(testEmail.ToUpper(), actualProfile, "error login");


        }


        [Test]
        public void LoginWithEmptyData()
        {

            var signIn = driver.FindElement(signInButton);
            signIn.Click();

            var login = driver.FindElement(emailInputButton);
            login.SendKeys(string.Empty);

            var password = driver.FindElement(passwordInputButton);
            password.SendKeys(string.Empty);

            var loginConfirm = driver.FindElement(loginButton);
            loginConfirm.Click();

            var actualProfile = driver.FindElement(profile).Text;

            Assert.AreEqual(testEmail.ToUpper(), actualProfile, "error login");


        }
        [TearDown]

        public void TearDown() 
        {
            driver.Quit();
        }
    }
}