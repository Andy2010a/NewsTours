using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace NewsTours
{


	public class NTPageObject
	{
		private IWebDriver driver;
		public const string URL = "http://newtours.demoaut.com/mercurywelcome.php";
		public const string userName = "mercury";
		public const string pwd = "mercury";


		public NTPageObject(IWebDriver driver)
		{
			this.driver = driver;
			PageFactory.InitElements(driver, this);
		}

		[FindsBy(How = How.Name, Using = "userName")]
		public IWebElement UserName { get; set; }

		[FindsBy(How = How.Name, Using = "password")]
		public IWebElement Password { get; set; }

		[FindsBy(How = How.Name, Using = "login")]
		public IWebElement SignIn { get; set; }

		[FindsBy(How = How.Name, Using = "tripType")]
		public IList<IWebElement> TripType { get; set; } // multiple webelements under same name

		[FindsBy(How = How.Name, Using = "fromPort")]
		public IWebElement Departures { get; set; }

		[FindsBy(How = How.Name, Using = "toPort")]
		public IWebElement Arrivals { get; set; }

		[FindsBy(How = How.Name, Using = "servClass")]
		public IList<IWebElement> ServiceClasses { get; set; } // multiple webelements under same name

		[FindsBy(How = How.Name, Using = "findFlights")]
		public IWebElement Continue { get; set; }

		[FindsBy(How = How.Name, Using = "reserveFlights")]
		public IWebElement Reserve { get; set; }

		[FindsBy(How = How.Name, Using = "passFirst0")]
		public IWebElement FirstName { get; set; }

		[FindsBy(How = How.Name, Using = "passLast0")]
		public IWebElement LastName { get; set; }

		[FindsBy(How = How.Name, Using = "creditnumber")]
		public IWebElement CreditCard { get; set; }

		[FindsBy(How = How.Name, Using = "ticketLess")]
		public IWebElement TicketLess { get; set; }

		[FindsBy(How = How.Name, Using = "buyFlights")]
		public IWebElement SecurePurchase { get; set; }




		public void Navigate()
		{
			driver.Navigate().GoToUrl(URL);
			Assert.AreEqual(driver.Title, "Welcome: Mercury Tours");
			
		}



		public void Login()
		{
			UserName.SendKeys(userName);
			Password.SendKeys(pwd);			
			SignIn.Submit();
			Assert.AreEqual(driver.Title, "Find a Flight: Mercury Tours");

		}

		public void Type(String text)
		{

		}


		public void SelectTripType()
		{
	


			foreach (IWebElement trip in TripType)
			{
				if (trip.GetAttribute("value") == "oneway")
					trip.Click(); ;
			}

		}

		public void SelectDepature()
		{

			SelectDropdown(Departures, "Sydney");
		}



		public void SelectArrival()
		{

			SelectDropdown(Arrivals, "London");
		}

		public void SelectDropdown( IWebElement dropdown, string option)
		{
			SelectElement item = new SelectElement(dropdown);
			item.SelectByValue(option);
		}

		public void SelectServiceClass()
		{
			foreach (IWebElement Serviceclass in ServiceClasses)
			{
				if (Serviceclass.GetAttribute("value") == "First")
					Serviceclass.Click();
			}
		}

		public void FindFlights()
		{
			Continue.Click();
			Assert.AreEqual(driver.Title, "Select a Flight: Mercury Tours");
		}
		public void Reserveflight()
		{

			Reserve.Click();

			Assert.AreEqual(driver.Title, "Book a Flight: Mercury Tours");

		}


		public void PassengerDetails()
		{
			FirstName.SendKeys("Anand");
			LastName.SendKeys("Mamidi");
			CreditCard.SendKeys("510510");
             if (!TicketLess.Selected)
				TicketLess.Click();

			SecurePurchase.Click();				
			Assert.AreEqual(driver.Title, "Flight of Confirmation: Mercury Tours");


		}



	}

	

    [TestClass]
	public class UnitTest1
	{

		[TestMethod]
		public void TestMethod1()
		{

			IWebDriver driver = new InternetExplorerDriver();
			
			NTPageObject PageObject = new NTPageObject(driver);
			PageObject.Navigate();
			PageObject.Login();			
			PageObject.SelectTripType();
			PageObject.SelectDepature();
			PageObject.SelectArrival();
			PageObject.SelectServiceClass();
			PageObject.FindFlights();
			PageObject.Reserveflight();
			PageObject.PassengerDetails();

		}
	}
}
