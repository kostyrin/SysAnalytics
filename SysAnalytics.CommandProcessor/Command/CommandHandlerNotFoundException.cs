﻿using System;

namespace SysAnalytics.CommandProcessor.Command
{
    public class CommandHandlerNotFoundException : Exception
    {
        public CommandHandlerNotFoundException(Type type) : base(string.Format("Command handler not found for command type: {0}", type))
        {
        }
    }
}

