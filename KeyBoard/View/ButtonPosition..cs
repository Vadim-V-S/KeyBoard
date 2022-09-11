using System;

namespace KeyBoard.View
{
    public struct ButtonPosition
    {
        private double FieldWidth { get; }
        private int ButtonWidth { get; }
        public int GapWidth { get; set; }
        public int ButtonsNumber { get; set; }

        public ButtonPosition(double fieldWidth, int buttonWidth)
        {
            this = new ButtonPosition();

            FieldWidth = fieldWidth;
            ButtonWidth = buttonWidth;

            ButtonsNumber = GetButtonsNumber();
            GapWidth = GetGap();
        }

        public int GetButtonsNumber()
        {
            return (int)(FieldWidth / ButtonWidth);
        }

        public int GetGap()
        {
            double gap = (FieldWidth - (ButtonsNumber * ButtonWidth)) / ButtonsNumber;
            return (int)Math.Round(gap, 1);
        }
    }
}
