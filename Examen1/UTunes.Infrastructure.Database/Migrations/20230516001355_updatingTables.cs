using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTunes.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class updatingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SongPrice",
                table: "Song",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Album",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Album",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "Album",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Album",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Dislikes", "Likes", "Votes" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Album",
                columns: new[] { "Id", "Artist", "Dislikes", "Likes", "Name", "Review", "Votes" },
                values: new object[] { 2, "One Directions", 0, 0, "What makes you beautiful", "Lorem ipsum yes dolor sit amet, consectetur adipiscing elit. Aenean sed leo elit. Nullam tellus ipsum, fringilla quis ex vitae, mattis rutrum felis. Duis venenatis faucibus turpis, at tincidunt arcu bibendum ac. Vestibulum eget placerat libero, nec tempus ipsum. Sed elit libero, luctus non dapibus et, sagittis a tellus. Ut suscipit porta vestibulum. Mauris justo velit, pretium at efficitur nec, posuere non massa. Proin quis aliquet quam. Maecenas malesuada mauris ex, eu sollicitudin quam laoreet ut. Sed mollis enim dolor, eu malesuada dui aliquet ut. Quisque rhoncus augue urna, at volutpat justo ultrices et. Vivamus maximus quam non nisl placerat varius. Nam mollis erat ullamcorper diam efficitur, molestie feugiat urna finibus. Phasellus dignissim interdum neque sed dictum.", 0 });

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 1,
                column: "SongPrice",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 2,
                column: "SongPrice",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 3,
                column: "SongPrice",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 4,
                column: "SongPrice",
                value: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Album",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "SongPrice",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "Votes",
                table: "Album");
        }
    }
}
