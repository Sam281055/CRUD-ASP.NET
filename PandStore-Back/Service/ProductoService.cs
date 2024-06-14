using MySql.Data.MySqlClient;
using PandStore_Back.Model;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PandStore_Back.Service
{
    public class ProductoService
    {
        private readonly string conexion;

        public ProductoService(IConfiguration iConfiguration)
        {
            conexion = iConfiguration.GetConnectionString("StringSQL");
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            List<Producto> prod = new List<Producto>();
            string peticion = "SELECT * FROM producto";
            using (var con = new MySqlConnection(conexion))
            {
                await con.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(peticion,con);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        prod.Add(new Producto
                        {
                            id = Convert.ToInt32(reader["Id"]),
                            name = reader["name"].ToString(),
                            picture = reader["picture"].ToString(),
                            price = Convert.ToInt32(reader["price"]),
                            amount = Convert.ToInt32(reader["amount"]),
                        });

                    }

                }
            }
            return prod;
        }

        public bool Exist(Producto producto)
        {
            bool resp = false;
            string query2 = "SELECT * FROM producto WHERE id = @id, name = @name";

            using (var con = new MySqlConnection(conexion))
            {
                    
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query2, con);
                    cmd.Parameters.AddWithValue("@id", producto.id);
                    cmd.Parameters.AddWithValue("@name", producto.name);
                    resp = cmd.ExecuteNonQuery() > 0 ? true : false;
                    con.Close();
            }
            return resp;
        }

        public async Task<bool> AgregarProducto(Producto producto)
        {
            string query = "INSERT INTO Producto (name, price, amount, picture) VALUES (@name, @price, @amount, @picture)";
            bool resp = true;
            if (Exist(producto))return resp = false;
            using (var con = new MySqlConnection(conexion)) {
                try {
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", producto.name);
                cmd.Parameters.AddWithValue("@price", producto.price);
                cmd.Parameters.AddWithValue("@amount", producto.amount);
                cmd.Parameters.AddWithValue("@picture", producto.picture);
                cmd.ExecuteNonQuery();
                con.Close();
                    return resp;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return resp = false;
                }
            }
        }

        public async Task<bool> Edit(Producto producto)
        {
            bool resp = true;
            using (var con = new MySqlConnection(conexion))
            {
                string query = "UPDATE producto SET name = @name, picture = @picture, price= @price, amount = @amount WHERE (id = @id)";
                    //string query2 = "UPDATE producto set VALUES (@name, @price, @amount, @picture) WHERE id=(@id)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                try
                {
                    cmd.Parameters.AddWithValue("@id", producto.id);
                    cmd.Parameters.AddWithValue("@name", producto.name);
                    cmd.Parameters.AddWithValue("@price", producto.price);
                    cmd.Parameters.AddWithValue("@amount", producto.amount);
                    cmd.Parameters.AddWithValue("@picture", producto.picture);
                    await con.OpenAsync();
                    resp = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                    con.Close();
                }
                catch { return resp = false;}
            }
            return resp;
        }

        public async Task<bool> EliminarProducto(int id)
        {
            bool respuesta = true;
            string peticion = "DELETE FROM producto Where id = (@id)";
            using (var con = new MySqlConnection(conexion))
            {
                MySqlCommand cmd = new MySqlCommand(peticion, con);
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                    con.Close();
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
