using System;
using System.Collections.Generic;
using Cinch;

namespace HartAnalyzer.Services
{
    public class TestMessageBoxService : IMessageBoxService
    {
        public Queue<Func<CustomDialogResults>> ShowYesNoResponders { get; set; }
        public Queue<Func<CustomDialogResults>> ShowYesNoCancelResponders { get; set; }
        public Queue<Func<CustomDialogResults>> ShowOkCancelResponders { get; set; }

        public List<MessageBoxRequest> ShowErrorRequests { get; set; }
        public List<MessageBoxRequest> ShowInformationRequests { get; set; }
        public List<MessageBoxRequest> ShowWarningRequests { get; set; }
        public List<MessageBoxRequest> ShowYesNoRequests { get; set; }
        public List<MessageBoxRequest> ShowYesNoCancelRequests { get; set; }
        public List<MessageBoxRequest> ShowOkCancelRequests { get; set; }

        public TestMessageBoxService()
        {
            ShowYesNoResponders = new Queue<Func<CustomDialogResults>>();
            ShowYesNoCancelResponders = new Queue<Func<CustomDialogResults>>();
            ShowOkCancelResponders = new Queue<Func<CustomDialogResults>>();

            ShowErrorRequests = new List<MessageBoxRequest>();
            ShowInformationRequests = new List<MessageBoxRequest>();
            ShowWarningRequests = new List<MessageBoxRequest>();
            ShowYesNoRequests = new List<MessageBoxRequest>();
            ShowYesNoCancelRequests = new List<MessageBoxRequest>();
            ShowOkCancelRequests = new List<MessageBoxRequest>();
        }

        public void ShowError(string message)
        {
            ShowErrorRequests.Add(new MessageBoxRequest { Message = message });
        }

        public void ShowError(string message, string caption)
        {
            ShowErrorRequests.Add(new MessageBoxRequest { Message = message, Caption = caption });
        }

        public void ShowInformation(string message)
        {
            ShowInformationRequests.Add(new MessageBoxRequest { Message = message });
        }

        public void ShowInformation(string message, string caption)
        {
            ShowInformationRequests.Add(new MessageBoxRequest { Message = message, Caption = caption});
        }

        public void ShowWarning(string message)
        {
            ShowWarningRequests.Add(new MessageBoxRequest { Message = message });
        }

        public void ShowWarning(string message, string caption)
        {
            ShowWarningRequests.Add(new MessageBoxRequest { Message = message, Caption = caption });
        }

        public CustomDialogResults ShowYesNo(string message, CustomDialogIcons icon)
        {
            if (ShowYesNoResponders.Count == 0)
                throw new ApplicationException("TestMessageBoxService ShowYesNo method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");

            ShowYesNoRequests.Add(new MessageBoxRequest { Message = message, Icon = icon });

            return ShowYesNoResponders.Dequeue()();
        }

        public CustomDialogResults ShowYesNo(string message, string caption, CustomDialogIcons icon)
        {
            if (ShowYesNoResponders.Count == 0)
                throw new ApplicationException("TestMessageBoxService ShowYesNo method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");

            ShowYesNoRequests.Add(new MessageBoxRequest { Message = message, Caption = caption, Icon = icon });

            return ShowYesNoResponders.Dequeue()();
        }

        public CustomDialogResults ShowYesNo(string message, string caption, CustomDialogIcons icon, CustomDialogResults defaultResult)
        {
            if (ShowYesNoResponders.Count == 0)
                throw new ApplicationException("TestMessageBoxService ShowYesNo method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");

            ShowYesNoRequests.Add(new MessageBoxRequest { Message = message, Caption = caption, Icon = icon, DefaultResult = defaultResult });

            return ShowYesNoResponders.Dequeue()();
        }

        public CustomDialogResults ShowYesNoCancel(string message, CustomDialogIcons icon)
        {
            if (ShowYesNoCancelResponders.Count == 0)
                throw new ApplicationException("TestMessageBoxService ShowYesNoCancel method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");

            ShowYesNoCancelRequests.Add(new MessageBoxRequest { Message = message, Icon = icon });

            return ShowYesNoCancelResponders.Dequeue()();
        }

        public CustomDialogResults ShowYesNoCancel(string message, string caption, CustomDialogIcons icon)
        {
            if (ShowYesNoCancelResponders.Count == 0)
                throw new ApplicationException("TestMessageBoxService ShowYesNoCancel method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");

            ShowYesNoCancelRequests.Add(new MessageBoxRequest { Message = message, Caption = caption, Icon = icon });

            return ShowYesNoCancelResponders.Dequeue()();
        }

        public CustomDialogResults ShowYesNoCancel(string message, string caption, CustomDialogIcons icon, CustomDialogResults defaultResult)
        {
            if (ShowYesNoCancelResponders.Count == 0)
                throw new ApplicationException("TestMessageBoxService ShowYesNoCancel method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");

            ShowYesNoCancelRequests.Add(new MessageBoxRequest { Message = message, Caption = caption, Icon = icon, DefaultResult = defaultResult });

            return ShowYesNoCancelResponders.Dequeue()();
        }

        public CustomDialogResults ShowOkCancel(string message, CustomDialogIcons icon)
        {
            if (ShowOkCancelResponders.Count == 0)
                throw new ApplicationException("TestMessageBoxService ShowOkCancel method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");

            ShowOkCancelRequests.Add(new MessageBoxRequest { Message = message, Icon = icon });

            return ShowOkCancelResponders.Dequeue()();
        }

        public CustomDialogResults ShowOkCancel(string message, string caption, CustomDialogIcons icon)
        {
            if (ShowOkCancelResponders.Count == 0)
                throw new ApplicationException("TestMessageBoxService ShowOkCancel method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");

            ShowOkCancelRequests.Add(new MessageBoxRequest { Message = message, Caption = caption, Icon = icon });

            return ShowOkCancelResponders.Dequeue()();
        }

        public CustomDialogResults ShowOkCancel(string message, string caption, CustomDialogIcons icon, CustomDialogResults defaultResult)
        {
            if (ShowOkCancelResponders.Count == 0)
                throw new ApplicationException("TestMessageBoxService ShowOkCancel method expects a Func<CustomDialogResults> callback \r\ndelegate to be enqueued for each Show call");

            ShowOkCancelRequests.Add(new MessageBoxRequest { Message = message, Caption = caption, Icon = icon, DefaultResult = defaultResult });

            return ShowOkCancelResponders.Dequeue()();
        }

        public struct MessageBoxRequest
        {
            public string Message;
            public string Caption;
            public CustomDialogIcons Icon;
            public CustomDialogResults DefaultResult;
        }
    }
}