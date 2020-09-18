using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
	public class UnitTestBase
	{
		private readonly ITestOutputHelper _output;
		private readonly DateTime _startTime;
		private DateTime _lastTimeStamp;

		public UnitTestBase(ITestOutputHelper output)
		{
			_output = output;
			_startTime = DateTime.Now;
			_lastTimeStamp = _startTime;
		}
		
		protected async void WriteStreamToOutput<T>(IAsyncEnumerable<T> stream,
			CancellationToken cancellationToken = default)
		{
			await WriteStreamToOutputAsync(stream, cancellationToken);
		}

		protected async void WriteToOutput<T>(IEnumerable<T> stream,
			CancellationToken cancellationToken = default)
		{
			await WriteStreamToOutputAsync(stream.ToAsyncEnumerable(), cancellationToken);
		}

		protected async Task WriteStreamToOutputAsync<T>(IAsyncEnumerable<T> stream,
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
		
		protected void Write(string text)
		{
			var now = DateTime.Now;
			var message =
				$"[{Thread.CurrentThread.ManagedThreadId}] {((int) (now - _startTime).TotalMilliseconds).ToString("00,000")}ms (Δ {((int) (now - _lastTimeStamp).TotalMilliseconds).ToString("000")}ms): {text}";
			_output.WriteLine(message);
			Debug.WriteLine(message);
			_lastTimeStamp = now;
		}
	}
}