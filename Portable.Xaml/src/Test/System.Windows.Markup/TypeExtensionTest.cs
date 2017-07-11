//
// Copyright (C) 2010 Novell Inc. http://novell.com
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
#if PCL
using Portable.Xaml.Markup;
using Portable.Xaml.ComponentModel;
using Portable.Xaml;
using Portable.Xaml.Schema;
#else
using System.Windows.Markup;
using System.ComponentModel;
using System.Xaml;
using System.Xaml.Schema;
#endif

using Category = NUnit.Framework.CategoryAttribute;

namespace MonoTests.Portable.Xaml.Markup
{
	[TestFixture]
	public class TypeExtensionTest
	{
		[Test]
		public void ConstructorNullType ()
		{
			Assert.Throws<ArgumentNullException> (() => new TypeExtension ((Type) null));
		}

		[Test]
		public void ConstructorNullName ()
		{
			Assert.Throws<ArgumentNullException> (() => new TypeExtension ((string) null));
		}

		[Test]
		public void ProvideValueWithoutTypeOrName ()
		{
			var te = new TypeExtension ();
			Assert.Throws<InvalidOperationException> (() => te.ProvideValue (null));
		}

		[Test]
		public void ProvideValueWithType ()
		{
			var x = new TypeExtension (typeof (int));
			Assert.AreEqual (typeof (int), x.ProvideValue (null), "#1"); // serviceProvider is not required.
		}

		[Test]
		public void ProvideValueWithNameWithoutResolver ()
		{
			var x = new TypeExtension ("System.Int32");
			Assert.Throws<ArgumentNullException> (() => x.ProvideValue (null)); // serviceProvider is required.
		}

		[Test]
		public void ProvideValueWithNameWithProviderNoResolver ()
		{
			var x = new TypeExtension ("System.Int32");
			Assert.Throws<InvalidOperationException> (() => x.ProvideValue (new Resolver (false, false)));
		}

		[Test]
		public void ProvideValueWithNameWithProviderResolveFail ()
		{
			var x = new TypeExtension ("System.Int32");
			Assert.Throws<InvalidOperationException> (() => x.ProvideValue (new Resolver (true, false))); // raise an error (do not return null)
		}

		[Test]
		public void ProvideValueWithNameWithProviderResolveSuccess ()
		{
			var x = new TypeExtension ("System.Int32");
			x.ProvideValue (new Resolver (true, true));
		}

		class Resolver : IServiceProvider, IXamlTypeResolver
		{
			bool works, resolves;

			public Resolver (bool worksFine, bool resolvesFine)
			{
				works = worksFine;
				resolves = resolvesFine;
			}

			public object GetService (Type serviceType)
			{
				Assert.AreEqual (typeof (IXamlTypeResolver), serviceType, "TypeToResolve");
				return works ? this : null;
			}

			public Type Resolve (string name)
			{
				return resolves ? Type.GetType (name) : null;
			}
		}
	}
}
