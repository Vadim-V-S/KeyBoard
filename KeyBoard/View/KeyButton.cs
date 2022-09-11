using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyBoard.View
{
    public class KeyButton
    {
        private Form Keyboardform { get; }
        private String Symbol { get; }

        private int HorGap { get; }
        private int VertGap { get; }
        private int ButtonWidth { get; }
        private int ButtonHeight { get; }
        private string ButtonName { get; }

        public KeyButton(Form form, string symbol, int horGap, int vertGap,
            int buttonWidth, int buttonHeight, string buttonName)
        {
            Keyboardform = form;
            Symbol = symbol;
            HorGap = horGap;
            VertGap = vertGap;
            ButtonWidth = buttonWidth;
            ButtonHeight = buttonHeight;
            ButtonName = buttonName;
        }

        public Button GenerateButton(Color color)
        {
            Button newButton = new Button();
            Keyboardform.Controls.Add(newButton);

            newButton.Text = Symbol;
            newButton.Location = new Point(HorGap, VertGap);
            newButton.Size = new Size(ButtonWidth, ButtonHeight);
            newButton.Font = new Font(newButton.Font.FontFamily, 20, FontStyle.Bold);
            newButton.BackColor = Color.Black;
            newButton.ForeColor = color;
            newButton.FlatStyle = FlatStyle.Flat;
            newButton.FlatAppearance.BorderColor = Color.LightCyan;
            newButton.FlatAppearance.BorderSize = 1;

            newButton.Name = ButtonName;

            return newButton;
        }
    }
}
