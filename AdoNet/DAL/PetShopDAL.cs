using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CrudAdoDotNet.DAL
{
    public class PetShopDal : IPetShopDal
    {
        readonly string connectionString = @"Server=.\BDLUCAS;Database=PetShop;Trusted_Connection=True;";

        public IEnumerable<PetShop> GetAll()
        {
            var lstPetShop = new List<PetShop>();

            using var con = new SqlConnection(connectionString);
            var cmd = new SqlCommand("SELECT Id, Name, PhoneNumber from PetShopName", con)
            {
                CommandType = CommandType.Text
            };

            con.Open();

            var rdr = cmd.ExecuteReader();

            var petShop = new PetShop();

            while (rdr.Read())
            {
                petShop.Id = Guid.Parse(rdr["Id"].ToString() ?? string.Empty);
                petShop.Name = rdr["Name"].ToString();
                petShop.PhoneNumber = rdr["PhoneNumber"].ToString();
                lstPetShop.Add(petShop);
            }
            con.Close();
            return lstPetShop;
        }

        public IEnumerable<PetShop> GetByName(string name)
        {
            var lstPetShop = new List<PetShop>();

            using var con = new SqlConnection(connectionString);
            var cmd = new SqlCommand($"SELECT * from PetShopName Where Name = '{name}'", con) { CommandType = CommandType.Text };

            con.Open();
            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var petShop = new PetShop
                {
                    Name = rdr["Name"].ToString(),
                    Id = Guid.Parse(rdr["Id"].ToString() ?? string.Empty),
                    PhoneNumber = rdr["PhoneNumber"].ToString()
                };
                lstPetShop.Add(petShop);
            }

            con.Close();

            return lstPetShop;
        }

        public void Add(PetShop petShop)
        {
            using var con = new SqlConnection(connectionString);

            var cmd = new SqlCommand("DECLARE @id uniqueIdentifier; set @id = newId(); Insert into PetShopName(Id, @Name, @PhoneNumber) values(Convert(uniqueidentifier,@id),'Name', 'PhoneNumber')", con)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("@Name", petShop.Name);
            cmd.Parameters.AddWithValue("@PhoneNumber", petShop.PhoneNumber);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Update(PetShop petShop)
        {
            
            using var con = new SqlConnection(connectionString);

            var cmd = new SqlCommand($"Update PetShopName Set Name = @Name, PhoneNumber = @PhoneNumber Where Id = @Id", con) { CommandType = CommandType.Text };

            cmd.Parameters.AddWithValue("@Id", petShop.Id);
            cmd.Parameters.AddWithValue("@Name", petShop.Name);
            cmd.Parameters.AddWithValue("@PhoneNumber", petShop.PhoneNumber);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Delete(Guid? id)
        {
            using var con = new SqlConnection(connectionString);

            var cmd = new SqlCommand($"Delete From PetShopName Where Id = '{id}'", con)
            {
                CommandType = CommandType.Text
            };

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}