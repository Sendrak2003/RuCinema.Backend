using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerUpdateReleasedEpisodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE TRIGGER UpdateReleasedEpisodes
            ON Episodes
            AFTER INSERT
            AS
            BEGIN
                UPDATE Movies
                SET Released_Episodes = (SELECT COUNT(*) FROM Episodes WHERE Movie_ID = m.Movie_ID)
                FROM Movies m
                INNER JOIN inserted i ON m.Movie_ID = i.Movie_ID;
            END;
           ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER UpdateReleasedEpisodes");
        }
    }
}
