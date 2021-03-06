// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Data.Common;

namespace Castle.ActiveRecord.Testing
{
	using System;
	using System.Data;
	using System.Reflection;
	using System.Collections.Generic;

	using Castle.ActiveRecord;
	using Castle.ActiveRecord.Framework.Config;

	using NHibernate.Connection;

	/// <summary>
	/// ConnectionProvider for Sqlite in memory tests, that suppresses closing
	/// the connection to keep the data until the test is finished.
	/// </summary>
	public class InMemoryConnectionProvider : DriverConnectionProvider
	{
		/// <summary>
		/// The connection to the database
		/// </summary>
		public static DbConnection Connection = null;

		/// <summary>
		/// Called by the framework.
		/// </summary>
		/// <returns>A connection to the database</returns>
		public override DbConnection GetConnection()
		{
			if (Connection == null)
				Connection = base.GetConnection();

			return Connection;
		}

		/// <summary>
		/// No-Op.
		/// </summary>
		/// <param name="conn">The connection to close.</param>
		public override void CloseConnection(DbConnection conn) { }

		/// <summary>
		/// Closes the connection after the tests.
		/// </summary>
		public static void Restart()
		{
			if (Connection != null)
			{
				Connection.Close();
				Connection = null;
			}
		}
	}
}