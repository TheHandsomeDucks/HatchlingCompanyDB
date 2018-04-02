﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatchlingCompany.Utils.Contracts
{
    public interface IExporter
    {
        void Export(string fileType, string text);

        //void Export(object obj, string path);
    }
}
