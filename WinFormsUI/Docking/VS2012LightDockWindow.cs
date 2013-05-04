using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WeifenLuo.WinFormsUI.Docking
{
    /// <summary>
    /// Dock window of Visual Studio 2012 Light theme.
    /// </summary>
    [ToolboxItem(false)]
    internal class VS2012LightDockWindow : DockWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VS2012LightDockWindow"/> class.
        /// </summary>
        /// <param name="dockPanel">The dock panel.</param>
        /// <param name="dockState">State of the dock.</param>
        public VS2012LightDockWindow(DockPanel dockPanel, DockState dockState) : base(dockPanel, dockState)
        {
        }

        public override Rectangle DisplayingRectangle
        {
            get
            {
                Rectangle rect = ClientRectangle;
                if (DockState == DockState.DockLeft)
                    rect.Width -= Measures.SplitterSize;
                else if (DockState == DockState.DockRight)
                {
                    rect.X += Measures.SplitterSize;
                    rect.Width -= Measures.SplitterSize;
                }
                else if (DockState == DockState.DockTop)
                    rect.Height -= Measures.SplitterSize;
                else if (DockState == DockState.DockBottom)
                {
                    rect.Y += Measures.SplitterSize;
                    rect.Height -= Measures.SplitterSize;
                }

                return rect;
            }
        }
        
        internal class VS2012LightDockWindowSplitterControl : SplitterBase
        {
            private DockPanel Panel { get; set; }

            public VS2012LightDockWindowSplitterControl(DockPanel panel)
            {
                Panel = panel;
                Brush = new SolidBrush(Panel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor);
            }

            private SolidBrush Brush { get; set; }

            protected override int SplitterSize
            {
                get { return Measures.SplitterSize; }
            }

            protected override void StartDrag()
            {
                DockWindow window = Parent as DockWindow;
                if (window == null)
                    return;

                window.DockPanel.BeginDrag(window, window.RectangleToScreen(Bounds));
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Rectangle rect = ClientRectangle;

                if (Dock==DockStyle.Left || Dock==DockStyle.Right)
                    e.Graphics.FillRectangle(Brush, rect.X + Measures.SplitterSize / 2 - 1, rect.Y, Measures.SplitterSize/3, rect.Height);
                else if (Dock==DockStyle.Top || Dock==DockStyle.Bottom)
                        e.Graphics.FillRectangle(Brush, rect.X, rect.Y, rect.Width, Measures.SplitterSize);
            }
        }
    }
}