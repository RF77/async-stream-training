using System;
using Shared;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
	public class ProducerTests : UnitTestBase
	{
		private Producer _unitUnderTest;

		[Fact]
		public void GetNumbersAsEnumerableWithYieldTest()
		{
			WriteToOutput(_unitUnderTest.GetNumbersAsEnumerableWithYield());
		}

		[Fact]
		public void GetNumbersAsAsyncEnumerableWithYieldAsStreamTest()
		{
			WriteStreamToOutput(_unitUnderTest.GetNumbersAsAsyncEnumerableWithYieldAsStream());
		}

		public ProducerTests(ITestOutputHelper output) : base(output)
		{
			_unitUnderTest = new Producer();
		}
	}
}
