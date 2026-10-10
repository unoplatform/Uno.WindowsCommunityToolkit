// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.WinUI.UI;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uno.UI.RuntimeTests;

namespace CommunityToolkit.WinUI.RuntimeTests.Tests
{
    [TestClass]
    [RunsOnUIThread]
    public class Given_MarkdownTextBlock
    {
        // MarkdownTextBlock rendered nothing on Uno Platform 7: its ThemeListener threw without a CoreWindow.
        [TestMethod]
        public async Task When_Text_Has_A_Code_Block_Then_It_Is_Rendered_And_Highlighted()
        {
            var renders = new List<MarkdownRenderedEventArgs>();
            var markdown = new MarkdownTextBlock();
            markdown.MarkdownRendered += (_, e) => renders.Add(e);
            markdown.Text = "# Title\n\nSome **bold** text.\n\n```csharp\npublic class Foo { }\n```\n";

            await UIHelper.Load(markdown);
            await UIHelper.WaitForIdle();

            Assert.IsTrue(renders.Count > 0, "MarkdownRendered was not raised");
            Assert.IsTrue(renders.All(e => e.Exception is null), string.Join("; ", renders.Select(e => e.Exception?.ToString())));

            // ColorCode colors the 'public' keyword of the code block.
            var keyword = markdown.FindDescendants()
                .OfType<RichTextBlock>()
                .SelectMany(r => r.Blocks.OfType<Paragraph>())
                .SelectMany(p => Flatten(p.Inlines))
                .OfType<Run>()
                .FirstOrDefault(r => r.Text == "public");
            Assert.IsNotNull(keyword, "The code block was not rendered");
            Assert.IsNotNull(keyword.Foreground, "The code block was not highlighted");
        }

        private static IEnumerable<Inline> Flatten(InlineCollection inlines)
        {
            foreach (var inline in inlines)
            {
                yield return inline;

                if (inline is Span span)
                {
                    foreach (var child in Flatten(span.Inlines))
                    {
                        yield return child;
                    }
                }
            }
        }
    }
}
