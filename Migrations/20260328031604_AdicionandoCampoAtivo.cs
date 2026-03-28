using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercadoSimples.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCampoAtivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<bool>(
                 name: "Ativo",
                 table: "Produtos",
                 type: "tinyint(1)",
                 nullable: false,
                 defaultValue: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Produtos");

          
        }
    }
}
