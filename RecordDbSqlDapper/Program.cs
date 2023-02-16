using Dapper;
using _at = RecordDbSqlDapper.Tests.ArtistTest;
using _rt = RecordDbSqlDapper.Tests.RecordTest;


namespace RecordDbSqlDapper
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            #region Artist Methods
            // _at.GetAllArtists();

            // _at.CreateArtist();
            // _at.CreateArtistSP();

            // _at.GetArtistByName("Bob Dylan");

            // _at.UpdateArtist(837);
            // _at.UpdateArtistSP(842);

            // _at.GetArtistById(114);

            // _at.DeleteArtist(838);
            // _at.DeleteArtistSP(842);

            // _at.GetBiography(114);

            // _at.ArtistHtml(114);

            // _at.GetArtistId("Bob", "Dylan");

            // _at.GetArtistsWithNoBio();

            // _at.GetNoBiographyCount();
            #endregion

            #region Record Methods
            // _rt.GetAllRecords();

            // _rt.CreateRecord(838);
            // _rt.CreateRecordSP(842);

            // _rt.GetRecordById(133);

            // _rt.UpdateRecord(5259);
            // _rt.UpdateRecordSP(5260);

            // _rt.DeleteRecord(5260);
            // _rt.DeleteRecordSP(5260);

            // _rt.GetRecordByName("Cutting Edge");

            // _rt.GetRecordsByArtistId(114);

            // _rt.GetArtistRecordsMultipleTables();

            // _rt.GetRecordsByArtistIdMultipleTables(114);

            // _rt.GetRecordsByYear(1974);

            // _rt.GetTotalNumberOfCDs(); 

            // _rt.GetTotalNumberOfDiscs(); 

            // _rt.GetTotalNumberOfRecords(); 

            // _rt.GetTotalNumberOfBlurays();

            // _rt.GetRecordList();

            // _rt.GetRecordListMultipleTables();

            // _rt.CountDiscs(string.Empty);

            // _rt.CountDiscs("DVD");

            // _rt.CountDiscs("CD");

            // _rt.CountDiscs("R");

            // _rt.GetArtistRecordEntity(2196);

            // _rt.GetArtistNumberOfRecords(114);

            // _rt.GetRecordDetails(2196);

            // _rt.GetArtistNameFromRecord(2196);

            // _rt.GetDiscCountForYear(1974);

            // _rt.GetBoughtDiscCountForYear("2019");

            // _rt.GetNoRecordReview();

            // _rt.GetNoReviewCount();

            // _rt.GetTotalArtistCost();

            // _rt.GetTotalArtistDiscs();

            // _rt.RecordHtml(2196);
            #endregion
        }
    }
}