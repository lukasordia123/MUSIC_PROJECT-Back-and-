using Microsoft.Data.SqlClient;
using Models;
using Service.interfaces;
using System.Data;
using System.Reflection;

namespace Service
{
    public class SqlServerDatabaseConnection : IDatabaseConnection
    {
        public async Task<List<Posts>> GetAllRituals()
        {
            const string sqlExpression = "sp_GetAll_Rituals";
            List<Posts> result = new();

            using (SqlConnection connection = new(GlobalConfig.ConnectionString))
            {
                try
                {
                    SqlCommand command = new(sqlExpression, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new Posts
                            {
                                PostId = reader.GetInt32("RitualId"),
                                Title = reader.GetString("Tile"),
                                Location = reader.GetString("Location"),
                                SearchAddress = reader.GetString("SearchAddress"),
                                Text = reader.GetString("Text"),
                                ImgUrl = reader.GetString("ImgUrl"),
                                Datetime = reader.GetDateTime("DateTime")
                            });
                        }
                    }

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }

                return result;
            }
        }
        //public async Task<List<Posts>> GetAllPosts()
        //{
        //    const string sqlExpression = "sp_GetAll_Users";
        //    List<Posts> result = new();

        //    using (SqlConnection connection = new(GlobalConfig.ConnectionString))
        //    {
        //        try
        //        {
        //            SqlCommand command = new(sqlExpression, connection);
        //            command.CommandType = CommandType.StoredProcedure;

        //            await connection.OpenAsync();
        //            SqlDataReader reader = await command.ExecuteReaderAsync();

        //            if (reader.HasRows)
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    result.Add(new Posts
        //                    {
        //                        PostId  = reader.GetInt32("PostId"),
        //                        Title = reader.GetString("Title"),
        //                        Location = reader.GetString("Location"),
        //                        SearchAddress = reader.GetString("SearchAddress"),
        //                        Text = reader.GetString("Text"),
        //                        ImgUrl = reader.GetString("ImgUrl"),
        //                        UserId = reader.GetInt32("UserID"),
        //                    });
        //                }
        //            }

        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            await connection.CloseAsync();
        //        }

        //        return result;
        //    }
        //}

        public async Task<List<User>> GetAllUSers()
        {
            const string sqlExpression = "sp_GetAll_Users";
            List<User> result = new();

            using (SqlConnection connection = new(GlobalConfig.ConnectionString))
            {
                try
                {
                    SqlCommand command = new(sqlExpression, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new User
                            {
                                ID = reader.GetInt32("UserID"),
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                Email = reader.GetString("Email"),
                                PasswordHash = reader.GetString("PasswordHash"),
                                PasswordSalt = reader.GetString("PasswordSalt")
                            });
                        }
                    }

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }

                return result;
            }
        }

        public async Task<Posts> InsertRitual(Posts model)
        {
            const string sqlExpression = "sp_Insert_Ritual";

            using (SqlConnection connection = new(GlobalConfig.ConnectionString))
            {
                try
                {
                    SqlCommand command = new(sqlExpression, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("title", model.Title);
                    command.Parameters.AddWithValue("location", model.Location);
                    command.Parameters.AddWithValue("searchaddress", model.SearchAddress);
                    command.Parameters.AddWithValue("text", model.Text);
                    command.Parameters.AddWithValue("imgurl", model.ImgUrl);
                    command.Parameters.AddWithValue("datetime", model.Datetime);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }

            }

            return model;
        }

        public async Task<User> RegisterUser(User model)
        {
            const string sqlExpression = "sp_insert_user";

            using (SqlConnection connection = new(GlobalConfig.ConnectionString))
            {
                try
                {
                    SqlCommand command = new(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    GlobalConfig.CreatePasswordHash(model.Password, out string passwordHash, out string passwordSalt);
                    model.PasswordHash = passwordHash;
                    model.PasswordSalt = passwordSalt;

                    command.Parameters.AddWithValue("firstname", model.FirstName);
                    command.Parameters.AddWithValue("lastname", model.LastName);
                    command.Parameters.AddWithValue("email", model.Email);
                    command.Parameters.AddWithValue("passwordhash", model.PasswordHash);
                    command.Parameters.AddWithValue("passwordsalt", model.PasswordSalt);


                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }

            return model;
        }

        //    public async Task<Posts> UploadRitual(Posts model)
        //    {
        //        const string sqlExpression = "sp_Insert_Post";

        //        using (SqlConnection connection = new(GlobalConfig.ConnectionString))
        //        {
        //            try
        //            {
        //                SqlCommand command = new(sqlExpression, connection);
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.Parameters.AddWithValue("title", model.Title);
        //                command.Parameters.AddWithValue("location", model.Location);
        //                command.Parameters.AddWithValue("searchaddress", model.SearchAddress);
        //                command.Parameters.AddWithValue("text", model.Text);
        //                command.Parameters.AddWithValue("imgurl", model.ImgUrl);
        //                command.Parameters.AddWithValue("userid",model.UserId);

        //                await connection.OpenAsync();
        //                await command.ExecuteNonQueryAsync();
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //            finally
        //            {
        //                await connection.CloseAsync();
        //            }

        //        }

        //        return model;
        //    }

        //    public async Task<Posts> UploadRitual2(Posts posts)
        //    {
        //        const string sqlExpression = "sp_Insert_Post2";

        //        using (SqlConnection connection = new(GlobalConfig.ConnectionString))
        //        {
        //            try
        //            {
        //                SqlCommand command = new(sqlExpression, connection);
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.Parameters.AddWithValue("title", posts.Title);
        //                command.Parameters.AddWithValue("location", posts.Location);
        //                command.Parameters.AddWithValue ("searchaddress", posts.SearchAddress);
        //                command.Parameters.AddWithValue("text",posts.Text);
        //                command.Parameters.AddWithValue("userid", posts.UserId );



        //                await connection.OpenAsync();
        //                await command.ExecuteNonQueryAsync();
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //            finally
        //            {
        //                await connection.CloseAsync();
        //            }
        //        }

        //        return posts;
        //    }
        //}
    }
}