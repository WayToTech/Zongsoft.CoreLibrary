﻿/*
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@gmail.com>
 *
 * Copyright (C) 2015-2016 Zongsoft Corporation <http://www.zongsoft.com>
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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using Zongsoft.Common;
using Zongsoft.Reflection;

namespace Zongsoft.Runtime.Serialization
{
	public class DictionarySerializer : IDictionarySerializer
	{
		#region 单例字段
		public static readonly DictionarySerializer Default = new DictionarySerializer();
		#endregion

		public IDictionary Serialize(object graph)
		{
			var dictionary = new Dictionary<string, object>();
			this.Serialize(graph, dictionary);
			return dictionary;
		}

		public void Serialize(object graph, IDictionary dictionary)
		{
			if(graph == null)
				return;

			if(dictionary == null)
				throw new ArgumentNullException("dictionary");

			dictionary.Add("@type", graph.GetType().AssemblyQualifiedName);

			var properties = graph.GetType().GetProperties();

			foreach(var property in properties)
			{
				if(!property.CanRead)
					continue;

				if(TypeExtension.IsScalarType(property.PropertyType))
					dictionary.Add(property.Name.ToLowerInvariant(), property.GetValue(graph));
			}
		}

		public object Deserialize(IDictionary dictionary)
		{
			return this.Deserialize(dictionary, null);
		}

		public object Deserialize(IDictionary dictionary, Type type)
		{
			return this.Deserialize(dictionary, type, null);
		}

		public object Deserialize(IDictionary dictionary, Type type, Func<MemberGettingContext, MemberGettingResult> resolve)
		{
			if(type == null)
				throw new ArgumentNullException("type");

			return this.Deserialize<object>(dictionary, () => Activator.CreateInstance(type), resolve);
		}

		public T Deserialize<T>(IDictionary dictionary)
		{
			return (T)this.Deserialize(dictionary, typeof(T));
		}

		public T Deserialize<T>(IDictionary dictionary, Func<T> creator, Func<MemberGettingContext, MemberGettingResult> resolve)
		{
			if(dictionary == null)
				return default(T);

			var result = creator != null ? creator() : Activator.CreateInstance<T>();

			foreach(DictionaryEntry entry in dictionary)
			{
				if(entry.Key == null)
					continue;

				MemberAccess.SetMemberValue(result, entry.Key.ToString(), entry.Value);
			}

			return result;
		}
	}
}
