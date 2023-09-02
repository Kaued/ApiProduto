using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Value('Bebidas', 'bebidas.png')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Value('Lanches', 'lanches.png')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Value('Teste', 'teste.png')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
