using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ScottPlot.Avalonia;
using System;
using System.Collections.Generic;

namespace ScottPlot.Demo.Avalonia.AvaloniaDemos
{
    public class RightClickMenu : Window
    {
        private readonly AvaPlot avaPlot1;

        public RightClickMenu()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            avaPlot1 = this.Find<AvaPlot>("avaPlot1");

            avaPlot1.Plot.AddSignal(DataGen.Sin(51));
            avaPlot1.Plot.AddSignal(DataGen.Cos(51));
            avaPlot1.Refresh();

            ContextMenu contextMenu = new ContextMenu();

            contextMenu.Items = new[] {
                MakeMenuItem("Add _Sine Wave", AddSine),
                MakeMenuItem("_Clear Plot", ClearPlot)
            };

            avaPlot1.RightClicked -= avaPlot1.DefaultRightClickEvent;
            avaPlot1.RightClicked += (sender, e) =>
            {
                avaPlot1.ShowContextMenu(contextMenu);
            };
        }

        public void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private MenuItem MakeMenuItem(string label, Action onClick)
        {
            var item = new MenuItem()
            {
                Header = label
            };
            item.Click += (sender, e) => onClick();
            return item;
        }

        private void AddSine()
        {
            Random rand = new Random();
            avaPlot1.Plot.AddSignal(DataGen.Sin(51, phase: rand.NextDouble() * 1000));
            avaPlot1.Plot.AxisAuto();
            avaPlot1.Refresh();
        }

        private void ClearPlot()
        {
            avaPlot1.Plot.Clear();
            avaPlot1.Plot.AxisAuto();
            avaPlot1.Refresh();
        }
    }
}
