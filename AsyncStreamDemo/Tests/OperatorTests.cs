// /*******************************************************************************
//  * Copyright (c) 2020 by RF77 (https://github.com/RF77)
//  * All rights reserved. This program and the accompanying materials
//  * are made available under the terms of the Eclipse Public License v1.0
//  * which accompanies this distribution, and is available at
//  * http://www.eclipse.org/legal/epl-v10.html
//  *
//  * Contributors:
//  *    RF77 - initial API and implementation and/or initial documentation
//  *******************************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Shared;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
	public class OperatorTests : UnitTestBase
	{
		private Operators _unitUnderTest;
		private List<int> _numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

		public IAsyncEnumerable<int> NumbersAsStream => _numbers.ToAsyncEnumerable().Delay(50);

		[Fact]
		public void OnlyEvenTest()
		{
			WriteToOutput(_unitUnderTest.OnlyEven(_numbers));
		}

		[Fact]
		public void OnlyEvenAsStreamTest()
		{
			WriteToOutput(_unitUnderTest.OnlyEvenAsStream(_numbers.ToAsyncEnumerable()));
		}

		[Fact]
		public async Task GetTemperaturesForStationsAsyncTest()
		{
			var result = await _unitUnderTest.GetTemperaturesForStationsAsync(_numbers);
			WriteToOutput(result);
		}

		[Fact]
		public void GetTemperaturesForStationListAsStreamTest()
		{
			// Läuft immer mal in einem anderen Thread !!
			WriteToOutput(_unitUnderTest.GetTemperaturesForStationListAsStreamSequential(_numbers));
		}

		[Fact]
		public void GetTemperaturesForStationsAsStreamWithSyncContextTest()
		{
			// Läuft nun immer im gleichen Thread
			AsyncContext.Run(() =>
				WriteToOutput(_unitUnderTest.GetTemperaturesForStationListAsStreamSequential(_numbers)));
		}

		[Fact]
		public void GetTemperaturesForStationListAsStreamParallelTest()
		{
			WriteToOutput(_unitUnderTest.GetTemperaturesForStationsAsStreamParallel(_numbers));
		}

		[Fact]
		public void GetTemperaturesForStationListAsStreamParallelButLimitedWithSyncContextTest()
		{
			AsyncContext.Run(async () =>
				await WriteToOutputAsync(_unitUnderTest.GetTemperaturesForStationsAsStreamParallelButLimited(_numbers)));
		}

		[Fact]
		public void GetTemperaturesForStationListAsStreamParallelButLimitedTest()
		{
			WriteToOutput(_unitUnderTest.GetTemperaturesForStationsAsStreamParallelButLimited(_numbers));
		}


		[Fact]
		public void GetTemperaturesForStationStreamAsStreamParallelButLimitedWithSyncContextTest()
		{
			AsyncContext.Run(async () =>
				await WriteToOutputAsync(_unitUnderTest.GetTemperaturesForStationsAsStreamParallelButLimited(NumbersAsStream)));
		}

		[Fact]
		public async Task GetTemperaturesForStationStreamAsStreamParallelButLimitedTest()
		{
			await WriteToOutputAsync(_unitUnderTest.GetTemperaturesForStationsAsStreamParallelButLimited(NumbersAsStream));
		}

		public OperatorTests(ITestOutputHelper output) : base(output)
		{
			_unitUnderTest = new Operators(OutputHelper);
		}
	}
}