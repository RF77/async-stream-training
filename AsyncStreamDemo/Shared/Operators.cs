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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared
{
	/// <summary>
	/// IEnumerable rein => IEnumerable raus
	/// IAsyncEnumerable rein => IAsnycEnumerable raus
	///
	/// z.B. Linq
	/// </summary>
	public class Operators
	{
		public IEnumerable<int> OnlyEven(IEnumerable<int> numbers)
		{
			foreach (var number in numbers)
			{
				if (number % 2 == 0)
				{
					yield return number;
				}
			}
		}

		public IEnumerable<int> OnlyEvenShort(IEnumerable<int> numbers)
		{
			return numbers.Where(i => i % 2 == 0);
		}

		public async IAsyncEnumerable<int> OnlyEvenAsStream(IAsyncEnumerable<int> numbers)
		{
			await foreach (var number in numbers)
			{
				if (number % 2 == 0)
				{
					yield return number;
				}
			}
		}

		public IAsyncEnumerable<int> OnlyEvenShortAsStream(IAsyncEnumerable<int> numbers)
		{
			return numbers.Where(i => i % 2 == 0);
		}

		public async Task<IEnumerable<double>> GetTemperaturesForStationsAsync(IEnumerable<int> ids)
		{
			var result = new List<double>();
			foreach (var id in ids)
			{
				var temperature = await GetTemperatureForStationWidthIdAsync(id);
				result.Add(temperature);
			}

			return result;
		}

		public async IAsyncEnumerable<double> GetTemperaturesForStationsAsStream(IEnumerable<int> ids)
		{
			foreach (var id in ids)
			{
				var temperature = await GetTemperatureForStationWidthIdAsync(id);
				yield return temperature;
			}
		}
		
		public IAsyncEnumerable<double> GetTemperaturesForStationsAsStream2(IEnumerable<int> ids)
		{
			return ids.ToAsyncEnumerable().SelectAwait(async id => await GetTemperatureForStationWidthIdAsync(id));
		}

		public IAsyncEnumerable<double> GetTemperaturesForStationsWithAsyncStreamsAsStream(IAsyncEnumerable<int> ids)
		{
			return ids.SelectAwait(async id => await GetTemperatureForStationWidthIdAsync(id));
		}
		
		private async Task<double> GetTemperatureForStationWidthIdAsync(int stationId)
		{
			await Task.Delay(100);

			return Math.Sqrt(stationId) + 10;
		}
	}
}