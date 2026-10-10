// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uno.UI.RuntimeTests;

namespace CommunityToolkit.WinUI.RuntimeTests.Tests
{
    // The generic constraints of these methods changed for Uno Platform 7, where DependencyObject is a class.
    [TestClass]
    [RunsOnUIThread]
    public class Given_DependencyObjectExtensions
    {
        [TestMethod]
        public async Task When_FindDescendant_Then_Returns_First_Match()
        {
            var (root, first, second) = CreateTree();
            await UIHelper.Load(root);

            Assert.AreSame(first, root.FindDescendant<TextBlock>());
            Assert.AreSame(second, root.FindDescendant<TextBlock>(t => t.Text == "second"));
            Assert.AreSame(second, root.FindDescendant<TextBlock, string>("second", (t, text) => t.Text == text));
        }

        [TestMethod]
        public async Task When_FindAscendant_Then_Returns_Closest_Match()
        {
            var (root, _, second) = CreateTree();
            await UIHelper.Load(root);

            Assert.AreSame(root, second.FindAscendant<StackPanel>());
            Assert.IsNotNull(second.FindAscendant<Border>(b => b.Tag is "outer"));
        }

        private static (StackPanel Root, TextBlock First, TextBlock Second) CreateTree()
        {
            var first = new TextBlock { Text = "first" };
            var second = new TextBlock { Text = "second" };
            var root = new StackPanel
            {
                Children =
                {
                    new Border
                    {
                        Tag = "outer",
                        Child = new Grid { Children = { first, second } },
                    },
                },
            };

            return (root, first, second);
        }
    }
}
