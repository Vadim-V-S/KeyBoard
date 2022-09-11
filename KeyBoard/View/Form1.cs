using KeyBoard.Model;
using KeyBoard.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace KeyBoard
{
    public interface IMainForm
    {
        string Content { get; set; }
        string SymbolCounter { get; set; }
        int CursorStartPosition { get; set; }

        event EventHandler SymbolAdded;
        event EventHandler ContentChanged;
        event EventHandler TextCopied;
        event EventHandler ImageCopied;
        event EventHandler AboutInfo;
        event EventHandler ClearText;
    }

    public partial class MainForm : Form, IMainForm
    {
        private int ButtonFirstPosition;
        private bool IniCheck = false;

        public MainForm()
        {
            InitializeComponent();

            MakeKeyboard();

            CopyBtn.Click += CopyBtn_Click;
            CopyBtn.Paint += Btn_Paint;

            CopyImageBtn.Click += CopyImageBtn_Click;
            CopyImageBtn.Paint += Btn_Paint;

            DisplayTxt.TextChanged += DisplayTxt_TextChanged;
            DisplayTxt.GotFocus += DisplayTxt_GotFocus;

            ClearBtn.Click += ClearBtn_Click;
            ClearBtn.Paint += Btn_Paint;

            About.Click += About_Click;
            this.ResizeEnd += MainForm_Resize;
            this.ResizeBegin += MainForm_ResizeBegin;
        }

        #region Реализация свойств
        public string Content
        {
            get { return DisplayTxt.Text; }
            set { DisplayTxt.Text = value; }
        }
        public string SymbolCounter
        {
            get { return NumSymbolLbl.Text; }
            set { NumSymbolLbl.Text = value; }
        }
        public int CursorStartPosition
        {
            get { return DisplayTxt.SelectionStart; }
            set { DisplayTxt.SelectionStart = value; }
        }
        #endregion

        #region Проброс событий
        private void AddButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SymbolAdded?.Invoke(btn.Text, EventArgs.Empty);
            DisplayTxt.SelectionStart = DisplayTxt.Text.Length + 1;
        }
        private void CopyBtn_Click(object sender, EventArgs e)
        {
            TextCopied?.Invoke(sender, EventArgs.Empty);
        }
        private void CopyImageBtn_Click(object sender, EventArgs e)
        {
            ImageCopied?.Invoke(sender, EventArgs.Empty);
        }
        private void DisplayTxt_TextChanged(object sender, EventArgs e)
        {
            IniCheck = true;
            ContentChanged?.Invoke(sender, EventArgs.Empty);
        }
        private void DisplayTxt_GotFocus(object sender, EventArgs e)
        {
            if (!IniCheck)
                ((TextBox)sender).Parent.Focus();
        }
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearText?.Invoke(sender, EventArgs.Empty);
        }
        private void About_Click(object sender, EventArgs e)
        {
            AboutInfo?.Invoke(sender, EventArgs.Empty);
        }
        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            oldSize = ClientSize;
        }

        private Size oldSize = new Size();
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (oldSize == ClientSize)
                return;

            this.SuspendLayout();

            RemoveButtons();
            MakeKeyboard();
            this.ResumeLayout();
        }

        private void Btn_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            LinearGradientBrush linGrBrush = new LinearGradientBrush(new Point(0, -(control.Height + (control.Height / 2))), new Point(0, control.Height), Color.FromArgb(255, 200, 200, 200), Color.FromArgb(0, 0, 0, 0));
            //Pen pen = new Pen(linGrBrush);
            e.Graphics.FillRectangle(linGrBrush, 0, 0, this.Width, this.Height);
        }

        public event EventHandler SymbolAdded;
        public event EventHandler TextCopied;
        public event EventHandler ImageCopied;
        public event EventHandler ContentChanged;
        public event EventHandler ClearText;
        public event EventHandler AboutInfo;
        #endregion

        #region Form setting
        private void MakeKeyboard()
        {
            ButtonFirstPosition = DisplayTxt.Height + 55;

            SetButtons(new VowelSymbol(), Color.MistyRose);
            SetButtons(new DiphtongSymbol(), Color.Yellow);
            SetButtons(new ConsonantSymbol(), Color.DeepSkyBlue);
            SetButtons(new SpecificSymbol(), Color.White);

            DisplayTxt.DeselectAll();
        }

        private void SetButtons(ISymbolsSet symbolProvider, Color buttonColor)
        {
            const int buttonWidth = 70;
            const int buttonHeight = 45;
            ButtonPosition buttonPosition = new ButtonPosition(DisplayTxt.Size.Width + 20, buttonWidth);
            int buttonsNumber = buttonPosition.ButtonsNumber;
            int gapWidth = buttonPosition.GapWidth;

            int HorGap = 10;
            int VertGap = GetButtonFirstPosition(buttonHeight);

            int buttonNumber = 1;

            foreach (var item in symbolProvider.Symbols)
            {
                string buttonName = string.Format($"SymbolBtn{buttonNumber}");

                KeyButton keyButton = new KeyButton(this, item, HorGap, VertGap, buttonWidth, buttonHeight, buttonName);
                Button button = keyButton.GenerateButton(buttonColor);

                HorGap += button.Size.Width + gapWidth;

                //fitting buttons to the form
                if (buttonNumber % buttonsNumber == 0)
                {
                    VertGap += button.Size.Height + 3;
                    HorGap = 10;
                }
                buttonNumber++;

                ButtonFirstPosition = button.Location.Y + 15;

                button.Click += AddButton_Click;
                button.Paint += Btn_Paint;
            }
            ResizeForm(buttonHeight + 30);
        }

        private int GetButtonFirstPosition(int buttonHeight)
        {
            return ButtonFirstPosition + buttonHeight;
        }

        private void ResizeForm(int buttonHeight)
        {
            this.Size = new Size(this.Size.Width, ButtonFirstPosition + buttonHeight + 30);
        }

        private void RemoveButtons()
        {
            List<Button> buttons = this.Controls.OfType<Button>().ToList();
            foreach (Button btn in buttons)
            {
                if (btn.Name.Contains("SymbolBtn"))
                {
                    this.Controls.Remove(btn);
                    btn.Dispose();
                }
            }
        }
        #endregion
    }
}
