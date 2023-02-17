using System;
using System.Collections.Generic;
using System.Configuration;
using Dapper;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperDAL.Models;
using _ad = DapperDAL.ArtistDataAccess;

namespace RecordDbSqlDapper.Tests
{
    public class ArtistTest
    {
        internal static void CreateArtist()
        {
            var artist = new ArtistModel
            {
                FirstName = "James",
                LastName = "Robson",
                Biography = "This is the Bio for James Robson."
            };

            var artistId = _ad.AddArtist(artist);

            if (artistId > 0 && artistId < 9999)
            {
                Console.WriteLine($"Artist added with Id: {artistId}.");
            }
            else if (artistId == 9999)
            {
                Console.WriteLine("ERROR: Artist already exists in the database!");
            }
            else
            {
                Console.WriteLine("ERROR: Artist couldn't be added to the database!");
            }
        }

        internal static void CreateArtistSP()
        {
            ArtistModel artist = new()
            {
                FirstName = "Ethan",
                LastName = "Robson",
                Biography = "Ethan is a Jazz Fusion artist."
            };

            var artistId = _ad.AddArtistSP(artist);

            Console.WriteLine(artistId);
        }

        internal static void GetArtistByName(string name)
        {
            var artistToFind = new ArtistModel { Name = name };

            var artist = _ad.GetArtistByName(artistToFind);
            var message = artist?.ArtistId > 0 ? $"Id: {artist.ArtistId} - {artist.FirstName} {artist.LastName}." : "ERROR: Artist not found!";

            Console.WriteLine(message);
        }

        internal static void GetArtistByNameSP(string name)
        {
            var artistToFind = new ArtistModel { Name = name };

            var artist = _ad.GetArtistByNameSP(artistToFind);
            var message = artist?.ArtistId > 0 ? $"Id: {artist.ArtistId} - {artist.FirstName} {artist.LastName}." : "ERROR: Artist not found!";

            Console.WriteLine(message);
        }

        internal static void GetAllArtists()
        {
            var artists = _ad.GetArtists();

            foreach (var artist in artists)
            {
                PrintArtist(artist);
            }
        }


        internal static void GetAllArtistsSP()
        {
            var artists = _ad.GetArtistsSP();

            foreach (var artist in artists)
            {
                PrintArtist(artist);
            }
        }

        internal static void PrintArtist(ArtistModel artist)
        {
            try
            {
                var bio = string.IsNullOrEmpty(artist.Biography) ? "No Biography" : (artist.Biography.Length > 30 ? artist.Biography.Substring(0, 30) + "..." : artist.Biography);
                Console.WriteLine($"Id: {artist.ArtistId} - {artist.FirstName} {artist.LastName}\n{bio}\n");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"No artist was found.\n\n{ex.Message}");
            }
        }

        internal static void UpdateArtist(int artistId)
        {
            ArtistModel artist = new()
            {
                ArtistId = artistId,
                FirstName = "Alan",
                LastName = "Robson",
                Biography = "Alan is an Australian C&W superstar."
            };

            var i = _ad.UpdateArtist(artist);

            var message = i > 0 ? "Artist updated." : "ERROR: Artist not updated!";
            Console.WriteLine(message);

        }

        internal static void UpdateArtistSP(int artistId)
        {
            ArtistModel artist = new()
            {
                ArtistId = artistId,
                FirstName = "Alano",
                LastName = "Robosono",
                Biography = "Alano is an Austrian C&W superstar."
            };

            var result = (int)_ad.UpdateArtistSP(artist);

            var message = result > 0 ? "Artist updated." : "ERROR: Artist not updated!";
            Console.WriteLine(message);
        }


        internal static void GetArtistById(int artistId)
        {
            var artist = _ad.GetArtistById(artistId);
            var message = artist?.ArtistId > 0 ? $"Id: {artist.ArtistId} - {artist.FirstName} {artist.LastName}." : "ERROR: Artist not found!";

            Console.WriteLine(message);
        }

