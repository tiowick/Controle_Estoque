﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Controle_Estoque.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Coluna_FilialId_Empresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilialId",
                table: "Empresas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FilialId",
                table: "Empresas",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }
    }
}
