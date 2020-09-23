

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared
{
	public class Producer
	{
		public IEnumerable<int> GetNumbersAsEnumerableWithYield()
		{
			for (int i = 0; i < 10; i++)
			{
				yield return GetNumber();
			}
		}

		public async IAsyncEnumerable<int> GetNumbersAsAsyncEnumerableWithYieldAsStream()
		{
			for (int i = 0; i < 10; i++)
			{
				yield return await GetNumberAsync();
			}
		}

		//Altmodisch async
		public async Task<IEnumerable<int>> GetNumbersAsEnumerableAsync()
		{
			List<int> result = new List<int>();

			for (int i = 0; i < 10; i++)
			{
				result.Add(await GetNumberAsync());
			}

			return result;
		}

		public IEnumerable<int> GetNumbersAsEnumerable()
		{
			return Enumerable.Range(0, 10);
		}

		public IAsyncEnumerable<int> GetNumbersAsAsyncEnumerable()
		{
			return AsyncEnum.Range(0, 10);
		}

		public IEnumerable<int> GetNumbersAsEnumerableFromList()
		{
			var result = new List<int>();
			return result;
		}

		public IAsyncEnumerable<int> GetNumbersAsAsyncEnumerableFromList()
		{
			var result = new List<int>();
			return result.ToAsyncEnumerable();
		}


		private int _counter = 0;

		private int GetNumber() => _counter++;

		private async Task<int> GetNumberAsync()
		{
			await Task.Delay(100);
			return _counter++;
		}
	}
}
