using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contacts.Api.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "CreationDate", "FirstName", "IsBookmarked", "LastName", "LastUpdateDate" },
                values: new object[,]
                {
                    { new Guid("e0ad95a4-a7eb-486d-a9d2-bd59bb659156"), "Put Skalica 25, Split", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3020), "Milan", true, "Rapaić", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3021) },
                    { new Guid("fbe433c5-9d16-4e23-af54-ad06e77d94c4"), "Vukovarska 125, Split", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017), "Marko", true, "Livaja", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017) },
                    { new Guid("897e8aa1-559a-4aee-8f6d-5f3e70d676b2"), "Spinutska 12, Split", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017), "Ivan", false, "Ivić", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017) },
                    { new Guid("9cb7bab3-4638-4e2d-82a5-e43c69f88159"), "Bruna Bušića 33, Split", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017), "Mario", false, "Mandžukić", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017) },
                    { new Guid("27a331bb-01d7-49c9-9846-689a392ee339"), "Matice Hrvatske 13a, Split", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017), "Goran", false, "Ivanišević", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017) },
                    { new Guid("4c0da46d-87fe-4aea-a166-b9428d1a33d9"), "Solinska 44, Split", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017), "Roko", false, "Ukić", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017) },
                    { new Guid("4a2ab59f-40f1-4b3b-9ef3-66b788c4fdc3"), "Velebitska 2, Split", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017), "Mario", false, "Pašalić", new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3017) }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreationDate", "LastUpdateDate", "Value" },
                values: new object[,]
                {
                    { new Guid("d0911701-eff2-4c3e-beed-6cb0e2b740ee"), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(2918), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(2919), "sport" },
                    { new Guid("e4768b3a-fbbd-42ab-83ec-d23e50209772"), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(2899), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(2902), "posao" }
                });

            migrationBuilder.InsertData(
                table: "ContactTags",
                columns: new[] { "Id", "ContactId", "CreationDate", "LastUpdateDate", "TagId" },
                values: new object[,]
                {
                    { new Guid("62211b83-7a50-4655-8740-0b5b4541961a"), new Guid("e0ad95a4-a7eb-486d-a9d2-bd59bb659156"), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3181), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3182), new Guid("e4768b3a-fbbd-42ab-83ec-d23e50209772") },
                    { new Guid("c4197984-53ef-4e0a-9b13-deb2d2c05ca4"), new Guid("fbe433c5-9d16-4e23-af54-ad06e77d94c4"), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3178), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3179), new Guid("d0911701-eff2-4c3e-beed-6cb0e2b740ee") },
                    { new Guid("cef90a2c-1d3c-4485-bea1-8f56a4dc0465"), new Guid("fbe433c5-9d16-4e23-af54-ad06e77d94c4"), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3175), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3175), new Guid("e4768b3a-fbbd-42ab-83ec-d23e50209772") }
                });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "ContactId", "CreationDate", "LastUpdateDate", "Value" },
                values: new object[,]
                {
                    { new Guid("005e8e78-9535-46f5-8fe7-d3b48db66f04"), new Guid("e0ad95a4-a7eb-486d-a9d2-bd59bb659156"), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3138), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3139), "miki.rapaic@st.com" },
                    { new Guid("237a7a2e-798e-4739-a9d0-7ae03ccf56ff"), new Guid("fbe433c5-9d16-4e23-af54-ad06e77d94c4"), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3134), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3135), "livaja@st.com" }
                });

            migrationBuilder.InsertData(
                table: "Numbers",
                columns: new[] { "Id", "ContactId", "CreationDate", "LastUpdateDate", "Value" },
                values: new object[,]
                {
                    { new Guid("005e8e78-9535-46f5-8fe7-d3b48db66f04"), new Guid("e0ad95a4-a7eb-486d-a9d2-bd59bb659156"), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3160), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3160), "0914563298" },
                    { new Guid("237a7a2e-798e-4739-a9d0-7ae03ccf56ff"), new Guid("fbe433c5-9d16-4e23-af54-ad06e77d94c4"), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3156), new DateTime(2024, 1, 18, 11, 17, 58, 410, DateTimeKind.Utc).AddTicks(3157), "0975549875" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
