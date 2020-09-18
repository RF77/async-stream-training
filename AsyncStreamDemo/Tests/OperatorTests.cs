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

using Shared;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
	public class OperatorTests : UnitTestBase
	{
		private Operators _unitUnderTest;

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

		public OperatorTests(ITestOutputHelper output) : base(output)
		{
			_unitUnderTest = new Operators();
		}
	}
}