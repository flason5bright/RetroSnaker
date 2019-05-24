using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RetroSnaker.UnitTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			string a = "hello";
			var b = a;
			Assert.AreEqual(a, b);
		}

		[TestMethod]
		public void TestMethod2()
		{
			int a = 1;
			var b = a;
			var c = 1;
			var d = 1;
			var e = 2;
			Assert.AreEqual(a, b);
			Assert.AreEqual(a, c);
			Assert.AreEqual(d, e);
			//throw new NullReferenceException();
		}
	}
}
