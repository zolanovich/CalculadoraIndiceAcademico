﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace Back
{
	public class ConnectionToSql
	{
		private SqlConnection Conexion = new SqlConnection("Server =.;DataBase = ProyectoIDS311; Integrated Security = true");
		//Open Connection String
		public SqlConnection OpenConnection()
		{
			if (Conexion.State == ConnectionState.Closed)
				Conexion.Open();
			return Conexion;
		}
		//Close Connection String
		public SqlConnection CloseConnection()
		{
			if (Conexion.State == ConnectionState.Open)
				Conexion.Close();
			return Conexion;
		}
		public class User : ConnectionToSql
		{
			private ConnectionToSql connection = new ConnectionToSql();

			SqlCommand command = new SqlCommand();

			public bool Login(string user, string pass)
			{
				command.Connection = connection.OpenConnection();
				command.CommandText = "select * from Register where StudentID=@User and StPassword=@pass";
				command.Parameters.AddWithValue("@user", user);
				command.Parameters.AddWithValue("@pass", pass);
				command.CommandType = CommandType.Text;
				SqlDataReader reader = command.ExecuteReader();
				if (reader.HasRows)
				{
					return true;
				}
				else
					return false;
				connection.CloseConnection();
			}
		}
		public class UserModel
		{
			User User = new User();
			public bool LoginUser(string user, string pass)
			{
				return User.Login(user, pass);
			}
		}

	}

}
