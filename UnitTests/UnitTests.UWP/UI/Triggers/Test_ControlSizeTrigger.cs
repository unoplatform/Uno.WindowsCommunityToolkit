using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Uwp.UI.Triggers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UnitTests.UWP.UI.Triggers
{
    [TestClass]
    [TestCategory("Test_ControlSizeTrigger")]
    public class Test_ControlSizeTrigger : VisualUITestBase
    {
        [DataTestMethod]
        [DataRow(450, 450, true)]
        [DataRow(400, 400, true)]
        [DataRow(500, 500, false)]
        [DataRow(399, 400, false)]
        [DataRow(400, 399, false)]
        public async Task ControlSizeTriggerTest(double width, double height, bool expectedResult)
        {
            await App.DispatcherQueue.EnqueueAsync(() =>
            {
                Grid grid = CreateGrid(width, height);
                var trigger = new ControlSizeTrigger();

                trigger.TargetElement = grid;
                trigger.MaxHeight = 500;
                trigger.MinHeight = 400;
                trigger.MaxWidth = 500;
                trigger.MinWidth = 400;

                Assert.AreEqual(expectedResult, trigger.IsActive);
            });
        }

        private Grid CreateGrid(double width, double height)
        {
            var grid = new Grid()
            {
                Height = height,
                Width = width
            };
            grid.Measure(new Windows.Foundation.Size(1000, 1000));
            grid.Arrange(new Windows.Foundation.Rect(0, 0, 1000, 1000));
            grid.UpdateLayout();

            return grid;
        }
    }
}
