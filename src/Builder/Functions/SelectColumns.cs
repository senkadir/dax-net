﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public class SelectColumns : Function
    {
        public string Table { get; set; }

        public List<string> Columns { get; set; }

        public SelectColumns()
        {
            Columns = new();
        }

        public override StringBuilder Generate(StringBuilder builder)
        {
            builder.AppendLine("SELECTCOLUMNS ( ");

            builder.AppendLine($"{Table}");

            builder.AppendJoin($", {Environment.NewLine}", Columns);

            builder.AppendLine(" )");

            return builder;
        }
    }
}
