﻿using LockWarden.DataAccess.Constants;
using LockWarden.DataAccess.Interfaces.IRepositories;
using LockWarden.Domain.Models;
using Microsoft.Data.Sqlite;
namespace LockWarden.DataAccess.Repositories

{

    public class ImageRepository : IImageRepository
    {

        private readonly SqliteConnection _sqliteConnection = new(DB_Constants.DB_Path_File);
        public async Task<bool> CreateAsync(Image entity)
        {
            try
            {
                await _sqliteConnection.OpenAsync();
                string query = "insert into images(deleted,name,content,user_id) " +
                    "values (@deleted,@name,@content, @user_id);";
                SqliteCommand command = new SqliteCommand(query, _sqliteConnection)
                {
                    Parameters =
                    {
                        new SqliteParameter("deleted",entity.Deleted),
                        new SqliteParameter("name",entity.Name),
                        new SqliteParameter("content",entity.Content),
                        new SqliteParameter("user_id",entity.UserId)
                    }
                };
                var result = await command.ExecuteNonQueryAsync();
                if (result == 0) return false; else return true;

            }
            catch
            {
                return false;
            }
            finally
            {
                await _sqliteConnection.CloseAsync();
            }

        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _sqliteConnection.OpenAsync();
                string query = $"delete from images where id={id};";
                SqliteCommand command = new SqliteCommand(query, _sqliteConnection);
                var result = await command.ExecuteNonQueryAsync();
                if (result == 0) return false; else return true;

            }
            catch
            {
                return false;
            }
            finally
            {
                await _sqliteConnection.CloseAsync();
            }
        }

        public async Task<Image> FindByLoginAsync(string login)
        {
            try
            {
                await _sqliteConnection.OpenAsync();
                string query = $"select * from images where login={login};";
                SqliteCommand command = new SqliteCommand(query, _sqliteConnection);
                var readly = await command.ExecuteReaderAsync();
                if (await readly.ReadAsync())
                {
                    Image image = new Image(readly.GetInt32(0), readly.GetDateTime(1), readly.GetString(2), readly.GetString(3), readly.GetInt32(4));
                    return image;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                await _sqliteConnection.CloseAsync();
            }
        }

        public async Task<List<Image>> GetAllAsync()
        {
            try
            {
                await _sqliteConnection.OpenAsync();
                string query = $"select * from images;";
                SqliteCommand command = new SqliteCommand(query, _sqliteConnection);
                var readly = await command.ExecuteReaderAsync();
                List<Image> images = new List<Image>();
                while (await readly.ReadAsync())
                {
                    Image image = new Image(readly.GetInt32(0), readly.GetDateTime(1),readly.GetString(2), readly.GetString(3), readly.GetInt32(4));
                    images.Add(image);
                }
                return images;

            }
            catch
            {
                return null;
            }
            finally
            {
                await _sqliteConnection.CloseAsync();
            }
        }

        public async Task<Image> GetAsync(int id)
        {
            try
            {
                await _sqliteConnection.OpenAsync();
                string query = $"select * from images where id={id};";
                SqliteCommand command = new SqliteCommand(query, _sqliteConnection);
                var readly = await command.ExecuteReaderAsync();
                if (await readly.ReadAsync())
                {
                    Image image = new Image(readly.GetInt32(0), readly.GetDateTime(1),  readly.GetString(2), readly.GetString(3), readly.GetInt32(4));
                    return image;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                await _sqliteConnection.CloseAsync();
            }
        }

        public async Task<bool> UpdateAsync(int id, Image entity)
        {
            try
            {
                await _sqliteConnection.OpenAsync();
                string query = "update images set deleted=@deleted,name=@name,content=@content,userId=@user_id;";
                SqliteCommand command = new SqliteCommand(query, _sqliteConnection)
                {
                    Parameters =
                    {
                         new SqliteParameter("deleted",entity.Deleted),
                        new SqliteParameter("name",entity.Name),
                        new SqliteParameter("content",entity.Content),
                        new SqliteParameter("user_id",entity.UserId)
                    }
                };
                var result = await command.ExecuteNonQueryAsync();
                if (result == 0) return false; else return true;

            }
            catch
            {
                return false;
            }
            finally
            {
                await _sqliteConnection.CloseAsync();
            }
        }
    }
}