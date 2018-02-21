﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagTool.Shaders
{
    public class XboxShaderInfo
    {
        public uint DataAddress { get; set; }
        public uint DebugInfoOffset { get; set; }
        public uint DebugInfoSize { get; set; }
        public string DatabasePath { get; set; }
        public uint DataSize { get; set; }
        public uint ConstantDataSize { get; set; }
        public uint CodeDataSize { get; set; }
    }
}
