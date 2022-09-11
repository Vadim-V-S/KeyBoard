using KeyBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoard.Presenter
{
    public class MainPresenter
    {
        private readonly IMainForm view;
        private readonly IEnKeyBoard keyboard;
        private readonly IMessageService messageService;

        public MainPresenter(IMainForm view, IEnKeyBoard keyboard, IMessageService messageService)
        {
            this.view = view;
            this.keyboard = keyboard;
            this.messageService = messageService;

            view.SymbolAdded += SymbolAdded;
            view.TextCopied += TextCopied;
            view.ImageCopied += View_ImageCopied;
            view.ContentChanged += ContentChanged;
            view.ClearText += View_ClearText;
            view.AboutInfo += View_AboutInfo;
        }

        private void View_AboutInfo(object sender, EventArgs e)
        {
            messageService.ShowMessage("Thank you Sergey");
        }

        private void ContentChanged(object sender, EventArgs e)
        {
            string text = view.Content;
            view.SymbolCounter = string.Format($"number of symbols - {keyboard.CountSymbols(text)}");
        }

        private void View_ImageCopied(object sender, EventArgs e)
        {
            try
            {
                string text = string.Format($"[ {view.Content} ]");
                keyboard.TextToImageCopy(text);
                messageService.ShowMessage("Copied as image");
            }
            catch (FormatException)
            {
                messageService.ShowError($"You can not copy \"{{\" or \"}}\" symbol ");
            }
            catch (Exception ex)
            {
                messageService.ShowError($"Text copy error ({ex.Message})");
            }
        }

        private void TextCopied(object sender, EventArgs e)
        {
            try
            {
                string text = string.Format($"[ {view.Content} ]");

                keyboard.TextCopy(text);
                messageService.ShowMessage("Copied as text");
            }
            catch (FormatException)
            {
                messageService.ShowError($"You can not copy \"{{\" or \"}}\" symbols ");
            }
            catch (Exception ex)
            {
                messageService.ShowError($"Text copy error ({ex.Message})");
            }
        }

        private void SymbolAdded(object sender, EventArgs e)
        {
            try
            {
                string text = keyboard.SetText(view.CursorStartPosition, view.Content, (string)sender);
                view.Content = text;
            }
            catch (Exception ex)
            {
                messageService.ShowError($"Symbol adding error ({ex.Message})");
            }
        }

        private void View_ClearText(object sender, EventArgs e)
        {
            try
            {
                view.Content = "";
            }
            catch (Exception ex)
            {
                messageService.ShowError($"Context clear error ({ex.Message})");
            }
        }
    }
}
