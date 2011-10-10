﻿// ActiveTime
// Copyright (C) 2011 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DustInTheWind.ActiveTime.Common
{
    public class ShellInfo
    {
        public Type ShellType { get; private set; }
        public string ShellName { get; private set; }
        public string OwnerName { get; private set; }

        public ShellInfo(string shellName, Type shellType, string ownerName = null)
        {
            if (shellName == null)
                throw new ArgumentNullException("shellName");

            if (shellType == null)
                throw new ArgumentNullException("shellType");

            if (shellType.BaseType != typeof(Window))
                throw new ArgumentException("A shell must be derived from System.Windows.Window base class.", "shellType");

            this.ShellName = shellName;
            this.ShellType = shellType;
            this.OwnerName = ownerName;
        }
    }
}