        internal static void GetArtistByIdSP(int artistId)
        {
            var artist = _ad.GetArtistByIdSP(artistId);
            var message = artist?.ArtistId > 0 ? $"Id: {artist.ArtistId} - {artist.FirstName} {artist.LastName}." : "ERROR: Artist not found!";

            Console.WriteLine(message);
        }

        internal static void DeleteArtist(int artistId)
        {
            int result = _ad.DeleteArtist(artistId);
            var message = result > 0 ? "Artist deleted." : "ERROR: Artist not deleted!";
            Console.WriteLine(message);
        }

        internal static void DeleteArtistSP(int artistId)
        {
            int result = _ad.DeleteArtistSP(artistId);
            var message = result > 0 ? "Artist deleted." : "ERROR: Artist not deleted!";
            Console.WriteLine(message);
        }

        internal static void GetBiography(int artistid)
        {
            var biography = _ad.GetBiography(artistid);

            if (biography.Length > 5)
            {
                Console.WriteLine(biography);
            }
        }

        internal static void GetBiographySP(int artistid)
        {
            var biography = _ad.GetBiographySP(artistid);

            if (biography.Length > 5)
            {
                Console.WriteLine(biography);
            }
        }

        internal static void ArtistHtml(int artistId)
        {
            var artist = _ad.GetArtistById(artistId);
            var message = artist?.ArtistId > 0 ? $"<p><strong>Id:</strong> {artist.ArtistId}</p>\n<p><strong>Name:</strong> {artist.FirstName} {artist.LastName}</p>\n<p><strong>Biography:</strong></p>\n<div>{artist.Biography}</p></div>" : "ERROR: Artist not found!";

            Console.WriteLine(message);
        }

        internal static void ArtistHtmlSP(int artistId)
        {
            var artist = _ad.GetArtistById(artistId);
            var message = artist?.ArtistId > 0 ? $"<p><strong>Id:</strong> {artist.ArtistId}</p>\n<p><strong>Name:</strong> {artist.FirstName} {artist.LastName}</p>\n<p><strong>Biography:</strong></p>\n<div>{artist.Biography}</p></div>" : "ERROR: Artist not found!";

            Console.WriteLine(message);
        }

        internal static void GetArtistId(string firstName, string lastName)
        {
            var artistToFind = new ArtistModel { FirstName = firstName, LastName = lastName };

            var artist = _ad.GetArtistByFirstLastName(artistToFind);
            var message = artist?.ArtistId > 0 ? $"Id: {artist.ArtistId} - {artist.FirstName} {artist.LastName}." : "ERROR: Artist not found!";

            Console.WriteLine(message);

        }

        internal static void GetArtistIdSP(string firstName, string lastName)
        {
            var artistToFind = new ArtistModel { FirstName = firstName, LastName = lastName };

            var artist = _ad.GetArtistByFirstLastNameSP(artistToFind);
            var message = artist?.ArtistId > 0 ? $"Id: {artist.ArtistId} - {artist.FirstName} {artist.LastName}." : "ERROR: Artist not found!";

            Console.WriteLine(message);

        }

        internal static void GetArtistsWithNoBio()
        {
            List<ArtistModel> artists = _ad.GetArtistsWithNoBio();

            foreach (var artist in artists)
            {
                Console.WriteLine($"Id: {artist.ArtistId} - {artist.Name}");
            }
        }

        internal static void GetArtistsWithNoBioSP()
        {
            List<ArtistModel> artists = _ad.GetArtistsWithNoBioSP();

            foreach (var artist in artists)
            {
                Console.WriteLine($"Id: {artist.ArtistId} - {artist.Name}");
            }
        }

        internal static void GetNoBiographyCount()
        {
            var number = _ad.NoBiographyCount();

            Console.WriteLine($"The total number of artists with missing biographies: {number}.");
        }

        internal static void GetNoBiographyCountSP()
        {
            var number = _ad.NoBiographyCount();

            Console.WriteLine($"The total number of artists with missing biographies: {number}.");
        }
    }
}