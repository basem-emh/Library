﻿namespace Library.BLL.Common.Helpers.Emails
{
    public class Email
    {
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
    }
}
