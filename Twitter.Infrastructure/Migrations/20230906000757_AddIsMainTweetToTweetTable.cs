using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twitter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsMainTweetToTweetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainTweet",
                table: "Tweets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainTweet",
                table: "Tweets");
        }
    }
}
