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
using System.Diagnostics;
using System.Threading;
using Xunit.Abstractions;

namespace Shared
{
	public class MyTestOutputHelper
	{
		private readonly ITestOutputHelper _output;
		private readonly DateTime _startTime = DateTime.Now;
		private DateTime _lastTimeStamp = DateTime.Now;

		public MyTestOutputHelper(ITestOutputHelper testOutputHelper)
		{
			_output = testOutputHelper;
		}

		public void Write(string text)
		{
			var now = DateTime.Now;
			var message =
				$"[{Thread.CurrentThread.ManagedThreadId}] {((int)(now - _startTime).TotalMilliseconds).ToString("00,000")}ms (Δ {((int)(now - _lastTimeStamp).TotalMilliseconds).ToString("000")}ms): {text}";
			_output.WriteLine(message);
			Debug.WriteLine(message);
			_lastTimeStamp = now;
		}
	}
}