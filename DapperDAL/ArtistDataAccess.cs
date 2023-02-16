using DapperDAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection.Metadata;

namespace DapperDAL
{
    public class ArtistDataAccess
    {
        public static List<ArtistModel> GetArtists()
        {
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                var artists = cn.Query<ArtistModel>("SELECT * FROM Artist ORDER BY LastName, FirstName", new DynamicParameters());

                return artists.ToList();
            }
        }

        public static List<ArtistModel> GetArtistsSP()
        {
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                return cn.Query<ArtistModel>("up_ArtistSelectFull", commandType: CommandType.StoredProcedure).ToList(); 
            }
        }

        public static ArtistModel? GetArtistById(int artistId)
        {
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                return cn.Query<ArtistModel>($"SELECT * FROM Artist WHERE ArtistId = {artistId}").FirstOrDefault() ?? new ArtistModel { ArtistId = 0 };
            }
        }

        public static ArtistModel? GetArtistByName(ArtistModel artist)
        {
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                return cn.Query<ArtistModel>("up_ArtistSelectFull", commandType: CommandType.StoredProcedure).FirstOrDefault() ?? new ArtistModel { ArtistId = 0 };
            }
        }

        public static ArtistModel? GetArtistByNameSP(ArtistModel artist)
        {
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", artist.Name);

                return cn.Query<ArtistModel>("up_GetArtistByName", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault() ?? new ArtistModel { ArtistId = 0 };
            }
        }

        public static ArtistModel? GetArtistByFirstLastName(ArtistModel artist)
        {
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                return cn.Query<ArtistModel>("SELECT * FROM Artist WHERE FirstName LIKE @FirstName AND LastName LIKE @LastName", artist).FirstOrDefault() ?? new ArtistModel { ArtistId = 0 };
            }
        }

        public static int UpdateArtist(ArtistModel artist)
        {
            var i = 0;
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                artist.Name = !string.IsNullOrEmpty(artist.FirstName) ? $"{artist.FirstName} {artist.LastName}" : artist.LastName;

                i = cn.Execute("UPDATE Artist SET FirstName = @FirstName, LastName = @LastName, Name = @Name, Biography = @Biography WHERE ArtistId = @ArtistId", artist);
            }

            return i;
        }

        public static object UpdateArtistSP(ArtistModel artist)
        {
            var result = 0;
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                if (string.IsNullOrEmpty(artist.Name))
                {
                    artist.Name = !string.IsNullOrEmpty(artist.FirstName) ? $"{artist.FirstName} {artist.LastName}" : artist.LastName;
                }

                var parameter = new DynamicParameters();
                parameter.Add("@ArtistId", artist.ArtistId);
                parameter.Add("@FirstName", artist.FirstName);
                parameter.Add("@LastName", artist.LastName);
                parameter.Add("@Name", artist.Name);
                parameter.Add("@Biography", artist.Biography);
                parameter.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                cn.Execute("up_UpdateArtist", parameter, commandType: CommandType.StoredProcedure);

                result = parameter.Get<int>("@ArtistId");
            }
            return result;
        }

        public static int AddArtist(ArtistModel artist)
        {
            var artistId = 0;

            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                artist.Name = !string.IsNullOrEmpty(artist.FirstName) ? $"{artist.FirstName} {artist.LastName}" : artist.LastName;

                var foundArtist = cn.QueryFirstOrDefault<ArtistModel>("SELECT * FROM Artist WHERE Name LIKE @Name", artist);
                if (foundArtist != null)
                {
                    artistId = 9999;
                }
                else
                {
                    var number = cn.Execute("INSERT INTO Artist (FirstName, LastName, Name, Biography) VALUES (@FirstName, @LastName, @Name, @Biography)", artist);
                    if (number == 1)
                    {
                        foundArtist = cn.QueryFirstOrDefault<ArtistModel>("SELECT * FROM Artist WHERE Name LIKE @Name", artist);
                        artistId = foundArtist?.ArtistId ?? 0;
                    }
                }
            }

            return artistId;
        }

        public static int AddArtistSP(ArtistModel artist)
        {
            var artistId = 0;
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                artist.Name = !string.IsNullOrEmpty(artist.FirstName) ? $"{artist.FirstName} {artist.LastName}" : artist.LastName;

                var parameter = new DynamicParameters();
                parameter.Add("@ArtistId", artist.ArtistId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                parameter.Add("@FirstName", artist.FirstName);
                parameter.Add("@LastName", artist.LastName);
                parameter.Add("@Name", artist.Name);
                parameter.Add("@Biography", artist.Biography);

                cn.Execute("adm_ArtistInsert", parameter, commandType: CommandType.StoredProcedure);

                //To get newly created ID back  
                artistId = parameter.Get<int>("@ArtistId");
            }
            return artistId;
        }

        public static int DeleteArtist(int artistId)
        {
            var result = 0;
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                // Delete records before deleting artist
                var records = cn.Query<RecordModel>($"SELECT * FROM Record WHERE artistId = {artistId}", new DynamicParameters());
                foreach (var record in records)
                {
                    cn.Execute($"DELETE FROM Record WHERE ArtistId={artistId}");
                }

                result = cn.Execute($"DELETE FROM Artist WHERE ArtistId={artistId}");
            }

            return result;
        }

        public static int DeleteArtistSP(int artistId)
        {
            var result = 0;
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ArtistId", artistId);

                result = (int)cn.Execute("up_deleteArtist", parameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        /// <summary>
        /// Get biography from the current record Id.
        /// </summary>
        public static string GetBiography(int artistId)
        {
            var biography = new StringBuilder();

            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                var artist = cn.Query<ArtistModel>("SELECT * FROM Artist WHERE artistId = @artistId", new { artistId }).FirstOrDefault();
                if (artist is ArtistModel)
                {
                    biography.Append($"Name: {artist.Name}\n");
                    biography.Append($"Biography:\n{artist.Biography}");
                }
            }

            return biography.ToString();
        }

        public static List<ArtistModel> GetArtistsWithNoBio()
        {
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                var artists = cn.Query<ArtistModel>("SELECT * FROM Artist WHERE Biography IS NULL;").ToList();

                return artists;
            }
        }

        public static int NoBiographyCount()
        {
            var number = 0;
            using (IDbConnection cn = new SqlConnection(LoadConnectionString()))
            {
                number = (int)cn.Query<ArtistModel>("SELECT * FROM Artist WHERE Biography IS NULL;").Count();
            }
            return number;
        }

        private static string LoadConnectionString(string id = "RecordDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
