﻿using AkkaChat.Features.Common;

namespace AkkaChat.Features.Settings
{
    public class DesignSettingsVm : BindableBase, ISettingsVm
    {
        public string Title { get; } = "Design title";
        public string UserName { get; set; } = "USer";
        public bool IsOnline { get; } = true;

        public void Join()
        {
        }
    }
}