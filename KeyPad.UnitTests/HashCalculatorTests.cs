﻿using KeyPad.Calculators;
using KeyPad.SettingsEditor;
using KeyPad.SettingsEditor.Models;
using KeyPad.SettingsEditor.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyPad.UnitTests {

	[TestClass]	
	public class HashCalculatorTests {

		ICalculator<string, object> _calculator;

		[TestInitialize]
		public void Init() => _calculator = new Md5Calculator();

		[TestMethod]
		public void Md5HashCalculationSucceeds() {
			var testObj = new TestClass() { IntegerProperty = 1, StringProperty = "test" };
			string hash = _calculator.Calculate(testObj);
			Assert.IsTrue(!String.IsNullOrEmpty(hash));
		}

		[TestMethod]
		public void ObjectChangeResultsInDifferentHash() {
			var testObj = new TestClass() { IntegerProperty = 1, StringProperty = "test" };
			string firstHash = _calculator.Calculate(testObj);

			testObj.IntegerProperty = 2;
			string secondHash = _calculator.Calculate(testObj);
			Assert.IsTrue(firstHash != secondHash);
		}

		[TestMethod]
		public void SameObjectResultsInSameHash() {
			var testObj = new TestClass() { IntegerProperty = 1, StringProperty = "test" };
			string firstHash = _calculator.Calculate(testObj);
			string secondHash = _calculator.Calculate(testObj);

			Assert.IsTrue(firstHash == secondHash);
		}

		[TestMethod]
		public void ApplicationSettingViewModelWillSerialize() {
			ApplicationSettingViewModel vm = new ApplicationSettingViewModel(
				new ApplicationSetting() { Name = "Test", Display = "Test", Value = 5 },
				_calculator
			);

			string hash = _calculator.Calculate(vm);
			Assert.IsTrue(!String.IsNullOrEmpty(hash));
		}

		[TestMethod]
		public void ServiceSettingViewModelWillSerialize() {
			ServiceSettingViewModel vm = new ServiceSettingViewModel(
				new ServiceSetting(ServiceSettingNames.JOYSTICK_PORT_SETTING, "Value"),
				_calculator
			);
			string hash = _calculator.Calculate(vm);
			Assert.IsTrue(!String.IsNullOrEmpty(hash));
		}

		[Serializable]
		private class TestClass {
			public int IntegerProperty { get; set; }
			public string StringProperty { get; set; }
		}

	}

}
