﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using ICSharpCode.PythonBinding;
using ICSharpCode.Scripting.Tests.Utils;
using ICSharpCode.SharpDevelop.Editor;
using ICSharpCode.SharpDevelop.Editor.CodeCompletion;
using NUnit.Framework;
using PythonBinding.Tests.Utils;

namespace PythonBinding.Tests.Completion
{
	/// <summary>
	/// Tests that the From keyword is correctly identified as a 
	/// importable code completion keyword.
	/// </summary>
	[TestFixture]
	public class CodeCompletionBindingFromImportCompletionTests
	{
		MockTextEditor fakeTextEditor;
		TestablePythonCodeCompletionBinding codeCompletionBinding;
		
		void CreatePythonCodeCompletionBinding()
		{
			fakeTextEditor = new MockTextEditor();
			codeCompletionBinding = new TestablePythonCodeCompletionBinding();			
		}
		
		[Test]
		public void HandleKeyword_KeywordIsFrom_ReturnsTrue()
		{
			CreatePythonCodeCompletionBinding();
			bool handled = codeCompletionBinding.HandleKeyword(fakeTextEditor, "from");
			Assert.IsTrue(handled);
		}
		
		[Test]
		public void HandleKeyword_KeywordIsFrom_PythonDotCodeCompletionItemProviderUsedToShowCompletionWindow()
		{
			CreatePythonCodeCompletionBinding();
			codeCompletionBinding.HandleKeyword(fakeTextEditor, "from");
			ITextEditor textEditor = codeCompletionBinding.TextEditorPassedToShowCompletionWindow;
			
			Assert.AreEqual(fakeTextEditor, textEditor);
		}
		
		[Test]
		public void HandleKeyword_KeywordIsFrom_PythonCodeCompletionItemProviderCreated()
		{
			CreatePythonCodeCompletionBinding();
			codeCompletionBinding.HandleKeyword(fakeTextEditor, "from");
			
			PythonCodeCompletionItemProvider provider = codeCompletionBinding.KeywordCompletionItemProviderCreated as PythonCodeCompletionItemProvider;
			
			Assert.IsNotNull(provider);
		}
		
		[Test]
		public void HandleKeyword_KeywordIsFrom_PythonCodeCompletionItemProviderPassedToShowCompletionWindow()
		{
			CreatePythonCodeCompletionBinding();
			codeCompletionBinding.HandleKeyword(fakeTextEditor, "from");
			
			AbstractCompletionItemProvider provider = codeCompletionBinding.CompletionItemProviderUsedWhenDisplayingCodeCompletionWindow;
			
			Assert.AreSame(codeCompletionBinding.KeywordCompletionItemProviderCreated, provider);
		}
	}
}
