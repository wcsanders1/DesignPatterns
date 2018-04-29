﻿using System.Collections.Generic;

namespace Mediator
{
    public static class DefaultArguments
    {
        public static List<string> GetDefaultArguments()
        {
            return new List<string>
            {
                "I don't think that's a very good idea.",
                "Well, I can sort of see your point.",
                "Holy heck! I don't know about the efficacy of that.",
                "Geez, I think there might be a better way of thinking!",
                "You present an interesting point, but I don't know about it."
            };
        }
    }
}
