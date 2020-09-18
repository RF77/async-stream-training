using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shared;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
	public class UnitTestBase
	{
		private readonly ITestOutputHelper _output;
		public MyTestOutputHelper OutputHelper { get; }

		public UnitTestBase(ITestOutputHelper output)
		{
			OutputHelper = new MyTestOutputHelper(output);
		}
		
		protected async void WriteToOutput<T>(IAsyncEnumerable<T> stream,
			CancellationToken cancellationToken = default)
		{
			await WriteToOutputAsync(stream, cancellationToken);
		}

		protected async void WriteToOutput<T>(IEnumerable<T> stream,
			CancellationToken cancellationToken = default)
		{
			await WriteToOutputAsync(stream.ToAsyncEnumerable(), cancellationToken);
		}

		protected async Task WriteToOutputAsync<T>(IAsyncEnumerable<T> stream,
			CancellationToken cancellationToken = default)
		{
			try
			{
				await foreach (var item in stream.WithCancellation(cancellationToken))
				{
					Write($"Received: {item}");
				}
			}
			catch (OperationCanceledException)
			{
				Write($"WriteStreamToOutput Operation was canceled");
			}
			catch (Exception e)
			{
				Write($"WriteStreamToOutput catched {e.GetType().Name} Exception");
			}

			Write("WriteStreamToOutput completed");
		}

		private void Write(string text)
		{
			OutputHelper.Write(text);
		}
	}


}