using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SamuraiApp.Data.Migrations
{
    public partial class samuraiBattleStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                    @"CREATE FUNCTION[dbo].[EarliestBattleFoughtBySamurai](@samuraiId int)
                          RETURNS char(30) AS
                          BEGIN
                            DECLARE @ret char(30)
                            SELECT TOP 1 @ret = Name
                            FROM Battles
                            WHERE Battles.Id IN(SELECT BattleId
                                               FROM SamuraiBattle
                                              WHERE SamuraiId = @samuraiId)
                            ORDER BY StartDate
                            RETURN @ret
                        END"
            );

            migrationBuilder.Sql(
                    @"CREATE VIEW dbo.SamuraiBattleStats
                          AS
                          SELECT dbo.Samurais.Name,
                          COUNT(dbo.SamuraiBattle.BattleId) AS NumberOfBattles,
                                  dbo.EarliestBattleFoughtBySamurai(MIN(dbo.Samurais.Id)) AS EarliestBattle
                          FROM dbo.SamuraiBattle INNER JOIN
                               dbo.Samurais ON dbo.SamuraiBattle.SamuraiId = dbo.Samurais.Id
                          GROUP BY dbo.Samurais.Name, dbo.SamuraiBattle.SamuraiId"
                );




        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
