﻿/*
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@gmail.com>
 *
 * Copyright (C) 2015 Zongsoft Corporation <http://www.zongsoft.com>
 *
 * This file is part of Zongsoft.CoreLibrary.
 *
 * Zongsoft.CoreLibrary is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * Zongsoft.CoreLibrary is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with Zongsoft.CoreLibrary; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
 */

using System;
using System.Collections.Generic;

namespace Zongsoft.Options.Configuration
{
	public class ModuleElementCollection : OptionConfigurationElementCollection
	{
		#region 构造函数
		public ModuleElementCollection() : base("module")
		{
		}

		protected ModuleElementCollection(string elementName) : base(elementName)
		{
		}
		#endregion

		#region 公共属性
		public ModuleElement this[int index]
		{
			get
			{
				return (ModuleElement)this.Items[index];
			}
		}

		public new ModuleElement this[string name]
		{
			get
			{
				return base.Find(name) as ModuleElement;
			}
		}
		#endregion

		#region 重写方法
		protected override OptionConfigurationElement CreateNewElement()
		{
			return new ModuleElement();
		}

		protected override string GetElementKey(OptionConfigurationElement element)
		{
			return ((ModuleElement)element).Name;
		}
		#endregion
	}
}
